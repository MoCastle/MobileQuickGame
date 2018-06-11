using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameWorldTimer{
    static GameWorldTimer _WorldTimerObj;

    bool GameStart = false;//是否游戏已开始
    bool GameInBattle = false;//是否进行战斗

    public event TimeEvent GameStartEvent;
    public event TimeEvent GameInBattleEvent;
    public delegate void TimeEvent( );
    //public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e) where;
    //对外接口
    public static GameWorldTimer WorldTimer
    {
        get
        {
            if( _WorldTimerObj == null )
            {
                _WorldTimerObj = new GameWorldTimer( );
            }
            return _WorldTimerObj;
        }
    }
    public void StartGameSet( bool StartGame = true )
    {
        GameStart = StartGame;
    }

    //内部功能
	// Use this for initialization
	// Update is called once per frame
	public void Update () {
        if ( GameStart )
        {
            GameUpdateFunc();
            if (GameInBattle)
            {
                GameInBattleFunc( );
            }
        }		
	}
    void GameUpdateFunc()
    {
        if( GameStartEvent != null )
        {
            GameStartEvent();
        }
    }
    void GameInBattleFunc( )
    {
        if (GameInBattleEvent != null)
        {
            GameStartEvent();
        }
    }
}
