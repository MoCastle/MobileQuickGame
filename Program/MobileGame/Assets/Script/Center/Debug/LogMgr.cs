using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
//静态工具类 用于日志相关功能管理
public static class LogMgr {
    public static string Path
    {
        get
        {
            switch( Application.platform )
            {
                case RuntimePlatform.WindowsEditor:
                    return Application.dataPath + "/outLog.txt";
                    break;
                case RuntimePlatform.Android:
                    return Application.persistentDataPath + "/outLog.txt";
                    break;
            }
            return Application.dataPath + "/outLog.txt";
        }
        set { }
    }
    private static List<string> mWrite = new List<string>();
    static string my_String = "";
    public static string LogString
    {
        get
        {
            return my_String;
        }
        set
        {
            my_String = my_String + value;
        }
    }
    public static void InitSet( )
    {
        Application.RegisterLogCallback( PrintLog );
        //每次启动客户端删除之前保存的Log
        if (System.IO.File.Exists(Path))
        {
            File.Delete(Path);
        }
        using (StreamWriter writer = new StreamWriter(Path, true, Encoding.UTF8))
        {
            writer.WriteLine("Log System Start\n");
            LogString = "Log System Start\n";
            LogString = "SystemInfo: Platform  " + Application.platform + "\n";
            LogString = "ParamInfo:\n RuntimePlatform.WindowsEditor: " + RuntimePlatform.WindowsEditor +"| ";
            LogString = "Application.persistentDataPath" + RuntimePlatform.Android + "\n";
        }
    }
    public static void Update( )
    {
        if ( mWrite.Count > 0 )
        {
            string[] temp = mWrite.ToArray();
            foreach (string t in temp)
            {
                
                using (StreamWriter writer = new StreamWriter(Path, true, Encoding.UTF8))
                {
                    LogString = "Writer Start Work:" + t + "\n";
                    writer.WriteLine(t);
                }
                mWrite.Remove(t);
            }
        }
    }
    public static void PrintLog(string condition, string stackTrace, LogType type)
    {
        if( type == LogType.Error || type == LogType.Exception )
        {
            mWrite.Add(condition + "\n" + stackTrace );
        }else
        {
            mWrite.Add(condition);
        }
    }
}
