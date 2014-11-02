namespace MyCompilation
{
    partial class LabeledRichTextBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxMain = new System.Windows.Forms.RichTextBox();
            this.numberLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBoxMain
            // 
            this.richTextBoxMain.AcceptsTab = true;
            this.richTextBoxMain.BackColor = System.Drawing.SystemColors.Desktop;
            this.richTextBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxMain.Font = new System.Drawing.Font("Verdana", 9F);
            this.richTextBoxMain.ForeColor = System.Drawing.Color.Yellow;
            this.richTextBoxMain.Location = new System.Drawing.Point(32, 0);
            this.richTextBoxMain.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBoxMain.Name = "richTextBoxMain";
            this.richTextBoxMain.Size = new System.Drawing.Size(438, 363);
            this.richTextBoxMain.TabIndex = 0;
            this.richTextBoxMain.Text = "";
            this.richTextBoxMain.VScroll += new System.EventHandler(this.richTextBoxMain_VScroll);
            this.richTextBoxMain.FontChanged += new System.EventHandler(this.richTextBoxMain_FontChanged);
            this.richTextBoxMain.TextChanged += new System.EventHandler(this.richTextBoxMain_TextChanged);
            this.richTextBoxMain.Resize += new System.EventHandler(this.richTextBoxMain_Resize);
            // 
            // numberLabel
            // 
            this.numberLabel.AutoSize = true;
            this.numberLabel.Location = new System.Drawing.Point(0, 0);
            this.numberLabel.Margin = new System.Windows.Forms.Padding(0);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(0, 17);
            this.numberLabel.TabIndex = 1;
            // 
            // LabeledRichTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.richTextBoxMain);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LabeledRichTextBox";
            this.Size = new System.Drawing.Size(525, 364);
            this.Load += new System.EventHandler(this.MyRichTextBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox richTextBoxMain;
        private System.Windows.Forms.Label numberLabel;
    }
}
