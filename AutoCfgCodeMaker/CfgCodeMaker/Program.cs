using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CfgCodeMaker
{
    class Program
    {
        //获取配置表文件夹路径
        static string ReadFiel = "\\CfgFiel";
        static string GetReadPath
        {
            get
            {
                string ReturnPath = Directory.GetCurrentDirectory() + ReadFiel;

                if (!Directory.Exists(ReturnPath))
                {
                    Directory.CreateDirectory(ReturnPath);
                }
                return ReturnPath;
            }
        }
        static void Main(string[] args)
        {
            //读取路径下的所有文件
            DirectoryInfo root = new DirectoryInfo(GetReadPath);
            FileInfo[] files = root.GetFiles();
            foreach( FileInfo File in files )
            {
                Console.WriteLine(File.FullName);
            }
            Console.ReadKey();
        }
    }
}
