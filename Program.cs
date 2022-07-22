using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CSVTool
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (args != null && args.Length >= 1)
            {
                path = args[0];
            }
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            var files = dirInfo.GetFiles("*.csv", SearchOption.AllDirectories);
            if (files == null || files.Length <= 0)
            {
                ConsoleHelper.WriteErrorLine("当前exe目录或者目标目录没有csv文件,请重新设置目录");
            }
            else
            {
                ConsoleHelper.WriteSuccessLine("=====================================================");
                ConsoleHelper.WriteSuccessLine("== 根据CSV生成模板代码和二进制文件工具             ==");
                ConsoleHelper.WriteSuccessLine("== 说明:将exe放在csv目录中或者exe或者传入csv根目录 ==");
                ConsoleHelper.WriteSuccessLine("=====================================================");
                foreach (var file in files)
                {
                    //生成CS文件
                    bool res = CsvHelper.GenCSharpModel(file.FullName);
                    if (res)
                    {
                        ConsoleHelper.WriteSuccessLine($"{file.Name}CS模板生成成功");
                    }
                    else
                    {
                        ConsoleHelper.WriteErrorLine($"{file.Name}CS模板生成失败");
                    }

                    //生成二进制文件，如果list或者vector数据为空则写入0，要根据类型来读取csv的字段数据强转成对应的数据类型然后写入
                    //if (file.Name.Contains("avatarguideTest"))
                    //{
                    res = CsvHelper.GenBinaryData(file.FullName);
                    if (res)
                    {
                        ConsoleHelper.WriteSuccessLine($"{file.Name}二进制数据生成成功");
                    }
                    else
                    {
                        ConsoleHelper.WriteErrorLine($"{file.Name}二进制数据生成失败");
                    }

                    //读取并查询
                    //IBinarySerializable newavList = new avatarguideTestConfig();
                    //var readOK = FileManager.ReadBinaryDataFromFile(Path.Combine(path, "avatarguideTest.bytes"), ref newavList);
                    //if (readOK)
                    //{
                    //    ConsoleHelper.WriteInfoLine(newavList.ToString());
                    //    var avatarguideTest = (avatarguideTestConfig)newavList;
                    //    var obj = avatarguideTest.QueryById(2);
                    //    if (obj != null && obj.Count() > 0)
                    //        ConsoleHelper.WriteInfoLine(obj.FirstOrDefault().ToString());
                    //}
                    //else
                    //{
                    //    ConsoleHelper.WriteErrorLine("读取失败");
                    //}
                    //}
                }

                //测试读取csv数据
                //var dataTable = CsvHelper.CSV2DataTable(files[0].FullName);
                //dataTable.ToDebug(); //参数是否打印表头


                //测试序列化 反序列化
                //var vectorList = new System.Collections.Generic.List<System.Collections.Generic.List<float>>();
                //vectorList.Add(new System.Collections.Generic.List<float>() { 1.1f, 2.2f, 3.3f });
                //vectorList.Add(new System.Collections.Generic.List<float>() { 4.4f, 5.5f, 6.6f });
                //avatarguideTest av = new avatarguideTest()
                //{
                //    Id = 1,
                //    age = 23,
                //    bValue = true,
                //    gender = "gender",
                //    vector = new System.Collections.Generic.List<float>() { 1, 2, 4 }, //vector可以传null
                //    Grid = vectorList //list也可以传null
                //};
                //FileManager.WriteBinaryDataToFile(Path.Combine(path, "1.bytes"), av);
                //IBinarySerializable newav = new avatarguideTest();
                //FileManager.ReadBinaryDataFromFile(Path.Combine(path, "1.bytes"), ref newav);
                //ConsoleHelper.WriteErrorLine(newav.ToString());

                //测试自定义二进制文件
                //List<Tuple<string, string>> datas = new List<Tuple<string, string>>();
                //var idTuple = new Tuple<string, string>("int", "1");
                //datas.Add(idTuple);
                //var genderTuple = new Tuple<string, string>("string", "gender1");
                //datas.Add(genderTuple);
                //var ageTuple = new Tuple<string, string>("float", "23");
                //datas.Add(ageTuple);
                //var vValueTuple = new Tuple<string, string>("bool", "TRUE");
                //datas.Add(vValueTuple);
                //var vectorTuple = new Tuple<string, string>("vector", "[1,3,4]");
                //datas.Add(vectorTuple);
                //var listTuple = new Tuple<string, string>("list", "[[1,2,3],[3,4,5]]");
                //datas.Add(listTuple);
                //FileManager.WriteBinaryDatasToFile(Path.Combine(path, "11.bytes"), datas);

                //IBinarySerializable newav = new avatarguideTest();
                //FileManager.ReadBinaryDataFromFile(Path.Combine(path, "11.bytes"), ref newav);
                //ConsoleHelper.WriteErrorLine(newav.ToString());

                //测试序列化反序列化列表
                //avatarguideTestList avList = new avatarguideTestList();
                //var vectorList = new System.Collections.Generic.List<System.Collections.Generic.List<float>>();
                //vectorList.Add(new System.Collections.Generic.List<float>() { 1.1f, 2.2f, 3.3f });
                //vectorList.Add(new System.Collections.Generic.List<float>() { 4.4f, 5.5f, 6.6f });
                //avatarguideTest av = new avatarguideTest()
                //{
                //    Id = 1,
                //    age = 23,
                //    bValue = true,
                //    gender = "gender",
                //    vector = new System.Collections.Generic.List<float>() { 1, 2, 4 }, //vector可以传null
                //    Grid = vectorList //list也可以传null
                //};
                //avList.avatarguideTestInfos.Add(av);
                //avList.avatarguideTestInfos.Add(av);
                //FileManager.WriteBinaryDataToFile(Path.Combine(path, "2.bytes"), avList);
                //IBinarySerializable newavList = new avatarguideTestList();
                //FileManager.ReadBinaryDataFromFile(Path.Combine(path, "2.bytes"), ref newavList);
                //ConsoleHelper.WriteErrorLine(newavList.ToString());

                Console.Read();
            }
        }
    }
}
