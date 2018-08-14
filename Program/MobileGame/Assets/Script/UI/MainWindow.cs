using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour {
    public Slider PlayerHP;
    public Slider PlayerVIT;

    public PlayerActor Player
    {
        get
        {
            return CenceMgr.Mgr.CurSenceDir.Player;
        }
    }
    public EnemyActor Enemy;

    private void Update()
    {
        PlayerHP.value = Player.ActorPropty.PercentLife;
        PlayerVIT.value = Player.ActorPropty.PercentVIT;
    }
}
