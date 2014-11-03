using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace MyCompilation
{
    //词法分析类
    class CLexicalAnalysis
    {
        public ArrayList MyTokenList = new ArrayList();       //储存词法分析的Token序列
        //public string[] TokenString;                      //储存词法分析的Token的字符串数组
        public ArrayList MyLexicalErrorList = new ArrayList();       //储存词法分析中的错误
        public ArrayList MyLexicalSymbolList = new ArrayList();      //存储符号表序列
        public List<Token> sementicsTokens = new List<Token>();
        public int z = 0;
        char[] Code = new char[10 * 1024];               //存放代码的缓冲区
        int index = 0;                //当前代码的索引
        int lineCount = 1;               //表示源代码的行数,初始化为1
        bool isWordEnd = true;          //表示当前的词是否判断结束
        IdentifierType identifierType = IdentifierType.Void;

        ~CLexicalAnalysis()
        {
            this.MyTokenList.Clear();
            this.sementicsTokens.Clear();
            
        }

        //词法分析
        public void BeginAnalysis(RichTextBox codeBox)
        {
            int SymbolNum = 1;
            this.MyTokenList.Clear();
            this.sementicsTokens.Clear();
            this.MyLexicalErrorList.Clear();
            this.MyLexicalSymbolList.Clear();
            this.InitCharInformation(codeBox);
            index = 0;
            lineCount = 1;
            while (Code[index] != '\0')
            {
                char charTemp = Code[index];

                if (CLUtility.IsInvalidChars(charTemp))
                {
                    this.isWordEnd = true;
                }
                //回车符
                else if(charTemp == '\n')
                {
                    lineCount++;
                    this.isWordEnd = true;
                }
                #region 数字
                else if (CLUtility.IsDigit(charTemp))
                {
                    String temp = charTemp.ToString();
                    do
                    {
                        charTemp = Code[++index];
                        if (CLUtility.IsDigit(charTemp) || CLUtility.IsLetter(charTemp) || (charTemp == '.'))
                            temp += charTemp;
                        else
                            index--;

                    } while (CLUtility.IsDigit(charTemp) || CLUtility.IsLetter(charTemp) || (charTemp == '.'));

                    if (CLUtility.IsBiInteger(temp))
                    {
                        MyLexicalToken token = new MyLexicalToken();
                        token.Type = ElementType.BiInteger;
                        token.Name = temp;
                        token.LineCount = lineCount;
                        token.Code = CLUtility.BiNum;
                        this.MyTokenList.Add(token);
                        sementicsTokens.Add(new Token(z++, temp, "CONST_INT"));

                    }
                    else if (CLUtility.IsOctInteger(temp))
                    {
                        MyLexicalToken token = new MyLexicalToken();
                        token.Type = ElementType.OctInteger;
                        token.Name = temp;
                        token.LineCount = lineCount;
                        token.Code = CLUtility.OctNum;
                        this.MyTokenList.Add(token);
                        sementicsTokens.Add(new Token(z++, temp, "CONST_INT"));
                    }
                    else if (CLUtility.IsFloat(temp))
                    {
                        MyLexicalToken token = new MyLexicalToken();
                        token.Type = ElementType.Float;
                        token.Name = temp;
                        token.LineCount = lineCount;
                        token.Code = CLUtility.FloatNum;
                        this.MyTokenList.Add(token);
                        sementicsTokens.Add(new Token(z++, temp, "CONST_FLOAT"));
                    }
                    else if (CLUtility.IsHexInteger(temp))
                    {
                        MyLexicalToken token = new MyLexicalToken();
                        token.Type = ElementType.HexInteger;
                        token.Name = temp;
                        token.LineCount = lineCount;
                        token.Code = CLUtility.HexNum;
                        this.MyTokenList.Add(token);
                        sementicsTokens.Add(new Token(z++, temp, "CONST_INT"));
                    }
                    else if (CLUtility.IsDecInteger(temp))
                    {
                        MyLexicalToken token = new MyLexicalToken();
                        token.Type = ElementType.DecInteger;
                        token.Name = temp;
                        token.LineCount = lineCount;
                        token.Code = CLUtility.DecNum;
                        this.MyTokenList.Add(token);
                        sementicsTokens.Add(new Token(z++, temp, "CONST_INT"));
                    }
                    else
                    {
                        LexicalError theError = new LexicalError();
                        theError.Type = ErrorType.UnkownSymbols;
                        theError.Value = temp;
                        theError.LineCount = lineCount;
                        MyLexicalErrorList.Add(theError);
                    }

                }
                #endregion 数字

                #region 字母
                else if (CLUtility.IsLetter(charTemp))
                {
                    string temp = charTemp.ToString();
                    do
                    {
                        charTemp = Code[++index];
                        if (CLUtility.IsDigit(charTemp) || CLUtility.IsLetter(charTemp))
                            temp = temp + charTemp;
                        else
                            index--;
                    } while (CLUtility.IsLetter(charTemp) || CLUtility.IsDigit(charTemp));

                    //得到一个token
                    MyLexicalToken token = new MyLexicalToken();
                    token.Name = temp;

                    //不是关键字要加入符号表
                    if (!CLUtility.IsKeyWord(temp))
                    {

                        MyLexicalSymbol theSymbol = new MyLexicalSymbol();
                        theSymbol.Value = temp;
                        
                        theSymbol.Type = IdentifierType.Void;
                        if (identifierType != IdentifierType.Void)
                        {
                            theSymbol.Type = identifierType;
                        }
                        theSymbol.LineCount = this.lineCount;

                        if ((IsInLexicalSymbolList(temp)) == -1)
                        {
                            theSymbol.Num = SymbolNum++;
                            token.Name = temp;
                            token.Others = "" + theSymbol.Num;
                            MyLexicalSymbolList.Add(theSymbol);

                        }
                        else
                        {
                            token.Others = "" + (IsInLexicalSymbolList(temp)+1);
                        }
                    }

                    //是关键字
                    if (CLUtility.IsKeyWord(temp))
                    {
                        token.Type = ElementType.Keyword;
                        if (temp == "char")
                        {
                            identifierType = IdentifierType.Char;
                        }
                        else if (temp == "int")
                        {
                            identifierType = IdentifierType.Int;
                        }
                        else if (temp == "float")
                        {
                            identifierType = IdentifierType.Float;
                        }
                        else if (temp == "double")
                        {
                            identifierType = IdentifierType.Double;
                        }
                        token.Code = CLUtility.KeyWordBase + CLUtility.GetIndexKeyWord(temp);
                        sementicsTokens.Add(new Token(z++,temp.ToUpper(),temp.ToUpper()));
                    }
                    //是标识符
                    else
                    {
                        token.Type = ElementType.Identifier;
                        token.Code = CLUtility.IdBase;
                        sementicsTokens.Add(new Token(z++,temp,"IDN"));
                        //token.Name += " No=" + SymbolNum;
                    }

                    
                    token.LineCount = lineCount;
                    this.MyTokenList.Add(token);

                    
                }
                #endregion 

                #region 字符
                //字符 ‘
                else if (charTemp == '\'')
                {
                    charTemp = Code[++index];
                    string temp = charTemp.ToString();
                    charTemp = Code[++index];
                    //字符封闭
                    if (charTemp == '\'')
                    {
                        MyLexicalToken token = new MyLexicalToken();
                        token.Type = ElementType.Char;
                        token.Name = temp;
                        token.LineCount = lineCount;
                        token.Code = CLUtility.CharCode;
                        this.MyTokenList.Add(token);
                    }
                    //字符未封闭出错
                    else
                    {
                        temp = temp + charTemp;
                        LexicalError theError = new LexicalError();
                        theError.Type = ErrorType.UnkownSymbols;
                        theError.Value = temp;
                        theError.LineCount = lineCount;
                        MyLexicalErrorList.Add(theError);
                    }
                }
                #endregion

                #region 字符串
                else if (charTemp == '\"')
                {
                    bool isAbnormal = false;
                    this.isWordEnd = false;
                    string temp = "";
                    do
                    {
                        charTemp = Code[++index];
                        //空串
                        if (charTemp == '\"')
                        {
                            this.isWordEnd = true;
                        }
                        //未结束的字符串换行，出错
                        else if (charTemp == '\n')
                        {
                            lineCount++;
                            this.isWordEnd = true;
                            isAbnormal = true;
                        }
                        else
                        {
                            temp = temp + charTemp;
                        }
                    } while (this.isWordEnd == false);

                    //没有错误，正常加入字符串
                    if (isAbnormal == false)
                    {
                        MyLexicalToken token = new MyLexicalToken();
                        token.Type = ElementType.String;
                        token.Name = temp;
                        token.LineCount = lineCount;
                        token.Code = CLUtility.StringCode;
                        this.MyTokenList.Add(token);
                    }
                    //有错误，加入ErrorList
                    else
                    {
                        LexicalError theError = new LexicalError();
                        theError.Type = ErrorType.UnkownSymbols;
                        theError.Value = temp;
                        theError.LineCount = lineCount;
                        MyLexicalErrorList.Add(theError);
                    }
                }
                #endregion
                
                #region 分隔符
                else if (CLUtility.IsSeparatorChar(charTemp))
                {
                    if (charTemp == ';')
                    {
                        identifierType = IdentifierType.Void;
                    }
                    //加入token
                    MyLexicalToken token = new MyLexicalToken();
                    token.Type = ElementType.Separator;
                    token.Name = charTemp.ToString();
                    token.LineCount = lineCount;
                    token.Code = CLUtility.SeparatorBase + CLUtility.GetIndexSeparator(charTemp);
                    this.MyTokenList.Add(token);
                    sementicsTokens.Add(new Token(z++, charTemp.ToString(), charTemp.ToString()));
                }
                #endregion

                #region‘+,-,&’号
                else if (charTemp == '+' || charTemp == '-' || charTemp == '&' || charTemp == '|')
                {
                    string temp = charTemp + "";
                    index++;
                    charTemp = Code[index];
                    //为运算符 ++ -- && || += -= &= |=
                    if (charTemp.ToString() == temp || charTemp == '=')
                    {
                        temp = temp + charTemp;
                        sementicsTokens.Add(new Token(z++,temp,temp));
                    }
                    //普通运算符，回退
                    else
                    {
                        index--;
                        sementicsTokens.Add(new Token(z++, temp, temp));
                    }

                    //将运算符元素加入到链表中
                    MyLexicalToken token = new MyLexicalToken();
                    token.Type = ElementType.Operator;
                    token.Name = temp;
                    token.LineCount = lineCount;
                    token.Code = CLUtility.OperatorBase + CLUtility.GetIndexOperator(charTemp);
                    this.MyTokenList.Add(token);
                }
                #endregion

                #region '*,%,=,<,>,!'号
                else if (charTemp == '*' || charTemp == '%'
                    || charTemp == '=' || charTemp == '<'
                    || charTemp == '>' || charTemp == '!')
                {
                    string temp = charTemp + "";
                    index++;
                    charTemp = Code[index];
                    //是否是这些符号后接着一个=
                    if (charTemp == '=')
                        temp = temp + charTemp;
                    //回退
                    else
                        index--;

                    //将元素加入到链表中
                    MyLexicalToken token = new MyLexicalToken();
                    token.Type = ElementType.Operator;
                    token.Name = temp;
                    token.LineCount = lineCount;
                    token.Code = CLUtility.OperatorBase + CLUtility.GetIndexOperator(charTemp);
                    this.MyTokenList.Add(token);
                    sementicsTokens.Add(new Token(z++,temp,temp));
                }
                #endregion

                #region  '/'
                else if (charTemp == '/')
                {
                    string temp = charTemp.ToString();
                    MyLexicalToken token = new MyLexicalToken();
                    charTemp = Code[++index];
                    this.isWordEnd = true;

                    if (charTemp != '/' && charTemp != '*')
                    {

                        // 是运算符/=
                        if (charTemp == '=')
                        {
                            temp = temp + charTemp;
                            this.isWordEnd = true;
                            sementicsTokens.Add(new Token(z++,temp,temp));
                        }
                        //是运算符/，回退
                        else
                        {
                            index--;
                            this.isWordEnd = true;
                             sementicsTokens.Add(new Token(z++,temp,temp));
                        }
                        token.Type = ElementType.Operator;
                        token.Name = temp;
                        token.Code = CLUtility.OperatorBase + CLUtility.GetIndexOperator(charTemp);
                        token.LineCount = this.lineCount;
                        this.MyTokenList.Add(token);
                    }

                    //是注释
                    else
                    {
                        //单行注释
                        if (charTemp == '/')
                        {
                            this.isWordEnd = false;
                            while ((charTemp = Code[++index]) != '\n') ;
                            this.isWordEnd = true;
                            lineCount++;
                        }
                        //多行注释
                        else if (charTemp == '*')
                        {
                            this.isWordEnd = false;
                            do
                            {
                                charTemp = Code[++index];
                                if (charTemp == '\n')
                                    lineCount++;
                                //进入下一个状态
                                if (charTemp == '*')
                                {
                                    charTemp = Code[++index];
                                    if (charTemp == '/')
                                    {
                                        this.isWordEnd = true;
                                        break;
                                    }
                                    else
                                        index--;
                                }
                            } while (charTemp != '\0');
                            //注释不封闭，出错
                            if (this.isWordEnd == false)
                            {
                                LexicalError theError = new LexicalError();
                                theError.Type = ErrorType.AbnormalEnd;
                                theError.Value = "Unclosed Comment";
                                theError.LineCount = lineCount;
                                MyLexicalErrorList.Add(theError);
                            }
                        }

                    }

                }
                #endregion

                          
                
                else
                {
                }

                index++;
            }
            #region
            //给TokenString 赋值
            /*this.TokenString = new string[this.MyTokenList.Count];
            int i = 0;
            foreach (MyLexicalToken theToken in this.MyTokenList)
            {
                this.TokenString[i] = theToken.Name;
                i++;
            }*/
            #endregion

            //Code[0] = '\0';
        }

        private void InitCharInformation(RichTextBox codeBox)
        {
            int count = 0;
            char[] codeStream = codeBox.Text.ToCharArray();
            for (; count < codeStream.Length; count++)
            {
                Code[count] = codeStream[count];
            }
        }

        


        //判断某变量是否在符号表中，在则返回索引，否则返回-1
        private int IsInLexicalSymbolList(string symbol)
        {
            int i = 0;
            foreach (MyLexicalSymbol theSymbol in this.MyLexicalSymbolList)
            {
                if (symbol == theSymbol.Value)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

    }
}
