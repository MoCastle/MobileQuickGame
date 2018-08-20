using System;
using System.Collections.Generic;
class BookReader
{
	public int Getid(int InLine)
	{		return Convert.ToInt32( CfgMgr.GetData("BookReader",InLine,0));
	}
	public string Getname(int InLine)
	{		return  CfgMgr.GetData("BookReader",InLine,1);
	}

}