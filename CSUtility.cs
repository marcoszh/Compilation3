using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace MyCompilation
{
    //终结符使用的类
    class CTChar
    {
        public String value;        //值
        public ArrayList First;     //其FIRST集
        public CTChar(string value)
        {
            First = new ArrayList();
            this.value = value;
        }
    }

    //非终结符使用的类
    class CNTChar
    {
        public String value;        //值
        public ArrayList First;     //其FIRST集
        public ArrayList Follow;    //其FOLLOW集
        public ArrayList Sync;      //同步记号的集合
        public CNTChar(string value)
        {
            First = new ArrayList();
            Follow = new ArrayList();
            Sync = new ArrayList();
            this.value = value;
        }

        public void generateSync()
        {
            //将Follow集加入
            foreach (string temp in Follow)
                Sync.Add(temp);

            //将FIRST集中未出现的也加入
            foreach (string temp in First)
            {
                if (!Sync.Contains(temp))
                    Sync.Add(temp);
            }
        }
    }

    //产生式使用的类
    class Production
    {
        public int no;
        public string Left;    //产生式的左部
        public ArrayList Right;   //产生式的右部
        public ArrayList Select;    //产生式的SELECT集

        public Production(int no,String Left, ArrayList Right)
        {
            this.no = no;
            this.Left = Left;
            this.Right = Right;
            Select = new ArrayList();
        }
    }


    class CSUtility
    {
        //从文件读取文法产生式
        public static ArrayList readProductionFile(String path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                ArrayList productions = new ArrayList();
                string line = "";
                string left = "";
                string right = "";
                line = sr.ReadLine();
                int i = 1;
                while (line != null)
                {
                    if (line.ElementAt(0) == '#' || line.ElementAt(0) == '\n')
                    {
                        line = sr.ReadLine();
                        continue;
                    }
                    
                    //string[] splits = line.Split(" -> ".ToCharArray());
                    string[] splits = Regex.Split(line," -> ");
                    left = splits[0];
                    right = splits[1];

                    //右侧有多个候选式
                    if (right.Contains(" @ "))
                    {
                        //string[] rights = right.Split(" @ ".ToCharArray());
                        string[] rights = Regex.Split(right, " @ ");
                        foreach (string temp in rights)
                        {
                            ArrayList oneRight = new ArrayList();
                            foreach (string s1 in temp.Split(" ".ToCharArray()))
                            {
                                oneRight.Add(s1);
                            }
                            productions.Add(new Production(i, left, oneRight));
                            Console.Write(""+i+" "+left+"->");
                            foreach(string s in oneRight)
                            {
                                Console.Write(s + ", ");
                            }
                            Console.WriteLine("");
                            i++;
                        }
                    }
                    else
                    {
                        ArrayList oneRight = new ArrayList();
                        //foreach (string temp in right.Split(" ".ToCharArray()))
                        foreach(string temp in Regex.Split(right," "))
                        {
                            oneRight.Add(temp);
                        }
                        productions.Add(new Production(i,left,oneRight));
                        Console.Write("" + i + " " + left + "->");
                        foreach (string s in oneRight)
                        {
                            Console.Write(s + ", ");
                        }
                        Console.WriteLine("");
                        i++;
                    }
                    line = sr.ReadLine();

                }
                sr.Close();
                return productions;
            }
        }
    }
}
