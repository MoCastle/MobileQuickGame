using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMgrObj : MonoBehaviour {
    public GameObject GUILayout;
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);
        UIEffectMgr.UIWindow = this;
    }
    public void AddUI( GameObject NewUI )
    {
        if(NewUI == null)
        {
            return;
        }
        NewUI.transform.SetParent(GUILayout.transform);
    }
    public void ShowMainWindow( )
    {

    }
}
