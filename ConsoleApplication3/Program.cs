using System;
using System.IO;
using System.Text;

namespace Consoleapplication3
{
    public class Data
    {
        public static int[] data = new int[1000];
    }
    class Program
    {
        static void Main(string[] args)
        {
            OutputAndRead.output();
            aa:
            OutputAndRead.ReadNums();
            LinkList mylist = new LinkList();
            Console.WriteLine("Please Enter N:");
            int N = int.Parse(Console.ReadLine());
            double time0 = DateTime.Now.Minute * 60000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            mylist.Sort(N);
            mylist.Print();
            mylist.Clear();
            double time1 = DateTime.Now.Minute * 60000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            double time = (time1 - time0) / 1000;
            Console.WriteLine("Timespan:{0}s", time);
            mylist.Print();
            Console.ReadLine();
            Console.WriteLine("Press Y To Again.");
            string c = Console.ReadLine();
            if (c == "Y")
            {
                goto aa;
            }
        }
    }

    /// <summary>
    /// 节点类（作用相当于数据及其指针）
    /// </summary>
    public class Node<T>
    {
        private int data;
        private Node<int> next;
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public int Data
        {
            set { data = value; }
            get { return data; }
        }
        public Node<int> Next
        {
            set { next = value; }
            get { return next; }
        }
        //节点构造函数及其重载
        public Node(int value, Node<int> n1)
        {
            data = value;
            next = n1;
        }

        public Node(Node<int> p)
        {
            next = p;
        }

        public Node(int val)
        {
            data = val;
            next = null;
        }
        public Node()
        {
            data = default(int);
            next = null;
        }
    }

    //自定义的链表功能接口
    public interface IListDS
    {
        int GetLength();
        void Clear();
        bool IsEmpty();
        void Append(int item);
        void Insert(int item, int i);
        int Delete(int i);
        int GetElement(int i);
        int Locate(int value);
        void Print();
    }

    /// <summary>
    /// 自定义链表
    /// </summary>
    public class LinkList:IListDS
    {
        private Node<int> head;
        public Node<int> Head
        {
            set { head = value; }
            get { return head; }
        }

        public void MyClear(int num)
        {
            for (int i = num - 1; i >= 0; i++) 
            {
                Node<int> p = head;
                int j = 1;
                while (!p.Data.Equals(i) && p.Next != null)
                {
                    ++j;
                    p = p.Next;
                }
                Delete(j);
            }
        }

        /// <summary>
        /// 排序（递增）
        /// </summary>
        /// <param name="num"></param>
        public void Sort(int num)
        {
            int[] data0 = Data.data;
            for (int i = 0; i < num; i++)
            {
                Node<int> node = new Node<int> { Data = data0[i], Index = i };
                if (GetLength() == 0)
                {
                    head = node;
                    continue;
                }
                int index = 0;
                for (int j = 0; j < GetLength(); j++) 
                {
                    int fda = GetLength();
                    int t = GetElement(j);
                    if (node.Data > t) 
                    {
                        index++;
                    }
                }
                if (index < GetLength())
                {
                    Insert(node.Data, index);
                }
                else
                {
                    Append(node.Data);
                }
            }
        }

        public LinkList()
        {
            //初始化链表
            head = null;
        }

        /// <summary>
        /// 获取链表大小
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            int len = 0;
            Node<int> p = head;
            while (p != null)
            {
                ++len;
                p = p.Next;
            }
            return len;
        }

        public void Clear()
        {
            //掐掉链表头
            head = null;
        }

        public bool IsEmpty()
        {
            return head == null ? true : false;
        }

        /// <summary>
        /// 向链表末尾追加项
        /// </summary>
        public void Append(int item)
        {
            Node<int> q0 = new Node<int>(item);
            Node<int> p = new Node<int>();
            if (head == null)
            {
                head = q0;
                return;
            }
            //相当于指针后移
            p = head;
            while (p.Next != null)
            {
                p = p.Next;
            }
            p.Next = q0;
        }

        /// <summary>
        /// 于指定索引处插入项
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public void Insert(int item, int index)
        {
            if (IsEmpty())
            {
                return;
            }
            if (index == 0)
            {
                head = new Node<int>(item, head);
                //注意：此操作会导致链表断裂
                return;
            }
            Node<int> p = head;
            Node<int> r = new Node<int>();
            int j = 0;
            while (p.Next != null && j < index)
            {
                ++j;
                r = p;
                p = p.Next;
            }
            if (j == index)
            {
                Node<int> q = new Node<int>(item);
                q.Next = p;
                r.Next = q;
            }
            return;
        }

        /// <summary>
        /// 于指定索引处删除项
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int Delete(int index)
        {
            if (IsEmpty() || index < 0)
            {
                return default(int);
            }

            Node<int> q = new Node<int>();
            if (index == 1)
            {
                q = head;
                head = head.Next;
                return q.Data;
            }

            Node<int> p = head;
            int j = 1;
            while (p.Next != null && j < index)
            {
                ++j;
                q = p;
                p = p.Next;
            }
            if (j == index)
            {
                q = p.Next;
                return p.Data;
            }
            else
            {
                return default(int);
            }
        }

        /// <summary>
        /// 获取指定索引的值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetElement(int index)
        {
            if (IsEmpty())
            {
                return default(int);
            }

            Node<int> p = head;
            int j = 0;
            while (p.Next != null && j < index)
            {
                ++j;
                p = p.Next;
            }
            if (j == index)
            {
                return p.Data;
            }
            else
            {
                return default(int);
            }
        }

        /// <summary>
        /// 定位，返回指定值的索引（比IndexOf慢多了）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Locate(int value)
        {
            if (IsEmpty())
            {
                return -1;
            }
            Node<int> p = head;
            int i = 1;
            while (!p.Data.Equals(value) && p.Next != null)
            {
                ++i;
                p = p.Next;
            }
            return i;
        }

        public void Print()
        {
            Node<int> p = head;
            if (p != null)
            {
                while (p != null)
                {
                    Console.WriteLine(p.Data);
                    p = p.Next;
                }
            }
            else
            {
                Console.WriteLine("LinkList is empty!");
            }
        }
    }

    class OutputAndRead
    {
        public static void output()
        {
            try
            {
                string path = "test.txt";
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        Random rand = new Random();
                        for (int i = 0; i < 1000; i++)
                        {
                            sw.WriteLine(rand.Next(0, 1000).ToString());
                        }
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                        Console.WriteLine("Save success.");
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(string.Format("Save file.[{0}]"), ee.Message);
            }
        }

        public static void ReadNums()
        {
            FileStream fs = new FileStream("test.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            for (int i = 0; i < 1000; i++)
            {
                int num = int.Parse(sr.ReadLine());
                Data.data[i] = num;
            }
            sr.Close();
            fs.Close();
        }
    }  
}
