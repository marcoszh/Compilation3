namespace MyCompilation
{
    public partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SyntacticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelToken = new System.Windows.Forms.Panel();
            this.listViewSymbol = new System.Windows.Forms.ListView();
            this.columnHeaderSymbolValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSymbolType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSymbolLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewToken = new System.Windows.Forms.ListView();
            this.columnHeaderTokenValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTokenType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTokenLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderOthers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.myRichTextBoxCode = new MyCompilation.LabeledRichTextBox();
            this.listViewError = new System.Windows.Forms.ListView();
            this.columnHeaderErrorValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderErrorType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderErrorLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.panelToken.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.AnalysisToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(793, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // AnalysisToolStripMenuItem
            // 
            this.AnalysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LAToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.SyntacticToolStripMenuItem});
            this.AnalysisToolStripMenuItem.Name = "AnalysisToolStripMenuItem";
            this.AnalysisToolStripMenuItem.Size = new System.Drawing.Size(46, 21);
            this.AnalysisToolStripMenuItem.Text = "Tool";
            // 
            // LAToolStripMenuItem
            // 
            this.LAToolStripMenuItem.Name = "LAToolStripMenuItem";
            this.LAToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.LAToolStripMenuItem.Text = "Lexical Analysis";
            this.LAToolStripMenuItem.Click += new System.EventHandler(this.LAToolStripMenuItem_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.generateToolStripMenuItem.Text = "Generate Prediction Table";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // SyntacticToolStripMenuItem
            // 
            this.SyntacticToolStripMenuItem.Name = "SyntacticToolStripMenuItem";
            this.SyntacticToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.SyntacticToolStripMenuItem.Text = "Syntactic Analysis";
            this.SyntacticToolStripMenuItem.Click += new System.EventHandler(this.SyntacticToolStripMenuItem_Click);
            // 
            // panelToken
            // 
            this.panelToken.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelToken.Controls.Add(this.listViewSymbol);
            this.panelToken.Controls.Add(this.listViewToken);
            this.panelToken.Location = new System.Drawing.Point(492, 50);
            this.panelToken.Margin = new System.Windows.Forms.Padding(0);
            this.panelToken.Name = "panelToken";
            this.panelToken.Size = new System.Drawing.Size(318, 466);
            this.panelToken.TabIndex = 1;
            // 
            // listViewSymbol
            // 
            this.listViewSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSymbol.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSymbolValue,
            this.columnHeaderSymbolType,
            this.columnHeaderSymbolLine,
            this.columnHeaderNum});
            this.listViewSymbol.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewSymbol.GridLines = true;
            this.listViewSymbol.Location = new System.Drawing.Point(0, 326);
            this.listViewSymbol.Margin = new System.Windows.Forms.Padding(0);
            this.listViewSymbol.Name = "listViewSymbol";
            this.listViewSymbol.Size = new System.Drawing.Size(299, 138);
            this.listViewSymbol.TabIndex = 2;
            this.listViewSymbol.UseCompatibleStateImageBehavior = false;
            this.listViewSymbol.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderSymbolValue
            // 
            this.columnHeaderSymbolValue.Text = "Value";
            this.columnHeaderSymbolValue.Width = 99;
            // 
            // columnHeaderSymbolType
            // 
            this.columnHeaderSymbolType.Text = "Type";
            this.columnHeaderSymbolType.Width = 77;
            // 
            // columnHeaderSymbolLine
            // 
            this.columnHeaderSymbolLine.Text = "Line";
            this.columnHeaderSymbolLine.Width = 71;
            // 
            // columnHeaderNum
            // 
            this.columnHeaderNum.Text = "Num";
            // 
            // listViewToken
            // 
            this.listViewToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewToken.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTokenValue,
            this.columnHeaderTokenType,
            this.columnHeaderTokenLine,
            this.columnHeaderCode,
            this.columnHeaderOthers});
            this.listViewToken.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewToken.GridLines = true;
            this.listViewToken.Location = new System.Drawing.Point(3, 1);
            this.listViewToken.Name = "listViewToken";
            this.listViewToken.Size = new System.Drawing.Size(296, 322);
            this.listViewToken.TabIndex = 1;
            this.listViewToken.UseCompatibleStateImageBehavior = false;
            this.listViewToken.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderTokenValue
            // 
            this.columnHeaderTokenValue.Text = "Word";
            this.columnHeaderTokenValue.Width = 73;
            // 
            // columnHeaderTokenType
            // 
            this.columnHeaderTokenType.Text = "Note";
            this.columnHeaderTokenType.Width = 64;
            // 
            // columnHeaderTokenLine
            // 
            this.columnHeaderTokenLine.Text = "Line";
            this.columnHeaderTokenLine.Width = 45;
            // 
            // columnHeaderCode
            // 
            this.columnHeaderCode.Text = "Code";
            this.columnHeaderCode.Width = 50;
            // 
            // columnHeaderOthers
            // 
            this.columnHeaderOthers.Text = "Others";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(557, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lexical Analysis Result";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Location = new System.Drawing.Point(9, 50);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.myRichTextBoxCode);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listViewError);
            this.splitContainer.Panel2.Enabled = false;
            this.splitContainer.Size = new System.Drawing.Size(478, 466);
            this.splitContainer.SplitterDistance = 322;
            this.splitContainer.TabIndex = 2;
            // 
            // myRichTextBoxCode
            // 
            this.myRichTextBoxCode.BackColor = System.Drawing.Color.Silver;
            this.myRichTextBoxCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myRichTextBoxCode.CurrentLine = 0;
            this.myRichTextBoxCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myRichTextBoxCode.ImeMode = System.Windows.Forms.ImeMode.On;
            this.myRichTextBoxCode.Location = new System.Drawing.Point(0, 0);
            this.myRichTextBoxCode.Margin = new System.Windows.Forms.Padding(0);
            this.myRichTextBoxCode.Name = "myRichTextBoxCode";
            this.myRichTextBoxCode.Size = new System.Drawing.Size(478, 323);
            this.myRichTextBoxCode.TabIndex = 0;
            this.myRichTextBoxCode.TabStop = false;
            // 
            // listViewError
            // 
            this.listViewError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewError.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderErrorValue,
            this.columnHeaderErrorType,
            this.columnHeaderErrorLine});
            this.listViewError.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewError.ForeColor = System.Drawing.Color.Red;
            this.listViewError.GridLines = true;
            this.listViewError.Location = new System.Drawing.Point(-2, 0);
            this.listViewError.Margin = new System.Windows.Forms.Padding(1);
            this.listViewError.Name = "listViewError";
            this.listViewError.Size = new System.Drawing.Size(480, 138);
            this.listViewError.TabIndex = 0;
            this.listViewError.TabStop = false;
            this.listViewError.UseCompatibleStateImageBehavior = false;
            this.listViewError.View = System.Windows.Forms.View.Details;
            this.listViewError.SelectedIndexChanged += new System.EventHandler(this.listViewError_SelectedIndexChanged);
            // 
            // columnHeaderErrorValue
            // 
            this.columnHeaderErrorValue.Text = "Value";
            this.columnHeaderErrorValue.Width = 147;
            // 
            // columnHeaderErrorType
            // 
            this.columnHeaderErrorType.Text = "Error type";
            this.columnHeaderErrorType.Width = 238;
            // 
            // columnHeaderErrorLine
            // 
            this.columnHeaderErrorLine.Text = "Line";
            this.columnHeaderErrorLine.Width = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            this.label2.Location = new System.Drawing.Point(219, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Code";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 525);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelToken);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compilation Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelToken.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.Panel panelToken;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView listViewError;
        private System.Windows.Forms.ToolStripMenuItem AnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LAToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeaderErrorValue;
        private System.Windows.Forms.ColumnHeader columnHeaderErrorType;
        private System.Windows.Forms.ColumnHeader columnHeaderErrorLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewToken;
        private System.Windows.Forms.ColumnHeader columnHeaderTokenValue;
        private System.Windows.Forms.ColumnHeader columnHeaderTokenType;
        private System.Windows.Forms.ColumnHeader columnHeaderTokenLine;
        private LabeledRichTextBox myRichTextBoxCode;
        private System.Windows.Forms.ListView listViewSymbol;
        private System.Windows.Forms.ColumnHeader columnHeaderSymbolValue;
        private System.Windows.Forms.ColumnHeader columnHeaderSymbolType;
        private System.Windows.Forms.ColumnHeader columnHeaderSymbolLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeaderCode;
        private System.Windows.Forms.ColumnHeader columnHeaderNum;
        private System.Windows.Forms.ColumnHeader columnHeaderOthers;
        private System.Windows.Forms.ToolStripMenuItem SyntacticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
    }
}

