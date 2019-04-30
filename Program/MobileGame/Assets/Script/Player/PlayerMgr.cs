using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameScene;

public struct SaveData
{
    public string BattleSceneName;
    public Vector3 PS;
    public Dictionary<string, SceneData> _SceneData;
    public Dictionary<string, SceneData> SceneData
    {
        get
        {
            if(_SceneData == null)
            {
                _SceneData = new Dictionary<string, SceneData>();
            }
            return _SceneData;
        }
    }
}

public class PlayerMgr {
    #region 对外接口
    BaseCharacter _PlayerCharactor;
    public BaseCharacter PlayerCharactor
    {
        get
        {
            return _PlayerCharactor;
        }
        set
        {
            _PlayerCharactor = value;
        }
    }
    public string GetPlayerInfo()
    {
        return "Player";
    }
    //设置玩家当前场景及位置
    public void SavePlayerLocation(string sceneName, Vector3 location)
    {
        GameMemory.BattleSceneName = sceneName;
        GameMemory.PS = location;
    }

    public string CurSceneName
    {
        get
        {
            return GameMemory.BattleSceneName;
        }set
        {
            GameMemory.BattleSceneName = value;
        }
    }
    public Vector3 CurLocation
    {
        get
        {
            return GameMemory.PS;
        }
        set
        {
            GameMemory.PS = value;
        }
    }

    //获取场景
    public SceneData GetSceneData(string sceneName)
    {
       
        SceneData returnData = new SceneData();
        /*if(! GameMemory.SceneData.TryGetValue(sceneName,out returnData))
       {
           returnData = APP.SceneDataManager.GetSceneData(sceneName);
       }*/
        return returnData;
    }

    public void SetSceneData( SceneData data )
    {
        GameMemory.SceneData[data.SceneName] = data;
    }
    //存档信息
    public SaveData GameMemory;
    #endregion
    #region 内部属性

    #endregion
    static PlayerMgr _Mgr;
    public static PlayerMgr Mgr
    {
        get
        {
            if(_Mgr== null)
            {
                _Mgr = new PlayerMgr();
            }
            return _Mgr;
        }
    }

    PlayerMgr()
    {
    }
}
