using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDir : BaseDir {

    public Transform _MainCamera;
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
        GameObject PlayerObj = CurSpawn.GenActor();
        Player = PlayerObj.GetComponent<PlayerActor>();
    }

    public void Update()
    {
        Vector3 NewPs = Player.TransCtrl.position;
        Vector3 OldPs = _MainCamera.transform.position;
        OldPs.x = NewPs.x;
        OldPs.y = NewPs.y;
        _MainCamera.transform.position = OldPs;
    }
}
