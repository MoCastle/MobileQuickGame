using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenueUI : MonoBehaviour {

    public void EnterTestGame()
    {
        GameCtrl.GameCtrler.CenceCtroler.EnterCence(new NormalScence(GameCtrl.GameCtrler.CenceCtroler, new ScenceMsg("EnterSence")));
    }
    public void StartNewGame()
    {
        GameCtrl.GameCtrler.CenceCtroler.EnterCence(new NormalScence(GameCtrl.GameCtrler.CenceCtroler, new ScenceMsg("EnterSence_1")));
    }
}
