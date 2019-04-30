using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : BaseUI {
    public void OnClick()
    {
       // BattleDir battleScene = SceneMgr.Mgr.CurDir as BattleDir;
        //battleScene.ReBirth();
        this.Close();
    }
}
