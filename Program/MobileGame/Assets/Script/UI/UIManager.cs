
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager {
    static UIManager _UIMgr;
    static UIManager UIMgr
    {
        get
        {
            if( _UIMgr == null)
            {
                _UIMgr = new UIManager();
            }
            return _UIMgr;
        }

    }

    UIManager()
    { }
}
