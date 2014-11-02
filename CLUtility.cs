using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompilation
{
    //元素类型
    public enum ElementType : int
    {
        None = 0,                   //未定义
        Separator = 1,           //分隔符
        Operator = 2,            //运算符
        Identifier = 3,              //标识符
        Keyword = 4,                //关键字
        BiInteger = 5,              //二进制数
        DecInteger = 6,                //十进制整形常数
        OctInteger = 7,           //八进制整数
        HexInteger=8,           //十六进制整数
        Float = 9,                  //浮点常数
        String = 10,                 //字符串
        Char = 11,                   //字符常数
    }


    //变量类型
    public enum IdentifierType : int
    { 
        Void = 0,                  //无
        Char = 1,                  //字符变量
        Int = 2,                   //整数变量
        Float = 3,                 //浮点数变量
        Double = 4,               //双精度
    }
    //错误类型
    public enum ErrorType : int
    {
        UnkownSymbols = 0,          //未定义的符号
        AbnormalEnd = 1,            //异常结尾

    }
    //词法分析符号表设计
    public struct MyLexicalSymbol
    {
        public string Value;         //符号值
        public IdentifierType Type;   //类型
        public int LineCount;        //行数
        public int Num;
    };

    //词法分析输出的Token序列
    public struct MyLexicalToken
    {
        public ElementType Type;      //Token序列中元素类型
        public string Name;          //Token序列中元素值
        public int LineCount;         //Token序列中元素所在的行数
        public int Code;
        public string Others;
    };

    //词法分析中的错误
    public struct LexicalError
    {
        public ErrorType Type;       //错误类型
        public string Value;         //错误点的元素值
        public int LineCount;        //错误所在的行数
    };


    class CLUtility
    {
        public const int KeyWordBase = 0;
        public const int SeparatorBase = 64;
        public const int OperatorBase = 128;
        public const int IdBase = 256;
        public const int OctNum = 257;
        public const int BiNum = 258;
        public const int DecNum = 259;
        public const int HexNum = 260;
        public const int FloatNum = 261;
        public const int CharCode = 512;
        public const int StringCode = 513;
        //关键字,32个
        public static string[] KeyWord = new string[]
                                        {
                                            "auto","break","case","char","const","continue",
	                                        "default","do","double","else","enum","extern",
					                        "float","for","goto","if","int","long","register",
					                        "return","short","signed","static","sizeof","struct",
					                        "switch","typedef","union","unsigned","void","volatile","while"
                                        };
        //分隔符，7个
        public static char[] SeparatorChar = new char[]
                                        {
                                            ',','(',')','{','}',';','.',':','[',']'
                                        };
        //运算符，10个
        public static char[] OperatorChar = new char[]
                                         {
                                             '+','-','*','/','%','&','|','>','=','<'
                                         };

        public static int GetIndexKeyWord(string word)
        {
            return Array.IndexOf(KeyWord, word);
        }

        public static int GetIndexSeparator(char word)
        {
            return Array.IndexOf(SeparatorChar, word);
        }

        public static int GetIndexOperator(char word)
        {
            return Array.IndexOf(OperatorChar, word);
        }

        #region 词法分析

        //判断字符串是否是定义的关键字
        public static bool IsKeyWord(string word)
        {
            int index = Array.BinarySearch(KeyWord, word);
            if (index >= 0)
                return true;
            else
                return false;
        }

        //判断是否是空白字符，过滤掉
        public static bool IsInvalidChars(char c)
        {
            if (c == ' ' || c == '\t')
                return true;
            else
                return false;
        }

        //判断字符是否是定义的分隔符
        public static bool IsSeparatorChar(char c)
        {
            int index = Array.IndexOf(SeparatorChar, c);
            if (index >= 0)
                return true;
            else
                return false;
        }

        //判断字符是否是定义的单个运算符
        public static bool IsOperatorChar(char c)
        {
            int index = Array.IndexOf(OperatorChar, c);
            if (index >= 0)
                return true;
            else
                return false;
        }

        //判断字符是否为字母
        public static bool IsLetter(char ch)
        {
            if (('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z'))
                return true;
            else
                return false;
        }

        //判断字符是否为数字
        public static bool IsDigit(char ch)
        {
            if ('0' <= ch && ch <= '9')
                return true;
            else
                return false;
        }

        public static bool IsOneToSeven(char ch)
        {
            if ('1' <= ch && ch <= '7')
                return true;
            else
                return false;
        }

        public static bool IsOneToNine(char ch)
        {
            if ('1' <= ch && ch <= '9')
                return true;
            else
                return false;
        }

        public static bool IsAToF(char ch)
        {
            if ('a' <= ch && ch <= 'f')
                return true;
            else
                return false;
        }

        public static bool IsBiInteger(String inStr)
        {
            char[] charArr = inStr.ToCharArray();
            for (int i = 0; i < charArr.Length - 1; i++)
                if ((charArr[i] != '0') && (charArr[i] != '1'))
                    return false;
            if (charArr[charArr.Length - 1] == 'b')
                return true;
            return false;
        }

        public static bool IsOctInteger(String inStr)
        {
            char[] charArr = inStr.ToCharArray();
            if (charArr.Length < 2)
                return false;
            if (charArr[0] != 0)
                return false;
            if (!IsOneToSeven(charArr[1]))
                return false;
            if (charArr.Length == 2)
                return true;
            for (int i = 2; i < charArr.Length; i++)
                if ((!IsOneToSeven(charArr[i]))&& charArr[i]!='0')
                    return false;
            return true;
        }

        public static bool IsFloat(String inStr)
        {
            int pointNum = 0;
            char[] charArr=inStr.ToCharArray();
            for (int i = 0; i < charArr.Length-1; i++)
            {
                if (charArr[i] == '.')
                    pointNum++;
                else if (!IsDigit(charArr[i]))
                    return false;
            }
            if ((IsDigit(charArr[charArr.Length-1])) && (pointNum == 1))
                return true;
            else
                return false;
        }

        public static bool IsHexInteger(String inStr)
        {
            char[] charArr = inStr.ToCharArray();
            if(charArr.Length<3)
                return false;
            if ((charArr[0] != '0') || (charArr[1] != 'x'))
                return false;
            for (int i = 2; i < charArr.Length; i++)
            {
                if ((!IsDigit(charArr[i])) && (!IsAToF(charArr[i])))
                    return false;
            }
            return true;
        }

        public static bool IsDecInteger(String inStr)
        {
            char[] charArr = inStr.ToCharArray();
            if (charArr.Length <= 2)
                for (int i = 0; i < charArr.Length; i++)
                {
                    if (!IsDigit(charArr[i]))
                        return false;
                }
            else if (charArr[1] == '.')
            {
                if (!IsOneToNine(charArr[0]))
                    return false;
                int i=2;
                while (charArr[i] != 'e')
                {
                    if (!IsDigit(charArr[i]))
                        return false;
                    i++;
                }
                i++;
                for (; i < charArr.Length; i++)
                {
                    if (!IsDigit(charArr[i]))
                        return false;
                }
                return true;

            }
            else
            {
                for (int i = 0; i < charArr.Length; i++)
                {
                    if (!IsDigit(charArr[i]))
                        return false;
                }
            }
            return true;


        }
        #endregion

        #region 句法分析
        //判断token和值的关系
        public static bool isEqualTT(MyLexicalToken token,string s)
        {
            if (s.Equals(token.Name))
                return true;
            else if ("IDENTIFIER".Equals(s))
            {
                if (token.Type == ElementType.Identifier)
                    return true;
                else
                    return false;
            }
            else if (IsKeyWord(s) || IsOperatorChar(s.ToCharArray()[0]) || IsSeparatorChar(s.ToCharArray()[0]))
            {
                if (s.Equals(token.Name))
                    return true;
                else
                    return false;
            }
            else if (s.Equals("CONST_INT"))
            {
                if (token.Type == ElementType.BiInteger || token.Type == ElementType.DecInteger
                    || token.Type == ElementType.HexInteger || token.Type == ElementType.OctInteger)
                    return true;
                else
                    return false;
            }
            else if (s.Equals("CONST_FLOAT"))
            {
                if (token.Type == ElementType.Float)
                    return true;
                else
                    return false;
            }
            else if (s.Equals("CONST_CHAR"))
            {
                if (token.Type == ElementType.Char)
                    return true;
                else
                    return false;
            }
            else if (s.Equals("CONST_STRING"))
            {
                if (token.Type == ElementType.String)
                    return true;
                else
                    return false;

            }
            else
                return false;
        }
        //获得token的值
        public static string getTokenValue(MyLexicalToken token)
        {
            if (token.Type == ElementType.Identifier)
                return "IDENTIFIER";
            else if (token.Type == ElementType.BiInteger || token.Type == ElementType.DecInteger
                    || token.Type == ElementType.HexInteger || token.Type == ElementType.OctInteger)
                return "CONST_INT";
            else if (token.Type == ElementType.Float)
                return "CONST_FLOAT";
            else if (token.Type == ElementType.String)
                return "CONST_STRING";
            else if (token.Type == ElementType.Char)
                return "CONST_CHAR";
            else
                return token.Name;
        }
        #endregion
    }
}
