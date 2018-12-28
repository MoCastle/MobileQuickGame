using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDir : MonoBehaviour {
    protected GameCtrl _GM;
    protected UIManager _UIMgr;
    protected SceneMgr _SceneMgr;
	void Awake()
    {
        _GM = GameCtrl.GameCtrler;
        _UIMgr = UIManager.Mgr;
        _SceneMgr = SceneMgr.Mgr;
    }
    public abstract void EnterScene();
        
}
