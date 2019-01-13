using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class CfgMgr {
    static string CfgPath
    {
        get
        {
            return Application.streamingAssetsPath + "\\Cfg\\";
        }
    }
    static Dictionary<string, List<string[]>> Data = new Dictionary<string, List<string[]>>();
    static public Dictionary<string, List<string[]>> DataList
    {
        get
        {
            return Data;
        }
    }

    public static void InitCfg()
    {
        //读取所有配置表
        DirectoryInfo root = new DirectoryInfo(CfgPath);
        
        FileInfo[] files = root.GetFiles();
        foreach (FileInfo File in files)
        {
            List<string[]> CfgData = GetData( File);
            if( File.Extension == ".csv" )
            {
                //将配置表数据放入列表中
                string KeyName = File.Name.Replace(File.Extension, "");
                KeyName += "Reader";
                Data.Add(KeyName, CfgData);
            }
        }
    }

    //获取数据
    public static List<string[]> GetData( FileInfo InFile )
    {
        List<string[]> CfgData = new List<string[]>();
        FileStream FileStr = InFile.OpenRead();
        StreamReader FileReader = new StreamReader(FileStr);
        //头三行是键和类型还有注释 不读
        try
        {
            FileReader.ReadLine();
            FileReader.ReadLine();
            FileReader.ReadLine();
            string LineData = FileReader.ReadLine();
            while (LineData != null)
            {
                string[] DataArr = LineData.Split(',');
                CfgData.Add(DataArr);
                LineData = FileReader.ReadLine();
            }

            return CfgData;
        }
        catch
        {
            return CfgData;
        } 
    }

    public static string GetData(string Name,int Line, int Column)
    {
        return Data[Name][Line][Column];
    }

    public static List<string[]> GetCfgInfo( string Name )
    {
        return Data[Name];
    }
 }
