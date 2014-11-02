using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MyCompilation
{
    public partial class STableForm : Form
    {
        //public ArrayList tokens = new ArrayList();
        //public ArrayList productions = new ArrayList();
        public Dictionary<string, Dictionary<string, ArrayList>> predictionTable;
        public STableForm()
        {
            InitializeComponent();
            //updateTable();
        }

        public void updateTable()
        {
            int i=1;
            Console.WriteLine("预测分析表");
            predictionListView.BeginUpdate();
            foreach (string s in predictionTable.Keys)
            {
                foreach (string s1 in predictionTable[s].Keys)
                {
                    ListViewItem item = new ListViewItem(i.ToString());
                    //item.SubItems.Add(i.ToString());
                    item.SubItems.Add(s);
                    item.SubItems.Add(s1);

                    string temp = "";
                    foreach (string s2 in predictionTable[s][s1])
                    {
                        temp += ("["+s2 + "] ");
                    }
                    item.SubItems.Add(temp);
                    Console.WriteLine(""+i+" "+s+" "+s1+" "+temp);
                    predictionListView.Items.Add(item);
                    i++;
                    //setWidth();
                }
            }
            predictionListView.EndUpdate();

        }

        public void setWidth()
        {
            this.NoColum.Width = -2;
            this.NextColum.Width = -1;
            this.InputColum.Width = -1;
            this.LeftColum.Width = -1;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            predictionListView.Items.Clear();
            updateTable();
            setWidth();
        }
    }
}
