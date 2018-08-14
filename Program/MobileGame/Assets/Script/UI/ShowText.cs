using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour {
    [Title("寿命 帧率为单位", "yellow")]
    public int Life;
    float Count;
    //消失速率
    float DisRate
    {
        get
        {
            return 1 / (float)Life;
        }
    }
    Text _TextObj;
    Text TextObj
    {
        get
        {
            if(_TextObj == null)
            {
                _TextObj = GetComponent<Text>();
            }
            return _TextObj;
        }
    }
    public void SetInfo( string InInfo, Color InColor )
    {
        //TextObj.color
        TextObj.text = InInfo;
        //TextObj.color = InColor;
    }

    public void OnEnable()
    {
        Count = 0;
        Color TextColor = TextObj.color;
        TextColor.a = 1;
        TextObj.color = TextColor;
    }
    public void Update()
    {
        if(Count < Life)
        {
            Color TextColor = TextObj.color;
            float Alpha = TextColor.a;
            Alpha = Alpha - DisRate;
            TextColor.a = Alpha;
            TextObj.color = TextColor;
            Count = Count + Time.deltaTime;
        }
        else
        {
            Death();
        }
    }
    public void Death()
    {
        GamePoolManager.Manager.Despawn(transform);
    }
}
