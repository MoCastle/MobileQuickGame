using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CfgCodeMaker
{
    class FileData
    {
        FileInfo _DataInfo;
        public FileData( FileInfo InDataInfo )
        {
            _DataInfo = InDataInfo;
        }
        //获取文件名
        string CSFileName
        {
            get
            {
                return FileName+".cs";
            }
        }
        string _FileName;
        string FileName
        {
            get
            {
                if (_FileName == null)
                {
                    _FileName = _DataInfo.Name;
                    string InfoFileException = _DataInfo.Extension;
                    _FileName = _FileName.Replace(InfoFileException, "Reader");
                }
                return _FileName;
            }
        }

        public void CreateCode(string WritePath)
        {
            if( !_DataInfo.Exists )
            {
                Console.WriteLine("CfgFile Not Exist");
                return;
            }
            FileStream DataStream = _DataInfo.OpenRead();
            StreamReader DataReader = new StreamReader(DataStream);
            //第一行都是汉字 不取
            DataReader.ReadLine();

            //取行键名
            string[] KeyNameArr = GetLineInfo(DataReader);
            if( KeyNameArr.Length<1 )
            {
                Console.WriteLine("键名没写");
            }

            //取键类型
            string[] KeyTypeArr = GetLineInfo(DataReader);
            if( KeyTypeArr.Length< KeyNameArr.Length )
            {
                Console.WriteLine("类型定义缺失");
            }

            List<string[]> DataLineList = new List<string[]>();
            string Code = CodeMaker(KeyNameArr, KeyTypeArr);

            CodeWriter(Code, WritePath);
            
        }

        //获取行数据
        string[] GetLineInfo( StreamReader DataReader )
        {
            string LineInfo = DataReader.ReadLine();
            if( LineInfo == null )
            {
                return null;
            }
            string[] LineData = LineInfo.Split(',');
            return LineData;
        }

        //生成代码
        string CodeMaker(string[] KeyNameArr,string[] KeyTypeArr )
        {
            string Code;
            //先写名字
            StringBuilder Codes = new StringBuilder("using System;\nusing System.Collections.Generic;\n");
            Codes.Append("class ");
            Codes.Append(FileName);
            Codes.Append("\n{\n");
            //写成员
            WriteMember(Codes, KeyNameArr, KeyTypeArr);

            //写类结尾
            Codes.Append("\n}");
            return Codes.ToString();
        }

        //写成员变量
        void WriteMember( StringBuilder Builder,string[] KeyNameArr, string[] KeyTypeArr)
        {
            for( int CountColum = 0; CountColum<KeyNameArr.Length; ++CountColum )
            {
                Builder.Append("\tpublic ");
                Builder.Append(KeyTypeArr[CountColum]);
                Builder.Append(" ");
                Builder.Append("Get");
                Builder.Append(KeyNameArr[CountColum]);
                Builder.Append("(int InLine)\n\t{");
                Builder.Append("\t\tInLine = InLine - 1; ");
                //读取数据函数体
                Builder.Append("\t\treturn ");
                Builder.Append(TranseFuncCode(KeyTypeArr[CountColum], " CfgMgr.GetData(\"" + FileName + "\",InLine," + CountColum + ")"));
                Builder.Append("\n\t}\n");
            }
        }

        //返回值功能字串
        string TranseFuncCode( string KeyType, string Value )
        {
            string TransCode = null;
            switch(KeyType)
            {
                case "int":
                    TransCode = "Convert.ToInt32(" + Value + ")";
                    break;
                default:
                    TransCode = Value;
                    break;
                
            }
            TransCode = TransCode + ";";
            return TransCode;
        }


        //书写代码
        void CodeWriter( string InCode,string InPath )
        {
            string FullName = InPath + "\\" + CSFileName;
            StreamWriter CodeWriter;
            if ( !File.Exists( FullName ) )
            {
                FileStream CodeFile = File.Create(FullName);
                CodeWriter = new StreamWriter(CodeFile);
            }
            else
            {
                CodeWriter = new StreamWriter(FullName);
            }
            
            try
            {
                CodeWriter.Write(InCode);
                CodeWriter.Close();
                Console.WriteLine(FullName + " Complete");
            }
            catch( Exception ex)
            {
                Console.WriteLine(FullName+" Write Error");
                Console.WriteLine(ex.Message);
            }
            
        }
    }

    class Program
    {
        //获取配置表文件夹路径
        static string WriteFiel = "\\CodeFiel";
        static string GetWritePath
        {
            get
            {
                string ReturnPath = Directory.GetCurrentDirectory() + WriteFiel;

                if (!Directory.Exists(ReturnPath))
                {
                    Directory.CreateDirectory(ReturnPath);
                }
                return ReturnPath;
            }
        }

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
                Console.WriteLine(File.Name);
                FileData DataInfo = new FileData( File);
                DataInfo.CreateCode(GetWritePath);
            }
            Console.ReadKey();
        }
    }
}
