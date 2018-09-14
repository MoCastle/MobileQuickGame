using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : BaseSpawn
{
    public GameObject Player
    {
        get
        {
            return GameCtrl.GameCtrler.PlayerSample;
        }
    }
    public override BaseActor GenActor()
    {
        Vector3 NewPs = this.transform.position;
        NewPs.z = 0;
        GameObject NewPlayer = Instantiate(Player, NewPs, this.transform.rotation);
        PlayerActor Actor = NewPlayer.GetComponent<PlayerActor>();
        if( PlayerDelegate.GetPropty( ) == null )
        {
            PlayerDelegate.SetPropty(Actor.ActorPropty);
        }else
        {
            Actor.ActorPropty = PlayerDelegate.GetPropty();
        }
        Actor.ActorPropty.Init();
        return Actor;
    }
}
