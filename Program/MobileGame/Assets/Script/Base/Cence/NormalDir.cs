using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDir : BaseDir {
    
    public PlayerSpawn[] PlayerSpawnList;
    int CurSpawnID = 0;
    // Use this for initialization
    public void Start()
    {
        GameWorldTimer.Continue();
    }

    public void GenPlayer()
    {
        PlayerSpawn CurSpawn = PlayerSpawnList[CurSpawnID];
        BaseActor PlayerObj = CurSpawn.GenActor();
        Player = PlayerObj as PlayerActor;
    }

    
}
