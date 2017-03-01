using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkWithFiles;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class TestSort
    {
        // Тест - выполнить - все тесты
        [TestMethod]
        public void TestMethodSort()
        {
            string[] strs;
            string[,] Info;
            strs = File.ReadAllLines("C:/Users/iaman/Documents/visual studio 2015/Projects/WorkWithFiles/WorkWithFiles/bin/Debug/Info.txt");
            Info = new string[2, strs.GetLength(0)];
            for (int i = 0; i < strs.GetLength(0); i++)
            {
                Info[0, i] = strs[i].Split(' ')[0];
                Info[1, i] = strs[i].Split(' ')[1];
            }

            for (int i = 0; i < Info.GetLength(0); i++)
            {
                DirectoryInfo dir = new DirectoryInfo($"C:/Users/iaman/Documents/visual studio 2015/Projects/WorkWithFiles/WorkWithFiles/bin/Debug/Directory/{Info[1,i]}");
                foreach (FileInfo f in dir.GetFiles())
                {
                    if (f.Extension != Info[0, i]) Assert.Fail();
                }
            }
        }
    }
}
