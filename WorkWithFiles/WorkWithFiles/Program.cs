using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WorkWithFiles
{
    class Program
    {
        static void Sort(string s)
        {
            string[] strs;
            string[,] Info;
            strs = File.ReadAllLines(s);
            Info = new string[2, strs.GetLength(0)];
            for (int i = 0; i < strs.GetLength(0); i++)
            {
                Info[0, i] = strs[i].Split(' ')[0];
                Info[1, i] = strs[i].Split(' ')[1];
            }
            DirectoryInfo dir = new DirectoryInfo("Directory");
            for (int i = 0; i < strs.GetLength(0); i++)
            {
                if (!Directory.Exists($"Directory/{Info[1, i]}"))
                {
                    Directory.CreateDirectory($"Directory/{Info[1, i]}");
                }

                foreach (FileInfo ff in dir.GetFiles())
                {
                    if (ff.Extension == Info[0, i])
                    {
                        ff.MoveTo($"Directory/{Info[1, i]}/{ff}");
                    }
                }
            }




        }
        static void Main(string[] args)
        {

            // Кол-во одинаковых слов в Text.txt

            string text;
            text = File.ReadAllText("Text.txt");
            List<string> a = new List<string>();
            for (int i = 0; i < text.Length; i++)
            {
                while (text[i] == ' ') i++;
                int i1 = i;
                while (text[i] != ' '&&i<text.Length-1) i++;
                if (i == text.Length - 1) i++;
                a.Add(text.Substring(i1, i - i1));
            }
            int c;
            for (int i=0; i<a.Count; i++)
            {
                c = 1;
                for (int j=i+1; j<a.Count; j++)
                {
                    if (a[i].Equals(a[j])) { c++; a.RemoveAt(j); }
                }
                Console.WriteLine($"Word {a[i]} repeats {c} times");
            }



            // Сортировка по директориям с помощью инструкции в Info.txt

            Sort("Info.txt");
            
        }
    }
}
