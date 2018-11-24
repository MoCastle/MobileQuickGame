using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class GamePoolManager {
    SpawnPool GamePool;
    static GamePoolManager _Manager;
    static Dictionary<string, GameObject> _ObjDict = new Dictionary<string, GameObject>();

    public static GamePoolManager Manager
    {
        get
        {
            if (_Manager == null)
            {
                _Manager = new GamePoolManager();
            }
            return _Manager;
        }
    }

    GamePoolManager()
    {
        GameCtrl GameCtrler = GameCtrl.GameCtrler;
        GamePool = GameCtrler.Pool;
        _ObjDict = new Dictionary<string, GameObject>();
    }

    //判断是否已经放在池子里了
    public bool IsSpawned(Transform Trans)
    {
        bool Juger = false;

        return GamePool.IsSpawned(Trans);
    }

    //注册
    public void Regist(string InName, Transform NewObj)
    {
        PrefabPool ReturnPool = new PrefabPool();
        if (!GamePool.prefabPools.TryGetValue(InName, out ReturnPool) && NewObj != null)
        {
            ReturnPool = null;
            ReturnPool = new PrefabPool(NewObj.transform);
            ReturnPool.cullDespawned = true;
            ReturnPool.cullDelay = 5;
            GamePool.CreatePrefabPool(ReturnPool);
        }
    }
    //从池子里取
    public Transform Spawn(string InName)
    {
        return GamePool.Spawn(InName);
    }
    //放回
    public void Despawn(Transform Target)
    {
        GamePool.Despawn(Target);
    }
    //读取并生成对象
    public GameObject GenObj(string road)
    {
        GameObject newObj = null;
        if (!_ObjDict.TryGetValue(road, out newObj))
        {
            newObj = Resources.Load(road) as GameObject;
        }
        newObj = GenObj(newObj);
        return newObj;
    }
    //从池子中生成目标对象
    public GameObject GenObj( GameObject SamplaeObj )
    {
        //检查是否有注册过
        if (SamplaeObj == null)
        {
            return null;
        }
        if (!GamePool.IsSpawned(SamplaeObj.transform))
        {
            Regist(SamplaeObj.name, SamplaeObj.transform);
        }
        Transform Target = GamePool.Spawn(SamplaeObj.name);
        return Target.gameObject;
    }
}
