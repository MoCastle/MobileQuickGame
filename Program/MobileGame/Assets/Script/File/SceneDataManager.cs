using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
[Serializable]
public struct DoorData
{
    public Vector3 Position;
    public string SceneName;
    public int Idx;
    public void SetData(SceneDoor door)
    {
        door.transform.position = Position;
        door.SceneName = SceneName;
        door.Idx = Idx;
    }
    public void WriteData(SceneDoor door)
    {
        Position = door.transform.position;
        SceneName = door.SceneName;
        Idx = door.Idx;
    }
}

[Serializable]
public struct SceneData
{
    //场景名字
    public string SceneName;
    public CharacterData[] EnemyArr;
    public DoorData[] DoorArr;
}

public class SceneDataManager {
    static SceneDataManager _Mgr;
    public static SceneDataManager Mgr
    {
        get
        {
            if(_Mgr == null)
            {
                _Mgr = new SceneDataManager();
            }
            return _Mgr;
        }
    }
    public SceneData GetSceneData( string sceneName)
    {
        SceneData returnData;
        _SceneData.TryGetValue(sceneName, out returnData);
        return returnData;
    }
    
    SceneDataManager()
    {
        _SceneData = new Dictionary<string, SceneData>();
        _Load();
    }
    Dictionary<string, SceneData> _SceneData;
    private void _Load()
    {
        byte[] bytes = LoaderFile.LoadBytes(PathManager.SceneData, Application.platform == RuntimePlatform.Android);
        string jsStr = Encoding.UTF8.GetString(bytes);
        SlzDictionary<string, SceneData> slzDictionary = JsonUtility.FromJson<SlzDictionary<string, SceneData>>(jsStr);
        _SceneData = slzDictionary.ToDictionary();
    }

    
}
