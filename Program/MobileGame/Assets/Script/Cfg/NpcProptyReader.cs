using System;
using System.Collections.Generic;
class NpcProptyReader
{
    static NpcProptyReader _Cfg;
    public static NpcProptyReader Cfg
    {
        get
        {
            if (_Cfg == null)
            {
                _Cfg = new NpcProptyReader();
            }
            return _Cfg;
        }
    }
    public int GetId(int InLine)
	{		InLine = InLine - 1;
        return Convert.ToInt32( CfgMgr.GetData("NpcProptyReader",InLine,0));
	}
	public string GetName(int InLine)
	{		InLine = InLine - 1;
        return  CfgMgr.GetData("NpcProptyReader",InLine,1);
	}
    public float GetMoveSpeed(int InLine)
    {
        InLine = InLine - 1;
        return ((float)Convert.ToInt32(CfgMgr.GetData("NpcProptyReader", InLine, 2)))/1000;
    }
    public int GetHealthy(int InLine)
    {
        InLine = InLine - 1;
        return Convert.ToInt32(CfgMgr.GetData("NpcProptyReader", InLine, 3));
    }
    public int GetAttack(int InLine)
    {
        InLine = InLine - 1;
        return Convert.ToInt32(CfgMgr.GetData("NpcProptyReader", InLine, 4));
    }
    public int GetVirtical(int InLine)
    {
        InLine = InLine - 1;
        return Convert.ToInt32(CfgMgr.GetData("NpcProptyReader", InLine, 5));
    }
    public float GetHitBackSpeed(int InLine)
    {
        InLine = InLine - 1;
        return ((float)Convert.ToInt32(CfgMgr.GetData("NpcProptyReader", InLine, 6))) / 1000;
    }
}