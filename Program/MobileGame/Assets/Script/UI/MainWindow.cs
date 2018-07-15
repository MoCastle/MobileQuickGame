using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour {
    public Slider PlayerHP;
    public Slider EnemyHP;
    public Slider PlayerVIT;

    public PlayerActor Player;
    public EnemyActor Enemy;

    private void Update()
    {
        PlayerHP.value = Player.ActorPropty.PercentLife;
        PlayerVIT.value = Player.ActorPropty.PercentVIT;
        EnemyHP.value = Enemy.ActorPropty.PercentLife;

    }
}
