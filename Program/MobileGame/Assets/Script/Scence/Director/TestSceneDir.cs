using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneDir : MonoBehaviour {
    GameCtrl _GM;
    UIManager _UIMgr;
    private void Awake()
    {
        _GM = GameCtrl.GameCtrler;
        _UIMgr = UIManager.Mgr;
        _UIMgr.ShowUI("ScrollArea");
    }
}
