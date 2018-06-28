using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : BaseAI {
    public GuardAI( EnemyActor Actor ):base(Actor)  { }
    // Use this for initialization
    // Update is called once per frame
    public override void Update()
    {
        Actor.CheckGetPlayer();
    }
}
