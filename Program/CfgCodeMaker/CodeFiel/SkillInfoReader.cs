using System;
using System.Collections.Generic;
class SkillInfoReader
{
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