using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class CfgReader
{
    //文件路径
    static string DataPath
    {
        get
        {
            /*
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    return Application.streamingAssetsPath + "/Cfg/";
                    break;
                case RuntimePlatform.Android:
                    return "jar:file://" + Application.dataPath + "!/assets/" + "/Cfg/"; ;
                    break;
            }
            return Application.streamingAssetsPath + "/Cfg/";
            */
            return "Cfg\\";
        }
    }

    //初始化
    public static List<string[]> ReadCfg(string fileName)
    {
        //读取数据
        StreamReader sr;
        List<string[]> dataList = new List<string[]>();
        /*
        sr = File.OpenText(DataPath + fileName);
        string Datas = sr.ReadToEnd();
        */
        string path = DataPath + fileName;
        string Datas = (Resources.Load(path) as TextAsset).text;
        //sr.Close();
        //把\r删掉
        string content = Datas.Replace("\r", "");
        //获取每行信息
        string[] lines = content.Split('\n');
        int ColumNum = lines[0].Split(',').Length;

        for (int CountLine = 0; CountLine < lines.Length; ++CountLine)
        {
            if (CountLine > 1)
            {
                string lineData = lines[CountLine];
                string[] lineDataArr = new string[ColumNum];
                if (lineData != "")
                {
                    string[] cfgLineDataArr = lineData.Split(',');
                    for(int countColumNum = 0; countColumNum < ColumNum; ++countColumNum )
                    {
                        if(countColumNum< cfgLineDataArr.Length)
                        {
                            lineDataArr[countColumNum] = cfgLineDataArr[countColumNum];
                        }else
                        {
                            lineDataArr[countColumNum] = "";
                        }
                        
                    }
                    dataList.Add(lineDataArr);
                }
            }
        }
        return dataList;
    }
}
