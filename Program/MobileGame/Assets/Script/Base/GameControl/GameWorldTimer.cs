using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class GameWorldTimer{
    static bool GameStart = false;//是否游戏已开始
    static bool GameInBattle = false;//是否进行战斗

    public static event TimeEvent GameStartEvent;
    public static event TimeEvent GameInBattleEvent;
    public delegate void TimeEvent( );
    //public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e) where;
    //对外接口

    public static void StartGameSet( bool StartGame = true )
    {
        GameStart = StartGame;
    }

    //内部功能
	// Use this for initialization
	// Update is called once per frame
	public static void Update () {
        if ( GameStart )
        {
            GameUpdateFunc();
            if (GameInBattle)
            {
                GameInBattleFunc( );
            }
        }		
	}
    static void GameUpdateFunc()
    {
        if( GameStartEvent != null )
        {
            GameStartEvent();
        }
    }
    static void GameInBattleFunc( )
    {
        if (GameInBattleEvent != null)
        {
            GameInBattleEvent();
        }
    }

    //对外接口
    public static void Pull( )
    {
        GameInBattle = false;
    }
    public static void Continue( )
    {
        GameInBattle = true;
    }
}
