namespace Dll_Validator
{
    partial class DllValidator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DllValidator));
            this.folderTextBox = new System.Windows.Forms.TextBox();
            this.folderDialogButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.stationsListView = new System.Windows.Forms.ListView();
            this.runButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SitesCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // folderTextBox
            // 
            this.folderTextBox.Location = new System.Drawing.Point(21, 94);
            this.folderTextBox.Name = "folderTextBox";
            this.folderTextBox.Size = new System.Drawing.Size(356, 20);
            this.folderTextBox.TabIndex = 0;
            // 
            // folderDialogButton
            // 
            this.folderDialogButton.Location = new System.Drawing.Point(383, 95);
            this.folderDialogButton.Name = "folderDialogButton";
            this.folderDialogButton.Size = new System.Drawing.Size(31, 19);
            this.folderDialogButton.TabIndex = 1;
            this.folderDialogButton.Text = "...";
            this.folderDialogButton.UseVisualStyleBackColor = true;
            this.folderDialogButton.Click += new System.EventHandler(this.folderDialogButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "New Dll";
            // 
            // stationsListView
            // 
            this.stationsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.version});
            this.stationsListView.GridLines = true;
            this.stationsListView.HideSelection = false;
            this.stationsListView.Location = new System.Drawing.Point(21, 187);
            this.stationsListView.Name = "stationsListView";
            this.stationsListView.Size = new System.Drawing.Size(393, 513);
            this.stationsListView.TabIndex = 3;
            this.stationsListView.UseCompatibleStateImageBehavior = false;
            this.stationsListView.View = System.Windows.Forms.View.Details;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(375, 715);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(50, 25);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 131);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(393, 21);
            this.progressBar1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Status:";
            // 
            // name
            // 
            this.name.Text = "Name of Station";
            this.name.Width = 121;
            // 
            // version
            // 
            this.version.Text = "Version Status";
            this.version.Width = 268;
            // 
            // SitesCombo
            // 
            this.SitesCombo.FormattingEnabled = true;
            this.SitesCombo.Items.AddRange(new object[] {
            "Sim A",
            "Sim B",
            "Sim C1",
            "Sim C2",
            "Sim D171",
            "Sim D175"});
            this.SitesCombo.Location = new System.Drawing.Point(21, 39);
            this.SitesCombo.Name = "SitesCombo";
            this.SitesCombo.Size = new System.Drawing.Size(393, 21);
            this.SitesCombo.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Site Type";
            // 
            // DllValidator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(437, 743);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SitesCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.stationsListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folderDialogButton);
            this.Controls.Add(this.folderTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DllValidator";
            this.Text = "Dll Validator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderTextBox;
        private System.Windows.Forms.Button folderDialogButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView stationsListView;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SitesCombo;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ColumnHeader name;
        public System.Windows.Forms.ColumnHeader version;
    }
}

