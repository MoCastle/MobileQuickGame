using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsuallyInfoMgr {
    List<string> _UsuallyInfo;
    static UsuallyInfoMgr _Mgr;
    public static UsuallyInfoMgr Mgr
    {
        get
        {
            if(_Mgr == null)
            {
                _Mgr = new UsuallyInfoMgr();
            }
            return _Mgr;
        }
    }

    UsuallyInfoMgr( )
    {
        InitSkillInfo();
    }

    //读取数据表
    public void InitSkillInfo()
    {
        _UsuallyInfo = new List<string>();
        List<string[]> usualDataList = CfgReader.ReadCfg("UsualInfo");
        foreach (string[] dataStr  in usualDataList)
        {
            string dataInfo = dataStr[2];
            _UsuallyInfo.Add(dataInfo);
        }
    }
    //
    public string GetInfo( int id )
    {
        return _UsuallyInfo[id];
    }
}
