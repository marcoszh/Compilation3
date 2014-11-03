using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCompilation
{
    //语义分析时使用的段
    struct Segment
    {
        public int id;  //标号
        public int start;   //开始索引
        public int end; //结束索引
        public Segment(int id, int start, int end)
        {
            this.id = id;
            this.start = start;
            this.end = end;
        }
    }
    //语义分析时用的token
    struct Token
    {
        public int id;  //token的id
        public string val;  //token的值
        public string type; //token的类型
        public Token(int id, string val, string type)
        {
            this.id = id;
            this.val = val; this.type = type;
        }
    }

    //函数的数据结构
    struct functable
    {
        public string type; //函数返回值
        public string name; //函数名
        public functable(string type, string name)
        {
            this.type = type;
            this.name = name;
        }
    }

    //符号表中使用的结构
    struct Chartable
    {
        public string name; //符号的类型
        public string type; //符号的类型
        public int offset;  //偏移地址
        public int length;  //分配空间大小
        public string funcname; //所处的函数作用域
        public Chartable(string name, string type, int offset, int length, string funcname)
        {
            this.name = name;
            this.type = type;
            this.offset = offset;
            this.length = length;
            this.funcname = funcname;
        }
    }

    //进行语义分析的类的封装
    class Semantics
    {
        static int lcount = 1;
        static int selfcount = 1;
        static int logicalcount = 1;
        static int j = 0;
        static int offset = 0;//
        static List<Segment> segment = new List<Segment>();
        public static List<Token> tokenlist = new List<Token>();

        //将代码分成片段，函数、分支、循环、赋值、声明，便于进行分别处理
        public static void GetSegment(List<Token> tokenlist)
        {
            Stack<Token> stack = new Stack<Token>();
            stack.Push(new Token(0, "#", "#"));
            int start = 0, end = 0;
            string s = "";
            foreach (Token t in tokenlist)
            {
                stack.Push(t);
                if (stack.Peek().type.Equals(";"))
                {
                    end = stack.Peek().id;
                    start = stack.Peek().id;
                    stack.Pop();
                    while (!stack.Peek().type.Equals(";") && !stack.Peek().type.Equals("#") && !stack.Peek().type.Equals("}") && !stack.Peek().type.Equals("{"))
                    {
                        start = stack.Peek().id;
                        stack.Pop();
                    }
                    segment.Add(new Segment(segment.Count + 1, start, end));
                }
                else if (stack.Peek().type.Equals("}"))
                {
                    end = stack.Peek().id;
                    start = stack.Peek().id;
                    stack.Pop();
                    while (!stack.Peek().type.Equals("{") && !stack.Peek().type.Equals("#"))
                    {
                        s = stack.Peek().type + stack.Peek().id.ToString();
                        start = stack.Peek().id;
                        stack.Pop();
                    }
                    if (!stack.Peek().type.Equals("#"))
                    {
                        start = stack.Peek().id;
                        stack.Pop();
                    }
                    segment.Add(new Segment(segment.Count + 1, start, end));
                }
                else if (stack.Peek().type.Equals(")"))
                {
                    end = stack.Peek().id;
                    start = stack.Peek().id;
                    stack.Pop();
                    while (!stack.Peek().type.Equals("(") && !stack.Peek().type.Equals("#"))
                    {
                        s = stack.Peek().type + stack.Peek().id.ToString();
                        start = stack.Peek().id;
                        stack.Pop();
                    }
                    if (!stack.Peek().type.Equals("#"))
                    {
                        s = stack.Peek().type + stack.Peek().id.ToString();
                        start = stack.Peek().id;
                        stack.Pop();
                    }
                    while (!stack.Peek().type.Equals("IF") && !stack.Peek().type.Equals("ELSE") && !stack.Peek().type.Equals("WHILE") && !stack.Peek().type.Equals("}") && !stack.Peek().type.Equals(";") && !stack.Peek().type.Equals("#") && !stack.Peek().type.Equals("INT") && !stack.Peek().type.Equals("FLOAT"))
                    {
                        s = stack.Peek().type + stack.Peek().id.ToString();
                        start = stack.Peek().id;
                        stack.Pop();
                    }
                    if (!stack.Peek().type.Equals("#"))
                    {
                        start = stack.Peek().id;
                        stack.Pop();
                    }
                    segment.Add(new Segment(segment.Count + 1, start, end));
                }
            }
        }
        public static string GetLId(List<Token> tokenlist, int j)
        {
            foreach (Segment seg in segment)
            {
                if (seg.start == j)
                {
                    return "L" + seg.id;
                }
            }
            return "";
        }
        public static string ShowL(List<Token> tokenlist, int j)
        {
            string lstr = "";
            lcount++;
            int lbegin = 0;
            string spr = "";
            foreach (Segment seg in segment)
            {
                if (seg.end == j)
                {
                    lbegin = seg.start;
                    lstr = "L" + seg.id + ":";
                }
            }
            for (int k = lbegin; k < j + 1; k++)
            {
                spr = spr + tokenlist[k].val + " ";
            }
            return lstr + " " + spr;
        }

        //返回label字符串
        public static string ShowLS(List<Token> tokenlist, int j)
        {
            string lstr = "";
            lcount++;
            int lend = 0;
            string spr = "";
            foreach (Segment seg in segment)
            {
                if (seg.start == j)
                {
                    lend = seg.end;
                    lstr = "L" + seg.id + ":";
                }
            }
            for (int k = j; k < lend + 1; k++)
            {
                spr = spr + tokenlist[k].val + " ";
            }
            return lstr + " " + spr;
        }
        ///
        public static List<string> tresult = new List<string>();
        public static List<string> eresult = new List<string>();
        public static List<string> rresult = new List<string>();
        public static List<Chartable> table = new List<Chartable>();
        public static List<functable> functable = new List<functable>();

        //比对符号表，看是否重复
        public static int findTable(string name, string funcname, List<Chartable> table)
        {
            int i = 0;
            while (i < table.Count)
            {
                if (name == table[i].name && funcname == table[i].funcname)
                {
                    return 4;
                }
                i++;
            }
            return -1;
        }
        //看函数表，是否重复
        public static int findVarType(string name, string funcname, List<Chartable> table)
        {
            int i = 0;
            while (i < table.Count)
            {
                if (name == table[i].name && funcname == table[i].funcname)
                {
                    if (table[i].type.Length > 5)
                    {
                        if (table[i].type.Substring(0, 5) == "array")
                        {
                            return Convert.ToInt32(table[i].type.Substring(6, 1));
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                i++;
            }
            return 0;
        }
        
        //查找函数表
        public static Boolean findFunc(string funcname, List<functable> functable)
        {
            int i = 0;
            while (i < functable.Count)
            {
                if (funcname == functable[i].name)
                {
                    return true;
                }
                i++;
            }
            return false;
        }
        public static string A(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type == "IDN")
            {
                rresult.Add(ShowLS(tokenlist, j));
                string IDNval = tokenlist[j].val;
                int rest = findTable(IDNval, funcname, table);
                if (rest != -1)
                {
                    int index = findVarType(IDNval, funcname, table);
                    string bsyn = B(tokenlist, funcname);
                    string var = "";
                    if (bsyn != "")
                    {
                        if (IsNumeric(bsyn))
                        {
                            if (Convert.ToInt32(bsyn) > index)
                            {
                                eresult.Add("var " + IDNval + " over range!");
                            }
                        }
                        var = "[ " + bsyn + " ]";
                        j++;
                    }
                    if (tokenlist[j].type == "=")
                    {
                        j++;
                        string tsyn = T(tokenlist, funcname);
                        Console.WriteLine(IDNval + var + "=" + tsyn);
                        rresult.Add(IDNval + var + "=" + tsyn);
                        if (tokenlist[j].type == ";")
                        {
                            j++;
                        }
                        return "success";
                    }
                    else
                    {
                        if (tokenlist[j].type == ";")
                        {
                            eresult.Add("expect" + "=");
                        }
                        else
                        {
                            eresult.Add("no end");
                        }
                        return "error";
                    }
                }
                else
                {
                    eresult.Add("undefined var" + " " + IDNval);
                    while (tokenlist[j].type != ";")
                    {
                        j++;
                    }
                    j++;
                    return "undefined var";
                }
            }
            else return "error";
        }
        public static string T(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type == "CONST_INT" || tokenlist[j].type == "CONST_FLOAT" || tokenlist[j].type == "IDN")
            {
                string fval = F(tokenlist, funcname);
                string Tval = T1(tokenlist, fval, funcname);
                return Tval;
                //prior try
                //string yval = Y(tokenlist, funcname);
            }
            else return "error";
        }
        public static string Y(List<Token> tokenlist, string funcname)
        {
            return "success";

        }
        public static string T1(List<Token> tokenlist, string inh, string funcname)
        {
            if (tokenlist[j].type == "*")
            {
                j++;
                string fval = F(tokenlist, funcname);
                rresult.Add("t" + selfcount + " = " + fval + " * " + inh);
                inh = "t" + (selfcount);
                selfcount++;
                string syn = T1(tokenlist, inh, funcname);
                return syn;
            }
            if (tokenlist[j].type == "+")
            {
                j++;
                string fval = F(tokenlist, funcname);
                Console.WriteLine("t" + selfcount + " = " + fval + " + " + inh);
                rresult.Add("t" + selfcount + " = " + fval + " + " + inh);
                inh = "t" + (selfcount);
                selfcount++;
                string syn = T1(tokenlist, inh, funcname);
                return syn;
            }
            else if (tokenlist[j].type == "]" || tokenlist[j].type == ";" || tokenlist[j].type == "<" || tokenlist[j].type == ">" || tokenlist[j].type == ")" || tokenlist[j].type == "&&" || tokenlist[j].type == "||")
            {
                return inh;
            }
            else
            {
                return "error";
            }
        }
        //判断字符串是否是整数
        public static Boolean IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
            return reg1.IsMatch(str);
        }

        public static string F(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type == "CONST_INT" || tokenlist[j].type == "CONST_FLOAT")
            {
                string fval = tokenlist[j].val;
                j++;
                return fval;
            }
            else if (tokenlist[j].type == "IDN")
            {
                string IDNval = tokenlist[j].val;//find table get type
                int rest = findTable(IDNval, funcname, table);
                if (rest == -1)
                {
                    eresult.Add("undefined variable " + IDNval);
                    return "undefined variable";
                }
                else
                {
                    int index = findVarType(IDNval, funcname, table);
                    string bsyn = B(tokenlist, funcname);
                    if (bsyn != "")//array
                    {
                        if (IsNumeric(bsyn))
                        {
                            if (Convert.ToInt32(bsyn) > index)
                            {
                                eresult.Add("var " + IDNval + " over range!");
                            }
                        }
                        string var = "t" + selfcount + "=" + bsyn + "*" + rest;
                        rresult.Add(var);
                        selfcount++;
                        string array = "t" + selfcount + "=" + IDNval + "[ t" + (selfcount - 1) + " ]";
                        rresult.Add(array);
                        selfcount++;
                        j++;
                        return "t" + (selfcount - 1);
                    }
                    else
                    { //IDN
                        return IDNval;
                    }
                }
            }
            else
                return "error";
        }
        public static string B(List<Token> tokenlist, string funcname)
        {
            j++;
            if (tokenlist[j].type == "[")
            {
                j++;
                string tval = T(tokenlist, funcname);
                if (tokenlist[j].type == "]")
                {
                    return tval;
                }
                else
                {
                    return "error";
                }
            }
            else if (tokenlist[j].type == "=" || tokenlist[j].type == "*" || tokenlist[j].type == "+" || tokenlist[j].type == ")" || tokenlist[j].type == "]" || tokenlist[j].type == ";" || tokenlist[j].type == "<" || tokenlist[j].type == ">" || tokenlist[j].type == "&&" || tokenlist[j].type == "||" || tokenlist[j].type == ")")//B#$
            {
                return "";
            }
            else return "error";
        }
        public static string P(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type == "FLOAT" || tokenlist[j].type == "INT" || tokenlist[j].type == "CONST_INT" || tokenlist[j].type == "CONST_FLOAT" || tokenlist[j].type == "IDN")
            {
                string resoffset = D(tokenlist, funcname);
                return resoffset;
            }
            else return "error";
        }
        public static string C(List<Token> tokenlist, string type, int width)
        {
            if (tokenlist[j].type == ";")
            {
                return type + "#" + width;//在结束位置写返回值内容
            }
            else if (tokenlist[j].type == "[")
            {
                j++;
                if (tokenlist[j].type == "CONST_INT")
                {
                    string res = tokenlist[j].val;
                    j++;
                    if (tokenlist[j].type == "]")
                    {
                        j++;
                        string ctype = "array(" + res + "," + type + ")";
                        int cwidth = Convert.ToInt32(res) * width;
                        string csyn = C(tokenlist, ctype, cwidth);//调用位置指示返回值
                        return csyn;
                    }
                    else
                        return "error";
                }
                else
                    return "error";
            }
            else
                return "error";
        }
        public static string D(List<Token> tokenlist, string funcname)
        {
            string paramtype = tokenlist[j].type;
            if (tokenlist[j].type == "INT" || tokenlist[j].type == "FLOAT")
            {
                j++;
                if (tokenlist[j].type == "IDN")
                {
                    string IDNval = tokenlist[j].val;
                    int width = 4;
                    string type = paramtype.ToLower();
                    j++;
                    string csyn = C(tokenlist, type, width);
                    string[] str = csyn.Split(new char[] { '#' });
                    if (findTable(IDNval, funcname, table) == 4)
                    {
                        eresult.Add("already defined var " + IDNval);
                        //j++;
                    }
                    else
                    {
                        table.Add(new Chartable(IDNval, str[0], offset, Convert.ToInt32(str[1]), funcname));
                        tresult.Add(IDNval + "\t" + str[0] + "\t" + offset + "\t" + str[1] + "\t" + funcname);
                        offset = offset + Convert.ToInt32(str[1]);
                    }
                    if (tokenlist[j].type == ";")
                    {
                        rresult.Add(ShowL(tokenlist, j));
                        j++;
                        string resoffset = D(tokenlist, funcname);
                        return resoffset;
                    }
                    else
                        return "error";
                }
                else
                    return "error";
            }
            else if (tokenlist[j].type == "CONST_INT" || tokenlist[j].type == "IDN" || tokenlist[j].type == "CONST_FLOAT")
            {
                return offset + "";
            }
            else
                return "error";
        }
        public static void S(List<Token> tokenlist)
        {
            if (tokenlist[j].type == "INT" || tokenlist[j].type == "FLOAT")
            {
                FUNC(tokenlist);
                S1(tokenlist);
            }
        }
        public static void S1(List<Token> tokenlist)
        {
            if (tokenlist[j].type == "INT" || tokenlist[j].type == "FLOAT")
            {
                FUNC(tokenlist);
                S1(tokenlist);
            }
            else if (tokenlist[j].type == "#")
            {

            }
        }

        //判断类型
        public static string TYPE(List<Token> tokenlist)
        {
            if (tokenlist[j].type == "INT")
                return "int";
            else if (tokenlist[j].type == "FLOAT")
                return "float";
            else return "error";
        }
        public static void E(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type == ")")
            {

            }
            else if (tokenlist[j].type == "INT" || tokenlist[j].type == "FLOAT")
            {
                string paramtype = TYPE(tokenlist);
                j++;
                if (tokenlist[j].type == "IDN")
                {
                    string IDNval = tokenlist[j].val;
                    table.Add(new Chartable(IDNval, paramtype, offset, 4, funcname));//string name, string type, int offset, int length
                    tresult.Add(IDNval + "\t" + paramtype + "\t" + offset + "\t" + 4 + "\t" + funcname);
                    offset += 4;
                    j++;
                    E1(tokenlist, funcname);
                }
            }
            else
            {
                //error
            }
        }
        public static void E1(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type == ")")
            {
            }
            else if (tokenlist[j].type == ",")
            {
                j++;
                string paramtype = TYPE(tokenlist);
                j++;
                if (tokenlist[j].type == "IDN")
                {
                    string IDNval = tokenlist[j].val;
                    if (findTable(IDNval, funcname, table) == 4)
                    {
                        eresult.Add("already defined var" + IDNval);
                    }
                    else
                    {
                        table.Add(new Chartable(IDNval, paramtype, offset, 4, funcname));//string name, string type, int offset, int length
                        tresult.Add(IDNval + "\t" + paramtype + "\t" + offset + "\t" + 4 + "\t" + funcname);
                        offset += 4;
                    }
                    j++;
                    E1(tokenlist, funcname);
                }
            }
            else
            {
                //error
            }
        }
        public static void G(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type == "IDN")
            {
                A(tokenlist, funcname);
                G(tokenlist, funcname);
            }
            else if (tokenlist[j].type == "IF")
            {
                H(tokenlist, funcname);
                G(tokenlist, funcname);
            }
            else if (tokenlist[j].type == "WHILE")
            {
                W(tokenlist, funcname);
                G(tokenlist, funcname);
            }
            else if (tokenlist[j].type == "}")
            {

            }
            else
            {
                //error miss a '}'
            }
        }
        public static void W(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type.Equals("WHILE"))
            {
                //MY
                rresult.Add(ShowLS(tokenlist, j));
                /////////////
                j++;
                if (tokenlist[j].type.Equals("("))
                {
                    j++;
                    string str1 = Q(tokenlist, funcname);
                    string str2 = O(tokenlist, str1, funcname);
                    if (tokenlist[j].type.Equals(")"))
                    {
                        //MY
                        string gotoTrue = GetLId(tokenlist, j + 1);
                        int trueEnd = 0;
                        foreach (Segment seg in segment)
                        {
                            if (("L" + seg.id).Equals(gotoTrue))
                            {
                                trueEnd = seg.end;
                            }
                        }
                        string gotoFalse = GetLId(tokenlist, trueEnd + 2);
                        rresult.Add("    " + str2 + " true: goto " + GetLId(tokenlist, j + 1));
                        if (!gotoFalse.Equals(""))
                            rresult.Add("    " + str2 + " false: goto " + gotoFalse);
                        j++;
                        if (tokenlist[j].type.Equals("{"))
                        {
                            j++;
                            G(tokenlist, funcname);
                            if (tokenlist[j].type.Equals("}"))
                            {
                                rresult.Add(ShowL(tokenlist, j));
                                j++;
                            }
                        }
                    }
                }
            }
        }

        public static void H(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type.Equals("IF"))
            {
                rresult.Add(ShowLS(tokenlist, j));
                j++;
                if (tokenlist[j].type.Equals("("))
                {
                    j++;
                    string qval1 = Q(tokenlist, funcname);
                    string logicalval = O(tokenlist, qval1, funcname);
                    if (tokenlist[j].type.Equals(")"))
                    {
                        string gotoTrue = GetLId(tokenlist, j + 1);
                        int trueEnd = 0;
                        foreach (Segment seg in segment)
                        {
                            if (("L" + seg.id).Equals(gotoTrue))
                            {
                                trueEnd = seg.end;
                            }
                        }
                        rresult.Add("    " + logicalval + " true: goto " + GetLId(tokenlist, j + 1));
                        if (tokenlist[trueEnd + 1].type.Equals("ELSE"))
                        {
                            string gotoFalse1 = GetLId(tokenlist, trueEnd + 2);
                            rresult.Add("    " + logicalval + " false: goto " + gotoFalse1);
                        }
                        else if (tokenlist[trueEnd + 1].type.Equals("}"))
                        {
                            trueEnd++;
                            while (tokenlist[trueEnd].Equals("}"))
                            {
                                trueEnd++;
                            }
                            trueEnd++;
                            string gotoFalse1 = GetLId(tokenlist, trueEnd);
                            rresult.Add("    " + logicalval + " false: goto " + gotoFalse1);
                        }
                        else
                        {
                            string gotoFalse1 = GetLId(tokenlist, trueEnd + 1);
                            rresult.Add("    " + logicalval + " false: goto " + gotoFalse1);
                        }

                        j++;
                        if (tokenlist[j].type.Equals("{"))
                        {
                            j++;
                            G(tokenlist, funcname);
                            if (tokenlist[j].type.Equals("}"))
                            {
                                rresult.Add(ShowL(tokenlist, j));
                                j++;
                                N(tokenlist, funcname);
                            }
                        }
                    }
                }
            }
        }
        public static void N(List<Token> tokenlist, string funcname)
        {
            if (tokenlist[j].type.Equals("ELSE"))
            {
                j++;
                if (tokenlist[j].type.Equals("{"))
                {
                    j++;
                    G(tokenlist, funcname);
                    if (tokenlist[j].type.Equals("}"))
                    {
                        //MY
                        rresult.Add(ShowL(tokenlist, j));
                        ///////////////////
                        j++;
                    }
                }
            }
            else
            {
                //Console.WriteLine("no else");
            }
        }

        public static string O(List<Token> tokenlist, string qval1, string funcname)
        {
            if (tokenlist[j].type == "&&" || tokenlist[j].type == "||")
            {
                string op = tokenlist[j].type;
                j++;
                string qval2 = Q(tokenlist, funcname);
             
                return qval1 + op + qval2;
            }
            else if (tokenlist[j].type == ")")
            {
                return qval1;
            }
            else
            {
                return "error";
            }
        }
        public static string Q(List<Token> tokenlist, string funcname)
        {
            string tval1 = T(tokenlist, funcname);
            if (tokenlist[j].type.Equals("<") || tokenlist[j].type.Equals(">"))
            {
                string op = tokenlist[j].type;
                j++;
                string tval2 = T(tokenlist, funcname);
                string res = "a" + logicalcount + "=" + tval1 + op + tval2;
                Console.WriteLine(res);
                rresult.Add(res);
                string al = "a" + logicalcount;
                logicalcount++;
                return al;
            }
            else
                return "error";
        }

        //进行函数语义分析
        public static void FUNC(List<Token> tokenlist)
        {
            string functype = TYPE(tokenlist);
            //生成三地址码
            rresult.Add(ShowLS(tokenlist, j));
            j++;
            if (tokenlist[j].type == "IDN")
            {
                string IDNval = tokenlist[j].val;
                j++;
                if (findFunc(IDNval, functable))
                {
                    Console.WriteLine("double functionname");
                }
                else
                    functable.Add(new functable(functype, IDNval));
                //进行参数表分析
                if (tokenlist[j].type == "(")
                {
                    j++;
                    E(tokenlist, IDNval);
                    if (tokenlist[j].type == ")")
                    {
                        j++;
                        if (tokenlist[j].type == "{")
                        {
                            j++;
                            P(tokenlist, IDNval);
                            G(tokenlist, IDNval);
                            if (tokenlist[j].type == "}")
                            {
                                j++;
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
