using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcProptyCfg : BaseCfgReader {
    static NpcProptyCfg _Reader;
    public static NpcProptyCfg Reader
    {
        get
        {
            if( _Reader == null )
            {
                _Reader = new NpcProptyCfg();
            }
            return _Reader;
        }
    }

    NpcProptyCfg() { }

    protected override string DataPath
    {
        get
        {
            string Path = base.DataPath;
            Path = Path + "NpcPropty.csv";
            return Path;
        }
    }
}
