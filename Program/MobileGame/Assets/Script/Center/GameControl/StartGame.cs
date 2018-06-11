using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
    GameWorldTimer _WorldTimer;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _WorldTimer = GameWorldTimer.WorldTimer;
        _WorldTimer.StartGameSet();
        GameStartScence.StartScence();
    }
    private void Update()
    {
        _WorldTimer.Update();
    }
}
