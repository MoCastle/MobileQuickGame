using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

//游戏总控制器 负责游戏核心逻辑
public class GameDriver : MonoBehaviour
{
    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
    }
    private void Update()
    {
        LogMgr.Update();
    }
}
