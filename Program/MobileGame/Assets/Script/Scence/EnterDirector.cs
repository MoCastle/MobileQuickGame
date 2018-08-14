using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDirector : SenceDir
{
    
    public Transform _MainCamera;
    public PlayerSpawn[] PlayerSpawnList;
    public PlayerSpawn CurSpawn;
    protected override void LogicAwake()
    {
        if(PlayerSpawnList!=null && PlayerSpawnList.Length > 0)
        {
            CurSpawn = PlayerSpawnList[0];
        }
    }
    // Use this for initialization
    void Start ()
    {
        GameWorldTimer.Continue();
        GameObject PlayerObj = CurSpawn.GenActor();
        Player = PlayerObj.GetComponent<PlayerActor>();
    }
    private void Update()
    {
        Vector3 NewPs = Player.TransCtrl.position;
        Vector3 OldPs = _MainCamera.transform.position;
        OldPs.x = NewPs.x;
        OldPs.y = NewPs.y;
        _MainCamera.transform.position = OldPs;
    }

}
