using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

//游戏总控制器 负责游戏核心逻辑
public class GameDriver : MonoBehaviour
{


    public GameObject SkillObj;
    public GameObject Test;
    private void Awake()
    {
        try
        {
            CfgMgr.InitCfg();
            NpcProptyReader Npc = new NpcProptyReader();
            Npc.Getname(0);
        }
        catch
        {

        }

        LogMgr.InitSet();

        GameCtrl GameCtrler = GameCtrl.GameCtrler;
        GameCtrler.Pool = GetComponent<SpawnPool>();

        Debug.Log("StartGame");
        DontDestroyOnLoad(this.gameObject);
        SkillManager.Obj = SkillObj.GetComponent<SkillObj>();
        GameWorldTimer.StartGameSet();
        GameStartScence.StartScence();

    }
    private void Start()
    {

    }
    private void Update()
    {
        GameWorldTimer.Update();
        LogMgr.Update();
    }
}
