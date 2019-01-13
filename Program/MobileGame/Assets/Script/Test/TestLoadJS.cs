using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLoadJS : MonoBehaviour {
    Text txt
    {
        get
        {
            return GetComponent<Text>();
        }
    }
	// Use this for initialization
	void Start () {
        LoadText loader = new LoadText();
        txt.text = PathManager.SceneData+"\n";
        txt.text += loader.Load( PathManager.SceneData);
        txt.text += "\nEnd";
	}
}
