using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadEffect {
    //Dictionary<string, GameObject> ObjDict = new Dictionary<string, GameObject>( );
	// Use this for initialization
	public static GameObject LoadEffectObj( string Name )
    {
        /*GameObject ReturnObj = new GameObject();
        if( !ObjDict.TryGetValue( Name,out ReturnObj) )
        {
            string Path = "Prefab/Effect" + Name;
            ReturnObj = Resources.Load(Path,typeof( GameObject )) as GameObject;
            ObjDict.Add(Name, ReturnObj);
        }*/
        string Path = "Prefab/Effect" + Name;
        GameObject ReturnObj = Resources.Load(Path, typeof(GameObject)) as GameObject;
        return ReturnObj;
    }
}
