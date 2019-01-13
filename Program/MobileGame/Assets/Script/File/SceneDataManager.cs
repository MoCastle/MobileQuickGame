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
    string SceneName;
    int Idx;
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
    //该功能只能在编辑模式下执行
    public void Save()
    {
        string saveInfo = JsonUtility.ToJson(_SceneData);
        File.WriteAllText(PathManager.SceneData, saveInfo, Encoding.UTF8);
    }
    public SceneData Load(string name)
    {
        SceneData newData = new SceneData();
        newData.SceneName = name;
        _SceneData.TryGetValue(name,out newData);
        return newData;
    }

    public void SetSceneData( SceneData data )
    {
        this._SceneData[data.SceneName] = data;
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
        try
        {
            _SceneData = JsonUtility.FromJson<Dictionary<string,SceneData>>(jsStr);
        }
        catch
        { }
    }

    
}
