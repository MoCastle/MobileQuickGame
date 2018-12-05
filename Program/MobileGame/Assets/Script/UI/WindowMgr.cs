using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMgr {

    public static Transform UIWindow;
    //Dictionary<string, GameObject> UIDict = new Dictionary<string, GameObject>();

    static GameObject GenWindow( string WindowName )
    {
        GameObject NewObj = null;
        string Path = "Prefab\\UI\\" + WindowName;
        NewObj = Resources.Load(Path) as GameObject;
        //UIDict.Add( WindowName, NewObj )
        return NewObj;
    }
    public static void ResetUI()
    {
        /*
        Transform FirstArray = UIWindow.GetChild(0);
        for( int UICount = FirstArray.childCount; UICount >0; --UICount )
        {
            //GameObject.Destroy(FirstArray.GetChild(UICount - 1).gameObject);
        }*/

    }
    //显示普通窗口
    public static void ShowMainWindow()
    {
        GenWindow("ScrollArea");
        GenWindow("MainWindow");
        
    }

}
