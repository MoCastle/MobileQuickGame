using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour {

    
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
        PauseEvent();
        Application.LoadLevel("MenuSence");
    }
    //返回主菜单
    public void Restart()
    {
        PauseEvent();
        Application.LoadLevel("EnterSence");
    }

    #endregion

    public Slider PlayerHP;
    public Slider PlayerVIT;
    public DebugInfoUI DebugText;
    public Propty PlayerPropty
    {
        get
        {
            return PlayerDelegate.GetPropty();
        }
    }

    HeadInRoundArr<NormInput> InputRemember = new HeadInRoundArr<NormInput>(10);
    private void Start()
    {
        PlayerCtrl.AddInputEvent(CountInputInfo);
    }
    private void Update()
    {
        if( PlayerPropty!= null )
        {
            PlayerHP.value = PlayerPropty.PercentLife;
            PlayerVIT.value = PlayerPropty.PercentVIT;
            PrintDebugInfo();
        }
        
    }

    private void PrintDebugInfo()
    {
        StringBuilder OutPutStr = new StringBuilder();
        HandInputInfo(OutPutStr);
        AnimInfo(OutPutStr);
        DebugText.ShowInfo(OutPutStr.ToString());
    }
    private void CountInputInfo( NormInput InputInfo )
    {
        InputRemember.AddInfo(InputInfo);
    }

    //输入信息
    private void HandInputInfo(StringBuilder PutInfo)
    {
        PutInfo.Append("输入信息\n");
        int ArrLength = 0;
        foreach ( NormInput Input in InputRemember )
        {
            if(Input.IsLegal)
            {
                PutInfo.Append("手势:");
                PutInfo.Append(Input.Gesture.ToString());
                PutInfo.Append(" 百比");
                PutInfo.Append(((int)(Input.InputInfo.Percent*100)).ToString());
                PutInfo.Append(" 方向");
                PutInfo.Append(Input.Dir.ToString());
                PutInfo.Append(" 时间");
                PutInfo.Append(Input.LifeTime.ToString());
                PutInfo.Append("\n");
            }
            else
            {
                PutInfo.Append("异常输入\n");
            }
            ArrLength = ArrLength + 1;
        }

        for( int LeftLength = ArrLength; LeftLength<9;++LeftLength )
        {
            PutInfo.Append("无输入\n");
        }
        PutInfo.Append("\n");
    }
    //缓存队列
    private void AnimInfo(StringBuilder PutInfo)
    {
        PutInfo.Append("缓存队列\n");
        PutInfo.Append(PlayerCtrl.InputRoundArr.HeadInfo.Gesture.ToString());
        PutInfo.Append(PlayerCtrl.InputRoundArr.TailInfo.Gesture.ToString());
        PutInfo.Append("\n");
    }
}
