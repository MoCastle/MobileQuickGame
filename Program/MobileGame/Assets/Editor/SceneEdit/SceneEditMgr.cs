using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;
using GameScene;
using LitJson;
public class SceneEditMgr {
    static SceneEditMgr _Mgr;
    public static SceneEditMgr Mgr
    {
        get
        {
            if(_Mgr == null)
            {
                _Mgr = new SceneEditMgr();
            }
            return _Mgr;
        }
    }
    static GameObject _NPCListOBJ;
    static GameObject _SceneObj;
    static GameObject _TranslateDoor;

    public void LoadSceneData()
    {
        _Load();
        if (_SceneObj != null)
        {
            GameObject.DestroyImmediate(_SceneObj);
        }
        GameObject newObj = new GameObject("场景配置对象");
        _SceneObj = newObj;
        newObj = new GameObject("怪物摆放");
        _NPCListOBJ = newObj;
        _NPCListOBJ.transform.SetParent(_SceneObj.transform);
        newObj = new GameObject("传送门");
        _TranslateDoor = newObj;
        _TranslateDoor.transform.SetParent(_SceneObj.transform);

        string sceneName = SceneManager.GetActiveScene().name;
        SceneData data = new SceneData();
        _SceneData.TryGetValue(sceneName, out data);
        if (data.EnemyArr != null)
        {
            foreach (CharacterData characterData in data.EnemyArr)
            {
                if (!characterData.propty.IsDeath)
                {
                    GameObject loadObj = Resources.Load<GameObject>("Prefab\\Character\\" + characterData.propty.name);
                    GameObject instntiateObj = GameObject.Instantiate(loadObj);
                    instntiateObj.name = characterData.propty.m_Name;
                    BaseActorObj newActor = instntiateObj.GetComponent<BaseActorObj>();
                    characterData.ReadActor(newActor);
                    newActor.transform.SetParent(_NPCListOBJ.transform);
                }
            }
        }
        if (data.DoorArr != null)
        {
            foreach (DoorData doorData in data.DoorArr)
            {
                GameObject loadObj = Resources.Load<GameObject>("Prefab\\Scene\\Door");
                GameObject instntiateObj = GameObject.Instantiate(loadObj);
                SceneDoor door = instntiateObj.GetComponent<SceneDoor>();
                doorData.SetData(door);
                door.transform.SetParent(_TranslateDoor.transform);
            }
        }
    }
    public void SaveSceneData()
    {
        if(_SceneObj == null)
        {
            _SceneObj = GameObject.Find("场景配置对象");
        }
        if (_SceneObj != null)
        {
            SceneData sceneData = new SceneData();
            sceneData.SceneName = SceneManager.GetActiveScene().name;
            sceneData.EnemyArr = new CharacterData[_NPCListOBJ.transform.childCount];
            for (int idx = 0; idx < _NPCListOBJ.transform.childCount; ++idx)
            {
                Transform actorTrans = _NPCListOBJ.transform.GetChild(idx);
                BaseActorObj actor = actorTrans.GetComponent<BaseActorObj>();
                sceneData.EnemyArr[idx].WriteActor(actor);
            }

            sceneData.DoorArr = new DoorData[_TranslateDoor.transform.childCount];
            for (int idx = 0; idx < _TranslateDoor.transform.childCount; ++idx)
            {
                Transform doorTrans = _TranslateDoor.transform.GetChild(idx);
                SceneDoor door = doorTrans.GetComponent<SceneDoor>();
                sceneData.DoorArr[idx].WriteData(door);
            }

            _SceneData[sceneData.SceneName] = sceneData;
            SlzDictionary<string, SceneData> slzDictionary = new SlzDictionary<string, SceneData>(_SceneData);
            string saveInfo = JsonMapper.ToJson(slzDictionary);
            byte[] bytes = Encoding.UTF8.GetBytes(saveInfo);
            File.WriteAllBytes(PathManager.SceneData, bytes);
            GameObject.DestroyImmediate(_SceneObj);

        }
    }

    Dictionary<string, SceneData> _SceneData;
    SceneEditMgr()
    {
        _SceneData = new Dictionary<string, SceneData>();
        
    }
    private void _Load()
    {
        byte[] bytes = LoaderFile.LoadBytes(PathManager.SceneData, Application.platform == RuntimePlatform.Android);
        string jsStr = Encoding.UTF8.GetString(bytes);
        SlzDictionary<string, SceneData> slzDictionary = JsonUtility.FromJson<SlzDictionary<string, SceneData>>(jsStr);//JsonMapper.ToObject<SlzDictionary<string, SceneData>>(jsStr);// 
        _SceneData = slzDictionary.ToDictionary();
    }
}
