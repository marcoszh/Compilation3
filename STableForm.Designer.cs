namespace MyCompilation
{
    partial class STableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.predictionListView = new DBListView();
            this.NoColum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeftColum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InputColum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NextColum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.refreshButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Prediction Table";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.predictionListView);
            this.panel1.Location = new System.Drawing.Point(13, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(828, 308);
            this.panel1.TabIndex = 1;
            // 
            // predictionListView
            // 
            this.predictionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NoColum,
            this.LeftColum,
            this.InputColum,
            this.NextColum});
            this.predictionListView.GridLines = true;
            this.predictionListView.Location = new System.Drawing.Point(0, 14);
            this.predictionListView.Name = "predictionListView";
            this.predictionListView.Size = new System.Drawing.Size(824, 294);
            this.predictionListView.TabIndex = 0;
            this.predictionListView.UseCompatibleStateImageBehavior = false;
            this.predictionListView.View = System.Windows.Forms.View.Details;
            // 
            // NoColum
            // 
            this.NoColum.Text = "No.";
            this.NoColum.Width = 25;
            // 
            // LeftColum
            // 
            this.LeftColum.Text = "Left";
            this.LeftColum.Width = 25;
            // 
            // InputColum
            // 
            this.InputColum.Text = "Input";
            this.InputColum.Width = 25;
            // 
            // NextColum
            // 
            this.NextColum.Text = "Next";
            this.NextColum.Width = 25;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(677, 8);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // STableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 348);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "STableForm";
            this.Text = "STableForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView predictionListView;
        private System.Windows.Forms.ColumnHeader NoColum;
        private System.Windows.Forms.ColumnHeader LeftColum;
        private System.Windows.Forms.ColumnHeader InputColum;
        private System.Windows.Forms.ColumnHeader NextColum;
        private System.Windows.Forms.Button refreshButton;
    }
}