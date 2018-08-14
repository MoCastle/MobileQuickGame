using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : BaseSpawn
{
    public GameObject Player;
    public override GameObject GenActor()
    {
        Vector3 NewPs = this.transform.position;
        NewPs.z = 0;
        GameObject NewPlayer = Instantiate(Player, NewPs, this.transform.rotation);
        return NewPlayer;
    }
    
}
