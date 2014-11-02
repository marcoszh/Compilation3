using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyCompilation
{
    public partial class SemanticsForm : Form
    {
        public SemanticsForm()
        {
            InitializeComponent();
            this.richTextBox1.Text = "";
            this.richTextBox2.Text = "";
            this.richTextBox3.Text = "";
        }

        public void Append3AC(string text)
        {
            this.richTextBox1.AppendText(text);
        }

        public void AppendErr(string text)
        {
            this.richTextBox2.AppendText(text);
        }

        public void AppendSym(string text)
        {
            this.richTextBox3.AppendText(text);
        }


    }
}
