using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
abstract class InfoClass
{
    abstract public string DebugInfo();
}

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

    HeadInRoundArr<NormInput> InputRemember = new HeadInRoundArr<NormInput>(8);

    private void Update()
    {
        PlayerHP.value = Player.ActorPropty.PercentLife;
        PlayerVIT.value = Player.ActorPropty.PercentVIT;
    }

    private void PrintDebugInfo()
    {
        StringBuilder OutPutStr = new StringBuilder();
    }
    private void HandInputInfo()
    {

    }
}
