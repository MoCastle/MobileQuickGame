using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadEffect {
    static Dictionary<string, GameObject> ObjDict = new Dictionary<string, GameObject>( );
	// Use this for initialization
	public static GameObject LoadEffectObj( string Name )
    {
        GameObject ReturnObj = null;
        bool CheckGetValue = ObjDict.TryGetValue(Name, out ReturnObj);
        if ( !CheckGetValue)
        {
            string Path = "Prefab\\Effect\\" + Name;
            ReturnObj = Resources.Load(Path) as GameObject;
            if( ReturnObj != null )
            {
                ObjDict.Add(Name, ReturnObj);
            }
        }
        return ReturnObj;
    }
}
