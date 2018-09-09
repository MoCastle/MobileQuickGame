using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SenceData
{
    int PlayerSpawnID;
}

public class BaseDir : MonoBehaviour {

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
    private void OnDestroy()
    {
        foreach( BaseActor Actor in ActorMenue.Values )
        {
            if( (Actor as EnemyActor)!=null)
            {
                GamePoolManager.Manager.Despawn(Actor.gameObject.transform);
            }
        }
    }
}
