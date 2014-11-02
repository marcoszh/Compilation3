using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MyCompilation
{
    public partial class LabeledRichTextBox : UserControl
    {
        int _currentLine = 0;
        public LabeledRichTextBox()
        {
            InitializeComponent();
            numberLabel.Font = new Font(richTextBoxMain.Font.FontFamily, richTextBoxMain.Font.Size);// + 0.1f);
        }

        private void MyRichTextBox_Load(object sender, EventArgs e)
        {

        }
        public int CurrentLine
        {
            get
            {
                return _currentLine;
            }
            set
            {
                _currentLine = value;
            }
        }
        private void updateNumberLabel()
        {
            //we get index of first visible char and number of first visible line
            Point pos = new Point(0, 0);
            int firstIndex = richTextBoxMain.GetCharIndexFromPosition(pos);
            int firstLine = richTextBoxMain.GetLineFromCharIndex(firstIndex);

            //now we get index of last visible char and number of last visible line
            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = richTextBoxMain.GetCharIndexFromPosition(pos);
            int lastLine = richTextBoxMain.GetLineFromCharIndex(lastIndex);
            int myStart = this.richTextBoxMain.SelectionStart;
            int myLine = this.richTextBoxMain.GetLineFromCharIndex(myStart) + 1;
            pos = richTextBoxMain.GetPositionFromCharIndex(lastIndex);
            if (lastIndex > _currentLine || lastIndex < _currentLine)
            {
                //finally, renumber label
                numberLabel.Text = "";
                for (int i = firstLine; i <= lastLine + 1; i++)
                {
                    numberLabel.Text += i + 1 + "\n";
                }
            }
            _currentLine = lastIndex;
            //this is point position of last visible char, we'll use its Y value for calculating numberLabel size 
        }

        private void richTextBoxMain_TextChanged(object sender, EventArgs e)
        {
            updateNumberLabel();
        }

        private void richTextBoxMain_VScroll(object sender, EventArgs e)
        {
            //move location of numberLabel for amount of pixels caused by scrollbar
            int d = richTextBoxMain.GetPositionFromCharIndex(0).Y % (richTextBoxMain.Font.Height + 1);
            numberLabel.Location = new Point(0, d);
            updateNumberLabel();
        }

        private void richTextBoxMain_Resize(object sender, EventArgs e)
        {
            richTextBoxMain_VScroll(null, null);
        }

        private void richTextBoxMain_FontChanged(object sender, EventArgs e)
        {
            updateNumberLabel();
            richTextBoxMain_VScroll(null, null);
        }
    }
}
