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
