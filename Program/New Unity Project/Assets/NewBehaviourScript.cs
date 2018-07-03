using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
    public Text Input;
    public Button InputButton;
    public Text OutPut;
    // Use this for initialization

    public void Count( )
    {
        if (Input.text == "")
        {
            return;
        }

        float InCountNum = float.Parse(Input.text);
        float CountNum = InCountNum;
        float BackNum = CountNum * 0.001f;
        int DayNum = 1;
        int BackDay = 1;
        float TotalBakck = 0;
        while (CountNum - BackNum > 0 && BackNum > 0.0001f)
        {
            DayNum = DayNum + 1;
            TotalBakck = TotalBakck + BackNum;
            CountNum = CountNum - BackNum;
            BackNum = CountNum * 0.001f;
            int CutCountNum = (int)(CountNum * 100);
            int CutIntNum = (int)(BackNum * 100);
            CountNum = (float)CutCountNum / 100;
            BackNum = (float)CutIntNum / 100;
            if (DayNum > 99999)
            {
                BackDay = DayNum;
                break;
            }
        }
        int NewDayNum = DayNum;
        string StringTotalGet = TotalBakck.ToString( );
        string StringBackDay = BackDay.ToString();
        string StringTotalDay = DayNum.ToString();

        OutPut.text = "TotalDal:" + StringTotalDay + "\n" + "BackDay:" + StringBackDay + "\n" + "TotalGet:" + StringTotalGet + "\n";
    }
}
