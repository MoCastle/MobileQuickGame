using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenceMgr {
    //当前场景导演
    //public BaseDir CurSenceDir;
    static CenceMgr _Mgr;
    public static CenceMgr Mgr
    {
        get
        {
            if( _Mgr == null )
            {
                _Mgr = new CenceMgr();
            }
            return _Mgr;
        }
    }
    CenceMgr()
    {
    }
}
