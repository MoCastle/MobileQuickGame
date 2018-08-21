using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfoUI : MonoBehaviour {
    Text _InfoText;
    Text InfoText
    {
        get
        {
            if(_InfoText==null)
            {
                _InfoText = GetComponent<Text>();
            }
            return _InfoText;
        }
    }

	// Use this for initialization
	// Update is called once per frame
	public void ShowInfo (string Info)
    {
        InfoText.text = Info;
    }
}
