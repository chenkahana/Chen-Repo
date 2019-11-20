namespace JFTES_Configurator
{
    partial class OptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.DBUserNameTextBox = new System.Windows.Forms.TextBox();
            this.DBPasswordTextBox = new System.Windows.Forms.TextBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.IPOfDBLabel = new System.Windows.Forms.Label();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.DatabasePasswordLabel = new System.Windows.Forms.Label();
            this.DBNameLabel = new System.Windows.Forms.Label();
            this.DBNameTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.DBPortTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.autoRefreshTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(23, 77);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(268, 20);
            this.IPTextBox.TabIndex = 0;
            this.IPTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPTextBox_KeyPress);
            // 
            // DBUserNameTextBox
            // 
            this.DBUserNameTextBox.Location = new System.Drawing.Point(25, 185);
            this.DBUserNameTextBox.Name = "DBUserNameTextBox";
            this.DBUserNameTextBox.Size = new System.Drawing.Size(267, 20);
            this.DBUserNameTextBox.TabIndex = 1;
            // 
            // DBPasswordTextBox
            // 
            this.DBPasswordTextBox.Location = new System.Drawing.Point(25, 250);
            this.DBPasswordTextBox.Name = "DBPasswordTextBox";
            this.DBPasswordTextBox.Size = new System.Drawing.Size(267, 20);
            this.DBPasswordTextBox.TabIndex = 2;
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(234, 486);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(83, 28);
            this.doneButton.TabIndex = 3;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // IPOfDBLabel
            // 
            this.IPOfDBLabel.AutoSize = true;
            this.IPOfDBLabel.Location = new System.Drawing.Point(20, 61);
            this.IPOfDBLabel.Name = "IPOfDBLabel";
            this.IPOfDBLabel.Size = new System.Drawing.Size(66, 13);
            this.IPOfDBLabel.TabIndex = 4;
            this.IPOfDBLabel.Text = "Database IP";
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(22, 169);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(109, 13);
            this.userNameLabel.TabIndex = 5;
            this.userNameLabel.Text = "Database User Name";
            // 
            // DatabasePasswordLabel
            // 
            this.DatabasePasswordLabel.AutoSize = true;
            this.DatabasePasswordLabel.Location = new System.Drawing.Point(22, 234);
            this.DatabasePasswordLabel.Name = "DatabasePasswordLabel";
            this.DatabasePasswordLabel.Size = new System.Drawing.Size(102, 13);
            this.DatabasePasswordLabel.TabIndex = 6;
            this.DatabasePasswordLabel.Text = "Database Password";
            // 
            // DBNameLabel
            // 
            this.DBNameLabel.AutoSize = true;
            this.DBNameLabel.Location = new System.Drawing.Point(23, 288);
            this.DBNameLabel.Name = "DBNameLabel";
            this.DBNameLabel.Size = new System.Drawing.Size(84, 13);
            this.DBNameLabel.TabIndex = 8;
            this.DBNameLabel.Text = "Database Name";
            // 
            // DBNameTextBox
            // 
            this.DBNameTextBox.Location = new System.Drawing.Point(26, 304);
            this.DBNameTextBox.Name = "DBNameTextBox";
            this.DBNameTextBox.Size = new System.Drawing.Size(267, 20);
            this.DBNameTextBox.TabIndex = 7;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(22, 110);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(75, 13);
            this.PortLabel.TabIndex = 10;
            this.PortLabel.Text = "Database Port";
            // 
            // DBPortTextBox
            // 
            this.DBPortTextBox.Location = new System.Drawing.Point(25, 126);
            this.DBPortTextBox.Name = "DBPortTextBox";
            this.DBPortTextBox.Size = new System.Drawing.Size(267, 20);
            this.DBPortTextBox.TabIndex = 9;
            this.DBPortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DBPortTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "AutoRefresh Path";
            // 
            // autoRefreshTextBox
            // 
            this.autoRefreshTextBox.Location = new System.Drawing.Point(26, 364);
            this.autoRefreshTextBox.Name = "autoRefreshTextBox";
            this.autoRefreshTextBox.Size = new System.Drawing.Size(267, 20);
            this.autoRefreshTextBox.TabIndex = 12;
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 526);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.autoRefreshTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.DBPortTextBox);
            this.Controls.Add(this.DBNameLabel);
            this.Controls.Add(this.DBNameTextBox);
            this.Controls.Add(this.DatabasePasswordLabel);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.IPOfDBLabel);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.DBPasswordTextBox);
            this.Controls.Add(this.DBUserNameTextBox);
            this.Controls.Add(this.IPTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionForm";
            this.Text = "Option";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.TextBox DBUserNameTextBox;
        private System.Windows.Forms.TextBox DBPasswordTextBox;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Label IPOfDBLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label DatabasePasswordLabel;
        private System.Windows.Forms.Label DBNameLabel;
        private System.Windows.Forms.TextBox DBNameTextBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox DBPortTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox autoRefreshTextBox;
    }
}