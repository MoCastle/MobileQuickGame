using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        GameWorldTimer.StartGameSet();
        GameStartScence.StartScence();
    }
    private void Update()
    {
        GameWorldTimer.Update();
    }
}
