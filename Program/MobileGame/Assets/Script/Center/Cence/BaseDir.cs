using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SenceData
{
    int PlayerSpawnID;
}

public abstract class BaseDir : MonoBehaviour {

    PlayerActor _Player;
    public PlayerActor Player
    {
        get
        {
            return _Player;
        }
        set
        {
            _Player = value;
        }
    }
    // Use this for initialization

    protected virtual void LogicAwake()
    { }

    Dictionary<int, BaseActor> ActorMenue;
    protected GameCtrl GM;
    private void Awake()
    {
        CenceMgr.Mgr.CurSenceDir = this;
        ActorMenue = new Dictionary<int, BaseActor>();
        GM = GameCtrl.GameCtrler;
        GM.CurDir = this;
        LogicAwake();
    }

    //生命循环相关
    #region 
    //开始函数暴露出去
    public void Start()
    {
        
    }
    public abstract void LogicStart();
    //更新函数暴露出去
    public void Update()
    {
        
    }
    public abstract void LogicUpdate();

    public void End()
    {
        foreach (BaseActor Actor in ActorMenue.Values)
        {
            if ((Actor as EnemyActor) != null)
            {
                GamePoolManager.Manager.Despawn(Actor.gameObject.transform);
            }
        }
    }
    #endregion

    public BaseActor GenActor( string Name )
    {
        BaseActor NewActor = null;
        return NewActor;
    }
    public BaseActor GenActor(GameObject SampleObj)
    {
        GameObject NewEnemy = GamePoolManager.Manager.GenObj(SampleObj);
        BaseActor NewActor = NewEnemy.GetComponent<EnemyActor>();
        ActorMenue.Add(NewActor.ActorID, NewActor);
        return NewActor;
    }
    
}
