using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneDir : BaseDir {

    public override void EnterScene()
    {
        
    }
    private void Start()
    {
        _UIMgr.ShowUI("ScrollArea");
        _SceneMgr.EnterScene(new BattleScene(this));
    }
}
