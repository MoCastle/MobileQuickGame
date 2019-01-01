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
    Transform _NPCCollision;
    Transform NPCCollision
    {
        get
        {
            if(_NPCCollision==null)
            {
                _NPCCollision = transform.FindChild("NPCCollision");
            }
            return _NPCCollision;
        }
    }
    LinkedList<EnemyObj> NPCList; 
    
    public SceneDoor[] Doors
    {
        get
        {
            Transform doors = transform.FindChild("Doors");
            if (doors != null)
            {
                _Doors = new SceneDoor[doors.transform.childCount];
                for (int doorIdx = 0; doorIdx < doors.transform.childCount; ++doorIdx)
                {
                    _Doors[doorIdx] = doors.GetChild(doorIdx).GetComponent<SceneDoor>();
                }
            }else
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
        base.EnterScene();
        _UIMgr.ShowUI("ScrollArea");
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
        SceneData data = PlayerMgr.Mgr.GetSceneData(sceneName);
        NPCList = new LinkedList<EnemyObj>();
        if (data.EnemyArr!=null)
        {
            if (NPCCollision != null)
                NPCCollision.gameObject.active = false;
            foreach( BaseCharacter character in data.EnemyArr )
            {
                BaseActorObj newActor = ActorManager.Mgr.GenActor(character.Propty.ActorInfo.Name);
                if (!character.Propty.IsDeath)
                {
                    if(newActor == null)
                    {
                        Debug.Log("error:BattleDir.LoadSceneData Cant BuildActor");
                        continue;
                    }
                    NPCList.AddLast(newActor as EnemyObj);
                    newActor.Character = character;
                    ActorList.AddFirst(newActor);
                }
            }
        }else
        {
            foreach( Transform NPC in NPCCollision )
            {
                EnemyObj actor = NPC.GetComponent<EnemyObj>();
                NPCList.AddFirst(actor);
            }
        }
    }
    
    //存档
    protected void SaveSceneData()
    {
        if(IfSaveData)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneData data = PlayerMgr.Mgr.GetSceneData(sceneName);
            data.SceneName = sceneName;
            if(NPCList!=null)
            {
                data.EnemyArr = new BaseCharacter[NPCList.Count];
                LinkedListNode<EnemyObj> actorNode = null;
                for(int idx = 0;idx<NPCList.Count;++idx)
                {
                    actorNode = idx == 0 ? NPCList.First : actorNode.Next;
                    data.EnemyArr[idx] = actorNode.Value.Character;
                }
            }
            PlayerMgr.Mgr.SetSceneData(data);
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
        if(IfSaveData)
        {
            string SceneName = SceneManager.GetActiveScene().name;
            _PlayerMgr.CurSceneName = SceneName;
            _PlayerMgr.CurLocation = PlayerActor.transform.position;
            
        }
        base.Leave();

    }
}
