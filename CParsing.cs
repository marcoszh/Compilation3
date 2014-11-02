using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MyCompilation
{
    class CParsing
    {
        //用于存储非终结符
        public Dictionary<string, CNTChar> ntChars
            = new Dictionary<string, CNTChar>();

        //用于存储终结符
        public Dictionary<string, CTChar> tChars
            = new Dictionary<string, CTChar>();

        //用于存储产生式
        public ArrayList productions = new ArrayList();
        //用于存储预测分析表
        public Dictionary<string, Dictionary<string, ArrayList>> predictionTable
            = new Dictionary<string, Dictionary<string, ArrayList>>();

        //用于存储可退出空串的终结符
        public ArrayList produceNullChars = new ArrayList();

        public ArrayList tokens = new ArrayList();

        public bool isChanged = true;

        //对产生式进行分析将字符分类加到对应的列表中
        public void charTNT()
        {
            //左部非终结符
            foreach (Production pro in productions)
            {
                if (!ntChars.ContainsKey(pro.Left))
                {
                    ntChars.Add(pro.Left, new CNTChar(pro.Left));
                }
            }
            //找出右部中的终结符
            foreach (Production pro in productions)
            {
                ArrayList tmpRight = new ArrayList();
                foreach (string temp in pro.Right)
                {
                    tmpRight.Add(temp);
                }
                foreach (string key in ntChars.Keys)
                {
                    tmpRight.Remove(key);
                    tmpRight.Remove(key);
                    tmpRight.Remove(key);
                    tmpRight.Remove(key);
                    tmpRight.Remove(key);
                }
                foreach (string s1 in tmpRight)
                {
                    if (!tChars.ContainsKey(s1))
                    {
                        tChars.Add(s1,new CTChar(s1));
                    }
                }

            }
        }

        //设置终结符FIRST集
        public void setTCharFirst(string X)
        {
            tChars[X].First.Add(X);
        }
      
        //是否为终结符
        public bool isTChar(string s)
        {
            return tChars.ContainsKey(s);
        }

        //是否为非终结符
        public bool isNTChar(string s)
        {
            return ntChars.ContainsKey(s);
        }

        //是否可以退出空串
        public bool canLeadNull(string X)
        {
            if (isTChar(X))
                return false;
            else
            {
                if (produceNullChars.Contains(X))
                    return true;

                foreach (Production p in productions)
                {
                    if (X.Equals(p.Left))
                    {
                        //当前存在$
                        if ("$".Equals(p.Right[0]))
                        {
                            produceNullChars.Add(X);
                            return true;
                        }
                        //递归查找
                        else
                        {
                            bool flag = true;
                            foreach (string s in p.Right)
                            {
                                if (!canLeadNull(s))
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag == true)
                            {
                                produceNullChars.Add(X);
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        //获得字符串的FIRST集
        public ArrayList getFirst(string X)
        {
            if (isNTChar(X))
            {
                return ntChars[X].First;
            }
            else if (isTChar(X))
            {
                return tChars[X].First;
            }
            else
                return null;
        }

        //初始化非终结符的FIRST集
        public void initNTCharFirst(string X)
        {
            foreach (Production p in productions)
            {
                if (X.Equals(p.Left))
                {
                    string firstR = (string)(p.Right)[0];
                    //右面第一个是终结符
                    if (isTChar(firstR))
                    {
                        if (!ntChars[X].First.Contains(firstR))
                        {
                            ntChars[X].First.Add(firstR);
                        }
                    }
                }
            }
        }

        //对非终结符的First集进行计算
        public void addNTCharFirst(string X)
        {
            foreach (Production p in productions)
            {
                //左部匹配
                if (X.Equals(p.Left))
                {
                    //右部第一个字符
                    string firstR = (string)p.Right[0];
                    //非终结符
                    if (isNTChar(firstR))
                    {
                        //将其FIRST集并入
                        foreach (string s in ntChars[firstR].First)
                        {
                            if (!ntChars[X].First.Contains(s))
                            {
                                ntChars[X].First.Add(s);
                                isChanged = true;
                            }
                        }
                    }
                }
            }

            foreach (Production p in productions)
            {
                if (X.Equals(p.Left))
                {
                    string current = "";
                    //右侧的所有字符
                    foreach (string s0 in p.Right)
                    {
                        current = s0;
                        //可推导出空串
                        if (canLeadNull(current))
                        {
                            //将其FIRST集加入
                            foreach (string s in getFirst(current))
                            {
                                if (!ntChars[X].First.Contains(s))
                                {
                                    ntChars[X].First.Add(s);
                                    isChanged = true;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    foreach (string s in getFirst(current))
                    {
                        if (!ntChars[X].First.Contains(s))
                        {
                            ntChars[X].First.Add(s);
                            isChanged = true;

                        }
                    }
                }
            }
        }

        //初始化FOLLOW集
        public void initFollow()
        {
            //将句子的结束符加入
            Production p = (Production) productions[0];
            ntChars[p.Left].Follow.Add("#");
        }

        //计算非终结符FOLLOW集
        public void addNTFollow()
        {
            foreach (Production production in productions)
            {
                string currentLeft = production.Left;
                ArrayList currentRight = production.Right;

                int len = currentRight.Count;

                for (int i = 0; i < len; i++)
                {
                    string currentChar = (string)currentRight[i];
                    //为终结符则不进行计算
                    if (isTChar(currentChar))
                        continue;

                    if (i < len - 1)
                    {
                        ArrayList rest = new ArrayList(currentRight.GetRange(i + 1, len-i-1));
                        ArrayList firstSetOfRest = getFirstSetOfChar(rest);
                        foreach (string s in firstSetOfRest)
                        {
                            if (!ntChars[currentChar].Follow.Contains(s))
                            {
                                ntChars[currentChar].Follow.Add(s);
                                isChanged = true;
                            }
                        }
                        bool flag = true;
                        foreach (string s in rest)
                        {
                            if (!canLeadNull(s))
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            ArrayList followSetOfLeft = ntChars[currentLeft].Follow;
                            foreach (string s in followSetOfLeft)
                            {
                                if (!ntChars[currentChar].Follow.Contains(s))
                                {
                                    ntChars[currentChar].Follow.Add(s);
                                    isChanged = true;
                                }
                            }
                        }

                    }
                    else if(i==len-1)
                    {
                        ArrayList followSetOfLeft = ntChars[currentLeft].Follow;
                        foreach (string s in followSetOfLeft)
                        {
                            if (!ntChars[currentChar].Follow.Contains(s))
                            {
                                ntChars[currentChar].Follow.Add(s);
                                isChanged = true;
                            }
                        }
                    }
                }
            }
        }

        //获得某产生式右部子串FIRST集
        public ArrayList getFirstSetOfChar(ArrayList alpha)
        {
            ArrayList firstSet = new ArrayList();

            if (alpha.Count > 0)
            {
                string initStr = (string)alpha[0];

                //初始化
                foreach (string s in getFirst(initStr))
                {
                    if (!firstSet.Contains(s))
                    {
                        firstSet.Add(s);
                    }
                }
            }

            //求FIRST集
            if (alpha.Count == 1)
            {
                foreach (string s in getFirst((string)alpha[0]))
                {
                    if (!firstSet.Contains(s))
                    {
                        firstSet.Add(s);
                    }
                }

            }
            else
            {
                int k = 0;
                while (canLeadNull((string)alpha[k]) && k < alpha.Count - 1)
                {
                    foreach (string s in getFirst((string)alpha[k + 1]))
                    {
                        if (!firstSet.Contains(s))
                            firstSet.Add(s);
                    }
                    k++;
                }
            }
            return firstSet;
        }

        //为产生式设置SELECT集
        public void setSelectForProduction()
        {
            foreach(Production production in productions)
            {
                if ("$".Equals(production.Right[0]))
                {
                    //左侧的FOLLOW集
                    foreach (string s in ntChars[production.Left].Follow)
                    {
                        if (!production.Select.Contains(s))
                        {
                            production.Select.Add(s);
                        }
                    }
                }
                else
                {
                    foreach (string s in getFirstSetOfChar(production.Right))
                    {
                        if (!production.Select.Contains(s))
                        {
                            production.Select.Add(s);
                        }
                    }
                    bool flag = true;
                    foreach (string s in production.Right)
                    {
                        if (!canLeadNull(s))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        foreach(string s in ntChars[production.Left].Follow)
                        {
                            if (!production.Select.Contains(s))
                            {
                                production.Select.Add(s);
                            }
                        }
                    }
                }
            }
        }

        //产生预测分析表
        public void setPredictionTable()
        {
            foreach(Production production in productions)
            {
                if (predictionTable.Keys.Contains(production.Left))
                {
                    foreach (string s in production.Select)
                    {
                        ArrayList outs;
                        if (predictionTable[production.Left].TryGetValue(s,out outs))
                        {
                            predictionTable[production.Left].Remove(s);
                        }
                         
                        predictionTable[production.Left].Add(s, production.Right);
                    }
                }
                else
                {
                    Dictionary<string, ArrayList> rowDic = new Dictionary<string, ArrayList>();
                    foreach (string s in production.Select)
                    {
                        rowDic.Add(s, production.Right);
                    }
                    predictionTable.Add(production.Left, rowDic);
                }
            }
        }

        //为非终结符设置同步记号，用于错误恢复
        public void setSync()
        {
            foreach (string s in ntChars.Keys)
            {
                ntChars[s].generateSync();
            }
        }

        //生成stack内的字符串
        public string getStackString(Stack<string> pdChars)
        {
            string temp = "";
            foreach (string s in pdChars)
            {
                temp +=("["+s+"] ");
            }
            return temp;
        }

        //输出token值
        public void printTokens()
        {
            Console.WriteLine("Tokens:");
            foreach (MyLexicalToken token in tokens)
            {
                Console.WriteLine("["+CLUtility.getTokenValue(token)+"]:"+token.Name);
            }
        }

        //进行LL1文法分析
        public void LL1Analysis(string root)
        {
            SResult rForm = new SResult();
            rForm.Show();
            printTokens();
            //ArrayList proSq = new ArrayList();
            Stack<string> pdChars = new Stack<string>();
            pdChars.Push("#");
            pdChars.Push(root);

            int index = 0;
            int No = 1;
            //未到结束
            rForm.Update0();
            while (!"#".Equals(pdChars.Peek()))
            {
                
                //取栈顶
                string X = pdChars.Peek();
                MyLexicalToken a = new MyLexicalToken();
                //当前输入符号
                if (index < tokens.Count)
                {
                    a = (MyLexicalToken) tokens[index];
                }

                //终结符或#
                if (isTChar(X) || "#".Equals(X))
                {
                    if (CLUtility.isEqualTT(a, X))
                    {
                        //弹出并前移指针
                        if (!"#".Equals(X))
                        {
                            pdChars.Pop();
                            index++;
                        }
                    }
                    else
                    {
                        string errorStr = pdChars.Pop();
                        errorStr = "ERROR: Ignore the input [" + errorStr + "]";
                        rForm.addListItem(No++, a.LineCount, "ERROR "+a.Name, errorStr, pdChars.Peek(), getStackString(pdChars));
                    }
                }
                else
                {
                    //ArrayList item = predictionTable[X][CLUtility.getTokenValue(a)];
                    ArrayList item = new ArrayList();
                    //predictionTable[X].TryGetValue(CLUtility.getTokenValue(a),out item);
                    if (predictionTable[X].TryGetValue(CLUtility.getTokenValue(a), out item))
                    {
                        pdChars.Pop();
                        if (!"$".Equals(item[0]))
                        {
                            for (int i = item.Count-1; i > -1; i--)
                            {
                                pdChars.Push((string)item[i]);
                            }
                        }
                        string output = "[" + X + "] -> ";
                        foreach (string s in item)
                        {
                            output += ("[" + s + "] ");
                        }
                        Console.WriteLine("" + No +" "+CLUtility.getTokenValue(a)+" " +a.Name+" "+ output + pdChars.Count);
                        rForm.addListItem(No++, a.LineCount, a.Name, output, pdChars.Peek(), getStackString(pdChars));
                    }
                    else
                    {
                        if (ntChars[X].Sync.Contains(a))
                        {
                            string errorStr = pdChars.Pop();
                            errorStr = "ERROR: Poped non-terminal char: [" + errorStr + "]";
                            rForm.addListItem(No++, a.LineCount, "ERROR " + a.Name, errorStr, pdChars.Peek(), getStackString(pdChars));
                        }
                        else
                        {
                            string errorStr = pdChars.Pop();
                            errorStr = "ERROR: Ignore the input [" + errorStr + "]";
                            rForm.addListItem(No++, a.LineCount, "ERROR " + a.Name, errorStr, pdChars.Peek(), getStackString(pdChars));
                        }
                    }
                }

            }
            rForm.Update1();
        }

    }
}
