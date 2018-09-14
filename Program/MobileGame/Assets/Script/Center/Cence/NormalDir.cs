using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDir : BaseDir {
    public CameraControler CameraCtrler;
    public PlayerSpawn[] PlayerSpawnList;
    int CurSpawnID = 0;
    // Use this for initialization
    public void Start()
    {
        GameWorldTimer.Continue();
        CameraCtrler = new CameraControler(this);
    }

    public void GenPlayer()
    {
        PlayerSpawn CurSpawn = PlayerSpawnList[CurSpawnID];
        BaseActor PlayerObj = CurSpawn.GenActor();
        Player = PlayerObj as PlayerActor;
    }

    public void Update()
    {
        CameraCtrler.Update();
    }
}
