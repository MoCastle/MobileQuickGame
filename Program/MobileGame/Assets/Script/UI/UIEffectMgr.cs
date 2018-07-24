using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIEffectMgr {
    public static WindowMgrObj UIWindow;
    public static GamePoolManager GamePool
    {
        get
        {
            return GamePoolManager.Manager;
        }
    }

    public static GameObject AddHurtInfo( string InTextInfo, Color InColor, Vector3 Position )
    {
        GameObject ReturnObj = LoadUIObj("ShowText");
        if (ReturnObj == null)
        {
            return null;
        }
        if (!GamePool.IsSpawned(ReturnObj.transform))
        {
            GamePool.Regist("ShowText", ReturnObj.transform);
        }
        Transform Target = GamePool.Spawn("ShowText");
        UIWindow.AddUI(Target.gameObject);
        Target.gameObject.transform.position = Position;
        ShowText NewText = Target.GetComponent<ShowText>();
        NewText.SetInfo(InTextInfo, InColor);
        return Target.gameObject;
    }

    public static GameObject LoadUIObj(string Name)
    {
        GameObject ReturnObj = null;
        string Path = "Prefab\\UI\\" + Name;
        ReturnObj = Resources.Load(Path) as GameObject;
        return ReturnObj;
    }
}
