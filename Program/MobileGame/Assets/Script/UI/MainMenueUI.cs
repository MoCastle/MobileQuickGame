using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenueUI : BaseUI {
    public void OnEnterGameBtn()
    {
        BattleSceneInfo info = new BattleSceneInfo();
        info.Name = "EnterSence_1";
    
    }
    public void OnEnterTestBtn()
    {
        BattleSceneInfo info = new BattleSceneInfo();
        info.Name = "TestSence";
        //BattleMgr.Mgr.EnterBattle(info);
    }

}
