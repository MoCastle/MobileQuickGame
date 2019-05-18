using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class MainWindow : BaseUI {

    
    bool _IfPaul = false;
    //打开菜单
    #region
    public OptionMenue Menue;
    //暂停事件
    public void PauseEvent()
    {
        GameCtrl.GameCtrler.SwitchPause();
        Menue.gameObject.active = GameCtrl.GameCtrler.IsPaused;
    }
    //返回主菜单
    public void ReturnToMain()
    {
        //PauseEvent();
        //Application.LoadLevel("MenuSence");
    }
    //返回主菜单
    public void Restart()
    {
        //PauseEvent();
        //Application.LoadLevel("EnterSence");
    }

    #endregion

    public Slider PlayerHP;
   // public Slider PlayerVIT;
    public DebugInfoUI DebugText;
    public Propty PlayerPropty
    {
        get
        {
            return PlayerDelegate.GetPropty();
        }
    }

    HeadInRoundArr<InputInfo> InputRemember = new HeadInRoundArr<InputInfo>(10);
    private void Start()
    {
        PlayerCtrl.AddInputEvent(CountInputInfo);
    }
    private void Update()
    {
        //PlayerHP.value = PlayerPropty.PercentLife;
    }

    private void CountInputInfo( InputInfo InputInfo )
    {
        InputRemember.AddInfo(InputInfo);
    }
}
