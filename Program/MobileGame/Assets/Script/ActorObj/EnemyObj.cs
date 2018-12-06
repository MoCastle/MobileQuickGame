using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObj : BaseActorObj
{

    protected override void LogicAwake()
    {
        //throw new System.NotImplementedException();
        _IDLayer = 1 << LayerMask.NameToLayer("Player");
    }

}
