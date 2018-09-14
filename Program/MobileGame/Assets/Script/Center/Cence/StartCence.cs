using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCence : BaseCence {
    public StartCence( CenceCtrl InCenceCtrl) :base( InCenceCtrl, new ScenceMsg("StartCence") )
    {

    }
    public override void Update()
    {
        CenceCtrl.EnterCence(new NormalScence(CenceCtrl, new ScenceMsg( "MenuSence")));
    }
}
