using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Xml;

public abstract class BaseCfgReader {
    //文件数据
    List<string[]> DataList = new List<string[]>( );
    
    //文件路径
    protected virtual String DataPath
    {
        get
        {
            return Application.streamingAssetsPath+ "\\Cfg\\";
        }
    }

    //读取单元格
	protected string ReadUnit( int InLine, int InColumn )
    {
        InLine = InLine - 1;
        if (DataList.Count>=InLine && DataList[InLine].Length> InColumn )
        {
            return DataList[InLine][InColumn];
        }
        return null;
    }

    //初始化
    public void Init( )
    {
        //读取数据
        StreamReader sr;
        sr = File.OpenText(DataPath);
        string Datas = sr.ReadToEnd();
        //把\r删掉
        string content = Datas.Replace("\r", "");
        //获取每行信息
        string[] lines = content.Split('\n');

        for(int CountLine = 0; CountLine <= lines.Length; ++CountLine)
        {
            if( CountLine>1 )
            {
                string LineData = lines[CountLine];
                string[] LineDataArr = LineData.Split(',');
                DataList.Add(LineDataArr);
            }
        }

    }
}
