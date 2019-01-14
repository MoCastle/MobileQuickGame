/*作者:Mo
 * 用于战斗场景控制
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleDir : BaseDir
{
    PlayerMgr _PlayerMgr;
    [Title("该关卡是否需要存档", "black")]
    public bool IfSaveData = true;
    SceneDoor[] _Doors;
    Transform _DoorCollision;
    Transform DoorCollision
    {
        get
        {
            if(_DoorCollision == null)
            {
                _DoorCollision = (new GameObject("DoorCollision")).transform;
            }
            return _DoorCollision;
        }
    }
    LinkedList<EnemyObj> NPCList; 
    
    public SceneDoor[] Doors
    {
        get
        {
            if (_Doors == null)
            {
                _Doors = new SceneDoor[0];
            }
            return _Doors;
        }
    }

    public PlayerObj PlayerActor;
    //进入
    public override void EnterScene()
    {
        LoadSceneData();
        base.EnterScene();
        _UIMgr.ShowUI("ScrollArea");
        _UIMgr.ShowUI("MainWindow");
        BattleSceneInfo info = BattleMgr.Mgr.CurSceneInfo;
        if(PlayerActor== null)
            GenPlayer(info.PS);
        if (PlayerActor != null && Doors.Length > 0 && info.Idx >= 0 && Doors[info.Idx] != null)
            Doors[info.Idx].GenPlayer(PlayerActor.transform);

        if (MainCamera == null&&PlayerActor!=null)
        {
            MainCamera = Camera.main;
            if(MainCamera == null)
            {
                MainCamera = Resources.Load("Prefab\\Camera\\Main Camera") as Camera;
            }
            CameraObj cameraObj= MainCamera.GetComponent<CameraObj>();
            cameraObj.TraceTarget = PlayerActor.transform.FindChild("CameraPoint");
        }
    }

    //读档
    protected void LoadSceneData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneData sceneData = PlayerMgr.Mgr.GetSceneData(sceneName);
        NPCList = new LinkedList<EnemyObj>();
        if (sceneData.EnemyArr!=null)
        {
            foreach( CharacterData character in sceneData.EnemyArr )
            {
                if (!character.Propty.IsDeath)
                {
                    BaseActorObj newActor = ActorManager.Mgr.GenActor(character.Propty.ActorInfo.Name);
                    NPCList.AddLast(newActor as EnemyObj);
                    newActor.ActorPropty = character.Propty;
                    ActorList.AddFirst(newActor);
                    newActor.transform.position = character.Position;
                }
            }
        }
        if(sceneData.DoorArr!=null)
        {
            _Doors = new SceneDoor[sceneData.DoorArr.Length];
            for(int doorIdx =0;doorIdx< sceneData.DoorArr.Length;++doorIdx)
            {
                DoorData doorData = sceneData.DoorArr[doorIdx];
                GameObject loadObj = Resources.Load<GameObject>("Prefab\\Scene\\Door");
                GameObject instntiateObj = GameObject.Instantiate(loadObj);
                SceneDoor door = instntiateObj.GetComponent<SceneDoor>();
                _Doors[doorIdx] = door;
                doorData.SetData(door);
                door.transform.SetParent(DoorCollision.transform);
            }
        }
    }
    
    //存档
    protected void SaveSceneData()
    {
        if(IfSaveData)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneData sceneData = PlayerMgr.Mgr.GetSceneData(sceneName);
            sceneData.SceneName = sceneName;
            if(NPCList!=null)
            {
                sceneData.EnemyArr = new CharacterData[NPCList.Count];
                LinkedListNode<EnemyObj> actorNode = null;
                for(int idx = 0;idx<NPCList.Count;++idx)
                {
                    actorNode = idx == 0 ? NPCList.First : actorNode.Next;
                    sceneData.EnemyArr[idx].Propty = actorNode.Value.Character.Propty;
                    sceneData.EnemyArr[idx].Position = actorNode.Value.transform.position;
                }
            }
            sceneData.DoorArr = new DoorData[DoorCollision.transform.childCount];
            for (int idx = 0; idx < DoorCollision.transform.childCount; ++idx)
            {
                Transform doorTrans = DoorCollision.transform.GetChild(idx);
                SceneDoor door = doorTrans.GetComponent<SceneDoor>();
                sceneData.DoorArr[idx].WriteData(door);
            }
            PlayerMgr.Mgr.SetSceneData(sceneData);
        }
    }

    //生成玩家
    protected virtual void GenPlayer(Vector3 BirthPS)
    {
        BaseActorObj player = ActorManager.Mgr.GenActor("Player");
        PlayerActor = player as PlayerObj;
        PlayerActor.transform.position = BirthPS;
        SynChracterInfo();
        ActorList.AddFirst(player);
    }

    //同步玩家数据
    protected virtual void SynChracterInfo()
    {
        BaseCharacter playerCharacter = PlayerMgr.Mgr.PlayerCharactor;
        if (playerCharacter != null)
        {
            PlayerActor.Character = playerCharacter;
        }
        else
        {
            PlayerMgr.Mgr.PlayerCharactor = playerCharacter;
        }
    }

    //重生玩家
    public virtual void ReBirth()
    {
        PlayerActor.Birth();
    }
    protected override void Awake()
    {
        _PlayerMgr = PlayerMgr.Mgr;
        base.Awake();
        _SceneMgr.EnterScene(new BattleScene(this));

    }
    public override void Leave()
    {
        SaveSceneData();
        base.Leave();

    }
}
