using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class StartGame : MonoBehaviour {
    public GameObject SkillObj;
    public GameObject Test;
    private void Awake()
    {
        try
        {
            CfgMgr.Init();
        }
        catch
        {

        }
        
        LogMgr.InitSet();

        Debug.Log("StartGame");
        DontDestroyOnLoad(this.gameObject);
        SkillManager.Obj = SkillObj.GetComponent<SkillObj>();
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
