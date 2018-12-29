using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDir : MonoBehaviour {
    public Camera MainCamera;
    protected GameCtrl _GM;
    protected UIManager _UIMgr;
    protected SceneMgr _SceneMgr;
	protected virtual void Awake()
    {
        _GM = GameCtrl.GameCtrler;
        _UIMgr = UIManager.Mgr;
        _SceneMgr = SceneMgr.Mgr;
        _SceneMgr.EnterScene(new BattleScene(this));
    }
    public abstract void EnterScene();
}
