using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct TestBtn
{
    public Button Btn;
    public Text TheText;
    public GameObject Obj;
    public void Swtich()
    {
        if(Obj.activeSelf)
        {
            TheText.text = "Off";
            Obj.SetActive(false);
        }else
        {
            TheText.text = "On";
            Obj.SetActive(true);
        }
    }
    public void SetTex()
    {
        if(Obj.activeSelf)
        {
            TheText.text = "Off";
        }else
        {
            TheText.text = "true";
        }
    }
}
public class TestUI : MonoBehaviour {
    public TestBtn[] BtnArr;
	// Use this for initialization
	void Start () {
        for(int idx = 0; idx< BtnArr.Length;++idx)
        {
            TestBtn btn = BtnArr[idx];
            btn.Btn.onClick.AddListener(btn.Swtich);
            btn.SetTex();
        }
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
