using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDirector : BaseDir
{
    
    public Transform _MainCamera;
    public PlayerSpawn[] PlayerSpawnList;
    public PlayerSpawn CurSpawn;
    int CurSpawnID;

    public void Start()
    {
        GameWorldTimer.Continue();
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
