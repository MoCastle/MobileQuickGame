using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour {
    public Slider PlayerHP;
    public Slider PlayerVIT;
    public DebugInfoUI DebugText;
    public PlayerActor Player
    {
        get
        {
            return CenceMgr.Mgr.CurSenceDir.Player;
        }
    }
    public EnemyActor Enemy;

    HeadInRoundArr<NormInput> InputRemember = new HeadInRoundArr<NormInput>(10);
    private void Start()
    {
        PlayerCtrl.AddInputEvent(CountInputInfo);
    }
    private void Update()
    {
        PlayerHP.value = Player.ActorPropty.PercentLife;
        PlayerVIT.value = Player.ActorPropty.PercentVIT;
        PrintDebugInfo();
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
