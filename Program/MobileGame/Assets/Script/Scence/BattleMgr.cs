/*
 * 作者:Mo
 * 该脚本统一管理战斗相关场景、导演
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BattleSceneInfo
{
    public string Name;
    public int Idx;
    public Vector3 PS;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inName">场景名字</param>
    /// <param name="inIdx">出生点索引,小于0表示使用坐标出生</param>
    /// <param name="inPS">坐标出生</param>
    /*
    public BattleSceneInfo(string inName = "TestSence", int inIdx = 0,Vector3 inPS = new Vector3())
    {
        Name = "TestSence";
        Idx = inIdx;
        PS = inPS;
    }
    */
}
public class BattleMgr {
    /*
    #region 外部功能
    public void EnterBattle(BattleSceneInfo inSceneInfo)
    {
        _CurSceneInfo = inSceneInfo;
        //SceneMgr.Mgr.JumpScene(inSceneInfo.Name);
    }
    #endregion

    #region 对外属性
    BattleSceneInfo _CurSceneInfo;
    public BattleSceneInfo CurSceneInfo
    {
        get
        {
            return _CurSceneInfo;
        }
    }
    
    BattleScene _CurBattleScene;
    /// <summary>
    /// 玩家所在场景及位置
    /// </summary>
    public BattleScene CurBattleScene
    {
        get
        {
            return _CurBattleScene;
        }
        set
        {
            _CurBattleScene = value;
        }
    }
    /// <summary>
    /// 当前战斗场景导演
    /// </summary>
    /*BattleDir _CurDir;
     public BattleDir CurDir
     {

         get
         {
             return _CurBattleScene.BattleDir;
         }
    set
        {
            _CurDir = value;
        }
}
#endregion
#region 内部功能
static BattleMgr _Mgr;
    public static BattleMgr Mgr
    {
        get
        {
            if (_Mgr == null)
            {
                _Mgr = new BattleMgr();
            }
            return _Mgr;
        }
    }

    BattleMgr()
    {

    }
    #endregion
*/
}
