using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameRun;

public class GameDriver : MonoBehaviour {
    private GameEntry m_GameEntry;
    public void Init( GameEntry gameEntry )
    {
        m_GameEntry = gameEntry;
    }
	
	// Update is called once per frame
	void Update () {
        m_GameEntry.Update();

    }
}
