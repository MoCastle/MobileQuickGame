using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCence : BaseCence {
    //当前场景导演
    BaseDir Director;
    public EnterCence( CenceCtrl InCenceCtrl ):base( InCenceCtrl) { }
    public override void Start()
    {
        GameObject Director = GameObject.Find("Director");
    }
    public override void Update()
    {
        
    }
}
