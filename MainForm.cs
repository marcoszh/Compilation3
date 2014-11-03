using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyCompilation;

namespace MyCompilation
{
    public partial class MainForm : Form
    {
        string FilePath;
        CLexicalAnalysis myLexicalAnalysis = new CLexicalAnalysis();
        CParsing mParsing = new CParsing();
        STableForm stForm = new STableForm();
        //Semantics mSementics = new Semantics();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.FilePath = "";
            this.openFileDialog.Filter = "(*.c)|*.c|(*.cpp)|*.cpp";
            this.openFileDialog.Title = "Open C file";
        }

        //打开文件的响应
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.FilePath = this.openFileDialog.FileName;
                this.myRichTextBoxCode.richTextBoxMain.Text = "";
                this.myRichTextBoxCode.richTextBoxMain.LoadFile(this.FilePath, RichTextBoxStreamType.PlainText);
            }

        }

        //退出程序的响应
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //菜单里词法分析的响应
        private void LAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listViewError.Items.Clear();
            this.listViewSymbol.Items.Clear();
            this.listViewToken.Items.Clear();
            if (this.myRichTextBoxCode.richTextBoxMain.Text != "")
            {
                myLexicalAnalysis.BeginAnalysis(this.myRichTextBoxCode.richTextBoxMain);     //词法分析
                //this.myRichTextBoxCode.richTextBoxMain.Text = "";
                //添加Error序列
                foreach (LexicalError theError in myLexicalAnalysis.MyLexicalErrorList)
                {
                    string errorMessage = "";
                    ListViewItem errorItem = new ListViewItem(theError.Value);
                    //errorItem.SubItems[0].Text = theError.Value;       //错误值
                    if (theError.Type == ErrorType.AbnormalEnd)
                        errorMessage = "Abnormal Ending";
                    else if (theError.Type == ErrorType.UnkownSymbols)
                        errorMessage = "Unrecognized Symbol";
                    else
                    {
                    }
                    errorItem.SubItems.Add(errorMessage);
                    errorItem.SubItems.Add(theError.LineCount.ToString());
                    listViewError.Items.Add(errorItem);
                }

                //添加Token序列
                foreach (MyLexicalToken theToken in myLexicalAnalysis.MyTokenList)
                {
                    ListViewItem tokenItem = new ListViewItem(theToken.Name);
                    //tokenItem.SubItems[0].Text = theToken.Name;       //token值
                    tokenItem.SubItems.Add(theToken.Type.ToString());
                    tokenItem.SubItems.Add(theToken.LineCount.ToString());
                    tokenItem.SubItems.Add(theToken.Code.ToString());
                    //if()
                    tokenItem.SubItems.Add(theToken.Others);
                    listViewToken.Items.Add(tokenItem);
                   
                }


                //添加SymbolList序列
                foreach (MyLexicalSymbol theSymbol in myLexicalAnalysis.MyLexicalSymbolList)
                {
                    ListViewItem tokenItem = new ListViewItem(theSymbol.Value);
                    //tokenItem.SubItems[0].Text = theSymbol.Value;       //符号值
                    tokenItem.SubItems.Add(theSymbol.Type.ToString());
                    tokenItem.SubItems.Add(theSymbol.LineCount.ToString());
                    tokenItem.SubItems.Add(theSymbol.Num.ToString());
                    this.listViewSymbol.Items.Add(tokenItem);
                }
            }

        }
    

        private string IntArrayToString(int[] intArray)
        {
            string myArrayStirng = "[";

            for (int i = 0; i < intArray.Length; i++)
            {
                myArrayStirng = myArrayStirng + intArray[i];

                if (i != intArray.Length - 1)
                {
                    myArrayStirng = myArrayStirng + ",";
                }
            }
            myArrayStirng = myArrayStirng + "]";
            return myArrayStirng;
        }

        private void listViewError_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = listViewError.SelectedItems[0].Index;
            MessageBox.Show("" + selected);
            //int lineNum = listViewError.SubItems(selected);
        }

        //生成预测分析表按钮被点击后进行的操作
        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mParsing.productions = CSUtility.readProductionFile("Grammar.txt");

            mParsing.charTNT();

            Console.WriteLine("非终结符");
            foreach(string s in mParsing.ntChars.Keys)
            {
                Console.Write(s+" , ");
            }
            Console.WriteLine("");
            Console.WriteLine("终结符");
            foreach (string s in mParsing.tChars.Keys)
            {
                Console.Write(s+" , ");
            }
            Console.WriteLine("");

            foreach (string s in mParsing.tChars.Keys)
            {
                mParsing.setTCharFirst(s);
            }

            foreach(string s in mParsing.ntChars.Keys)
            {
                mParsing.initNTCharFirst(s);
            }

            while (mParsing.isChanged == true)
            {
                mParsing.isChanged = false;
                foreach (string s in mParsing.ntChars.Keys)
                {
                    mParsing.addNTCharFirst(s);
                }
            }

            //去掉FIRST中的空串
            foreach (string s in mParsing.ntChars.Keys)
            {
                mParsing.ntChars[s].First.Remove("$");
            }

            foreach (string s in mParsing.tChars.Keys)
            {
                Console.Write("FIRST(" + s + ") = ");
                foreach (string s1 in mParsing.tChars[s].First)
                {
                    Console.Write(s1+" , ");
                }
                Console.WriteLine("");
            }

            foreach (string s in mParsing.ntChars.Keys)
            {
                Console.Write("FIRST(" + s + ") = ");
                foreach (string s1 in mParsing.ntChars[s].First)
                {
                    Console.Write(s1 + " , ");
                }
                Console.WriteLine("");
            }

            mParsing.initFollow();
            mParsing.isChanged = true;
            while (mParsing.isChanged == true)
            {
                mParsing.isChanged = false;
                mParsing.addNTFollow();
            }

            foreach (string s in mParsing.ntChars.Keys)
            {
                Console.Write("FOLLOW(" + s + ") = ");
                foreach (string s1 in mParsing.ntChars[s].Follow)
                {
                    Console.Write(s1+" , ");
                }
                Console.WriteLine("");
            }

            mParsing.setSelectForProduction();

            foreach (Production production in mParsing.productions)
            {
                string temp = "";
                foreach (string ss in production.Right)
                    temp += (ss + " ");
                Console.Write("SELECT(" + production.Left + "->" + temp + ") = ");
                foreach (string s1 in production.Select)
                {
                    Console.Write(s1+" , ");
                }
                Console.WriteLine("");
            }

            mParsing.setPredictionTable();

            mParsing.setSync();
      
            //stForm.tokens = myLexicalAnalysis.MyTokenList;
            //stForm.productions = mParsing.productions;
            stForm.predictionTable = mParsing.predictionTable;
            stForm.Show();
            stForm.updateTable();
            stForm.setWidth();

            //mParsing.tokens = myLexicalAnalysis.MyTokenList;
            
        }

        private void SyntacticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mParsing.tokens.Clear();
            foreach (MyLexicalToken token in myLexicalAnalysis.MyTokenList)
            {
                mParsing.tokens.Add(token);
            }

            foreach (Token t in myLexicalAnalysis.sementicsTokens)
            {
                Semantics.tokenlist.Add(new Token(Semantics.tokenlist.Count, t.val, t.type));
                Console.WriteLine(t.val+" "+t.type);
            }
            Semantics.tokenlist.Add(new Token(Semantics.tokenlist.Count, "#", "#"));
            Semantics.GetSegment(Semantics.tokenlist);
            Semantics.S(Semantics.tokenlist);
            showSF();


            MyLexicalToken endToken = new MyLexicalToken();
            endToken.Code = -1;
            endToken.LineCount = -1;
            endToken.Name = "#";
            endToken.Others = "";
            endToken.Type = ElementType.None;
            mParsing.tokens.Add(endToken);
            mParsing.LL1Analysis(((Production)mParsing.productions[0]).Left);
            //mParsing.LL1Analysis("program");
        }

        public void showSF()
        {
            SemanticsForm sf = new SemanticsForm();
            foreach (string r in Semantics.tresult)
            {
                sf.AppendSym(r + "\n");
            }
            foreach(string r in Semantics.rresult)
            {
                sf.Append3AC(r + "\n");
            }
            foreach (string r in Semantics.eresult)
            {
                sf.AppendErr(r + "\n");
            }
            sf.Show();
        }


    }
}
