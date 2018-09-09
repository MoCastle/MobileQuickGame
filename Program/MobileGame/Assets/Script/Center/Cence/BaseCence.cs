using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCence {
    CenceCtrl CenceCtrl;
    public BaseCence( CenceCtrl InCenceCtrl )
    {
        CenceCtrl = InCenceCtrl;
    }
    //始
    public virtual void Start( )
    {

    }
    //末
    public virtual void End( )
    {

    }
    //更新
    public virtual void Update( )
    {
    }
}
