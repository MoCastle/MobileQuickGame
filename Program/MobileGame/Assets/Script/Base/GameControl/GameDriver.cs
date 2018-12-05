using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

//游戏总控制器 负责游戏核心逻辑
public class GameDriver : MonoBehaviour
{

    public GameObject PlayerSample;
    private void Awake()
    {
        try
        {
            CfgMgr.InitCfg();
        }
        catch
        {

        }

        LogMgr.InitSet();

        //UI用
        WindowMgr.UIWindow = transform.FindChild("UICanvas");

        GameCtrl GameCtrler = GameCtrl.GameCtrler;
        GameCtrler.PlayerSample = PlayerSample;
        GameCtrler.Pool = GetComponent<SpawnPool>();

        Debug.Log("StartGame");
        DontDestroyOnLoad(this.gameObject);
        //GameWorldTimer.StartGameSet();

        
    }
    private void Start()
    {
        Application.LoadLevel("EnterSence");
    }
    private void Update()
    {
        //GameWorldTimer.Update();
        LogMgr.Update();
        //GameCtrl.GameCtrler.Update();
    }
}
