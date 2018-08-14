using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenceDir : MonoBehaviour {
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
    void Awake () {
        CenceMgr.Mgr.CurSenceDir = this;
        LogicAwake();
    }

    protected virtual void LogicAwake()
    { }
}
