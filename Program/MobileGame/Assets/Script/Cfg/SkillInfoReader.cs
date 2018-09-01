using System;
using System.Collections.Generic;
class SkillInfoReader
{
    static SkillInfoReader _Cfg;
    public static SkillInfoReader Cfg
    {
        get
        {
            if( _Cfg == null )
            {
                _Cfg = new SkillInfoReader();
            }
            return _Cfg;
        }
    }

    //∑¥”≥…‰
    Dictionary<string, int> Reflect;
    SkillInfoReader( )
    {
        Reflect = new Dictionary<string, int>();
        List<string[]> CfgInfo = CfgMgr.GetCfgInfo(this.ToString());
        foreach( string[] LineInfo in CfgInfo )
        {
            Reflect.Add(LineInfo[2], Convert.ToInt32( LineInfo[0]));
        }
    }

    public int GetDamPerByKey( string Name )
    {
        return GetDamPer(Reflect[Name]);
    }

    public float GetVioPerByKey( string Name)
    {
        return GetVioPer(Reflect[Name]);
    }


	public int Getid(int InLine)
	{		InLine = InLine - 1; 		return Convert.ToInt32( CfgMgr.GetData("SkillInfoReader",InLine,0));
	}
	public string GetName(int InLine)
	{		InLine = InLine - 1; 		return  CfgMgr.GetData("SkillInfoReader",InLine,1);
	}
	public string GetScrName(int InLine)
	{		InLine = InLine - 1; 		return  CfgMgr.GetData("SkillInfoReader",InLine,2);
	}
	public int GetDamPer(int InLine)
	{		InLine = InLine - 1; 		return Convert.ToInt32( CfgMgr.GetData("SkillInfoReader",InLine,3));
	}
	public int GetVioPer(int InLine)
	{		InLine = InLine - 1; 		return Convert.ToInt32( CfgMgr.GetData("SkillInfoReader",InLine,4));
	}

}