using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class StartGame : MonoBehaviour {
    private void Awake()
    {
        LogMgr.InitSet();

        Debug.Log("StartGame");
        DontDestroyOnLoad(this.gameObject);
        GameWorldTimer.StartGameSet();
        GameStartScence.StartScence();
        
    }
    private void Start()
    {
        GameCtrl GameCtrler = GameCtrl.GameCtrler;
        GameCtrler.Pool = GetComponent<SpawnPool>();
    }
    private void Update()
    {
        GameWorldTimer.Update();
        LogMgr.Update();
    }
}
