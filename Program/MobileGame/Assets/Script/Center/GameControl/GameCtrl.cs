using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class GameCtrl
{
    static GameCtrl _GameCtrler;
    static public GameCtrl GameCtrler
    {
        get
        {
            if (_GameCtrler == null)
            {
                _GameCtrler = new GameCtrl();
            }
            return _GameCtrler;
        }
    }
    GameCtrl()
    { }
    SpawnPool _Pool;
    public SpawnPool Pool
    {
        get
        {
            return _Pool;
        }
        set
        {
            if (_Pool == null)
            {
                _Pool = value;
            }
        }
    }
}
