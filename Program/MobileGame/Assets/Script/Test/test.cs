using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text txt = GetComponent<Text>();
        RectTransform showTrans = ((RectTransform)(transform.transform));

       // RectTransform showTrans = ((RectTransform)(transform.parent.transform));
        txt.text = showTrans.gameObject.name + " " + showTrans.rect;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
