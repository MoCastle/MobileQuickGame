using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene : BaseScene
{
    #region 对外属性
    public BattleDir BattleDir
    {
        get
        {
            return _BattleDir;
        }
    }
    #endregion
    #region 对外方法
    #endregion
    BattleDir _BattleDir;

    public BattleScene(BattleDir dir):base(dir)
    {
        _BattleDir = dir as BattleDir;
        BattleMgr.Mgr.CurDir = dir;
    }
}
