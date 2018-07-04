using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEffect : MonoBehaviour {
    Dictionary<string, GameObject> ObjDict = new Dictionary<string, GameObject>( );
	// Use this for initialization
	public GameObject LoadEffectObj( string Name )
    {
        GameObject ReturnObj = new GameObject();
        if( !ObjDict.TryGetValue( Name,out ReturnObj) )
        {
            string Path = "Prefab/Effect" + Name;
            ReturnObj = Resources.Load(Path,typeof( GameObject )) as GameObject;
            ObjDict.Add(Name, ReturnObj);
        }
        return ReturnObj;
    }
}
