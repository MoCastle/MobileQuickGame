using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadText {
    public string Load(string path)
    {
        string newString = null;
        if(Application.platform == RuntimePlatform.Android)
        {
            WWW www = new WWW(path);
            while(!www.isDone)
            {
            }
            newString = www.text;
        }
        else
        {
            StreamReader reader = new StreamReader(path);
            newString = reader.ReadToEnd();
        }
        return newString;
    }
}

public static class LoaderFile
{
    public static byte[] LoadBytes(string path,bool useWWW = false)
    {
        byte[] newBytes = null;
        if (useWWW)
        {
            WWW www = new WWW(path);
            while (!www.isDone)
            {
            }
            newBytes = www.bytes;
        }
        else
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            //获取文件大小
            long size = fs.Length;
            newBytes = new byte[size];

            //将文件读到byte数组中
            fs.Read(newBytes, 0, newBytes.Length);
            fs.Close();
            fs.Dispose();
        }
        return newBytes;
    }
}

