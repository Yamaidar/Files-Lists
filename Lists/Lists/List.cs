using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lists
{

    public class Student: IComparable
    {
        public string FIO { get; private set; }
        public int Group { get; set; }
        public int NumSTD { get; set; }
        public int Points { get; set; }

        public Student(string FIO, int NumSTD, int Group, int Points)
        {
            this.FIO = FIO;
            this.Group = Group;
            this.NumSTD = NumSTD;
            this.Points = Points;
        }

        public Student(string FIO)
        {
            this.FIO = FIO;
        }

        public override string ToString()
        {
            return String.Format($"FIO : {FIO}, personal number : {NumSTD}, group : {Group}, points : {Points}");
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class Elem<T> where T:IComparable
    {
        public T Info { get; set; }
        public Elem<T> Next { get; set; }
        public Elem<T> Prev { get; set; }

        public bool Less(T a)
        {
            return Info.CompareTo(a) < 0;
        }
        public bool More(T a)
        {
            return a.CompareTo(Info) < 0;
        }
    }

    class List<T> where T: IComparable
    {
        public Elem<T> First { get { return first; } }
        private Elem<T> first;
        public int length { get; private set; }

        public void AddFirst(T s)
        {
            Elem<T> NewFirst = new Elem<T>() { Info = s, Next = first };
            first = NewFirst;
            if (first.Next!=null)
            first.Next.Prev = first;
            length++;
        }

        public void AddLast(T s)
        {
            if (this[0] == null)
            {
                AddFirst(s);
                return;
            }
            Elem<T> f = first;
            while (f.Next!=null)
            {
                f = f.Next;
            }
            f.Next = new Elem<T>() { Info = s, Prev = f };
            length++;
        }

        public Elem<T> Get(int k)
        {
            if (k > length - 1||k<0) { return null; }
            Elem<T> f = first;
            for (int i = 0; i < k; i++)
            {
                f = f.Next;
            }
            return f;
        }

        
        public void AddAfterk(T s, int k)
        {
            if (k < 0 || k > length - 1)
            {
                Console.WriteLine("k-го элемента не существует");
                return;
            }
            Elem<T> f  = first;
            for (int i = 0; i < k; i++)
            {
                f = f.Next;
            }
            f.Next = new Elem<T>() { Info = s, Next = f.Next, Prev = f };
            length++;
        }

        public void AddBeforek(T s, int k)
        {
            if (k < 0 || k > length - 1)
            {
                Console.WriteLine("k-го элемента не существует");
                return;
            }
            if (k == 0) AddFirst(s);
            AddAfterk(s, k - 1);
            length++;
        }

        public void Delete(int k)
        {
            if (k < 0 || k > length - 1)
            {
                Console.WriteLine("k-го элемента не существует");
                return;
            }
            if (k == 0)
            {
                first = first.Next;
                first.Prev = null;
                return;
            }
            Elem<T> f = first;

            for (int i = 0; i < k-2; i++)
            {
                f = f.Next;
            }

            f.Next = f.Next.Next;
            f = f.Next;
            f.Prev = f.Prev.Prev;
            length--;
        }

        public void DeleteAll()
        {
            first = null;
            length = 0;
        }

        public Elem<T> this[int index] { get { return Get(index); } set { this[index] = value; } }
    }

    class Program1
    {

        static void Main(string[] args)
        {
            string[] strs;
            List<Student> a = new List<Student>();
            strs = File.ReadAllLines("StudentsInfo.txt");
            foreach (string s in strs)
            {
                int i = 0;
                while (s[i] != ' ') i++;
                int j = i + 1;
                while (s[j] != ' ') j++;
                int k = j + 1;
                while (s[k] != ' ') k++;
                a.AddLast(new Student(s.Substring(0, i), Convert.ToInt32(s.Substring(i + 1, j - i - 1)), Convert.ToInt32(s.Substring(j + 1, k - j - 1)), Convert.ToInt32(s.Substring(k))));
            }
            for (int i = 0; i < a.length; i++) { Console.WriteLine(a.Get(i).Info); }
            Console.ReadLine();

            //Проверка методов списка

            a[1].Info = new Student("Aidar", 04, 2, 23);
            Console.WriteLine(a[1].Info);
            Console.ReadLine();
            a.Delete(2);
            Console.Clear();
            for (int i = 0; i < a.length; i++) { Console.WriteLine(a.Get(i).Info); }
            Console.ReadLine();
            Console.Clear();
            a.DeleteAll();
            for (int i = 0; i < a.length; i++) { Console.WriteLine(a.Get(i).Info); }
            Console.ReadLine();
            Program2.Go();
        }
    }
}
