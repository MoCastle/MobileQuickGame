using System;
using System.Collections.Generic;
class NpcProptyReader
{
	public int Getid(int InLine)
	{		InLine = InLine - 1; 		return Convert.ToInt32( CfgMgr.GetData("NpcProptyReader",InLine,0));
	}
	public string Getname(int InLine)
	{		InLine = InLine - 1; 		return  CfgMgr.GetData("NpcProptyReader",InLine,1);
	}

}