using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lists
{
    class OrderedList<T> where T:IComparable
    {
        public Elem<T> First { get { return first; } }
        private Elem<T> first;
        public int Length { get { return length; } }
        private int length = 0;

        public void Add(T k)
        {
            if (length == 0)
            {
                first = new Elem<T>() { Info = k };
                length++;
                return;
            }
            if (Get(length-1).Less(k))
            {
                Get(length - 1).Next = new Elem<T>() { Info = k };
                length++;
                return;
            }
            Elem<T> f = first;
            if (f.More(k))
            {
                first = new Elem<T>() { Info = k, Next = f };
                length++;
                return;
                
            }
            while(f.Less(k))
            {
                if (f.Next.More(k)) break;
                f = f.Next;
            }
            f.Next = new Elem<T>() { Info = k, Next = f.Next };
            length++;
        }

        public Elem<T> Get(int k)
        {
            if (k > length - 1 || k < 0) { return null; }
            Elem<T> f = first;
            for (int i = 0; i < k; i++)
            {
                f = f.Next;
            }
            return f;
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
                length--;
                return;
            }
            Elem<T> f = first;
            for (int i = 0; i < k - 2; i++)
            {
                f = f.Next;
            }
            f.Next = f.Next.Next;
            length--;
        }

        public Elem<T> this[int index] { get { return Get(index); } set { this[index] = value; } }


        //Merge

        public static OrderedList<T> Merge(OrderedList<T> b, OrderedList<T> c)
        {
            OrderedList<T> a = new OrderedList<T>();

            Elem<T> b1 = b.first;
            Elem<T> c1 = c.first;
            while (b1 != null && c1 != null)
            {
                if (b1.Less(c1.Info)) { a.Add(b1.Info); b1 = b1.Next; }
                else { a.Add(c1.Info); c1 = c1.Next; }
            }

            if (b1 == null) { while (c1 != null) { a.Add(c1.Info); c1 = c1.Next; } }
            if (c1 == null) { while (b1 != null) { a.Add(b1.Info); b1 = b1.Next; } }


            //int j = 0;
            //int i = 0;
            //while (j < b.length && i < c.length)
            //{
            //    if (b[j].Less(c[i].Info)) { a.Add(b[j].Info); j++; }
            //    else { a.Add(c[i].Info); i++; }

            //}
            //if (j >= b.Length) for (int k = i; k < c.Length; k++) { a.Add(c[k].Info); }
            //if (i >= c.Length) for (int k = j; k < b.Length; k++) { a.Add(b[k].Info); }

            return a;
        }
    }

    public class Program2
    {
        static int Function1<T>(T param)
        {
            return param.ToString().Length;
        }

        public static void Go()
        {
            OrderedList<int> b = new OrderedList<int>();
            OrderedList<int> c = new OrderedList<int>();
            string[] strs1;
            strs1 = File.ReadAllLines("List1.txt");
            foreach (string s in strs1)
            {
                b.Add(Convert.ToInt32(s));
            }
            strs1 = File.ReadAllLines("List2.txt");
            foreach (string s in strs1)
            {
                c.Add(Convert.ToInt32(s));
            }
            for (int i = 0; i < b.Length; i++) { Console.Write($"{b.Get(i).Info}  "); }
            Console.WriteLine();
            for (int i = 0; i < c.Length; i++) { Console.Write($"{c.Get(i).Info}  "); }
            Console.ReadLine();
            Console.WriteLine();

            OrderedList<int> a = OrderedList<int>.Merge(b, c);

            for (int i = 0; i < a.Length; i++) { Console.Write($"{a.Get(i).Info}  "); }
            

        }
    }
}
