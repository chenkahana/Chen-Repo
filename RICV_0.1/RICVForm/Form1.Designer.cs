namespace RICVForm
{
    partial class Form1
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
            this.actionComboBox = new System.Windows.Forms.ComboBox();
            this.preformActionByComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sitesComboBox = new System.Windows.Forms.ComboBox();
            this.componentComboBox = new System.Windows.Forms.ComboBox();
            this.stationComboBox = new System.Windows.Forms.ComboBox();
            this.siteLabel = new System.Windows.Forms.Label();
            this.componentLabel = new System.Windows.Forms.Label();
            this.stationLabel = new System.Windows.Forms.Label();
            this.defualtListView = new System.Windows.Forms.ListView();
            this.runButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // actionComboBox
            // 
            this.actionComboBox.FormattingEnabled = true;
            this.actionComboBox.Location = new System.Drawing.Point(20, 33);
            this.actionComboBox.Name = "actionComboBox";
            this.actionComboBox.Size = new System.Drawing.Size(258, 21);
            this.actionComboBox.TabIndex = 0;
            // 
            // preformActionByComboBox
            // 
            this.preformActionByComboBox.FormattingEnabled = true;
            this.preformActionByComboBox.Items.AddRange(new object[] {
            "By Site",
            "By Component",
            "By Station"});
            this.preformActionByComboBox.Location = new System.Drawing.Point(311, 33);
            this.preformActionByComboBox.Name = "preformActionByComboBox";
            this.preformActionByComboBox.Size = new System.Drawing.Size(267, 21);
            this.preformActionByComboBox.TabIndex = 1;
            this.preformActionByComboBox.SelectedIndexChanged += new System.EventHandler(this.preformActionByComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Action";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Preform Action By:";
            // 
            // sitesComboBox
            // 
            this.sitesComboBox.FormattingEnabled = true;
            this.sitesComboBox.Location = new System.Drawing.Point(20, 95);
            this.sitesComboBox.Name = "sitesComboBox";
            this.sitesComboBox.Size = new System.Drawing.Size(121, 21);
            this.sitesComboBox.TabIndex = 4;
            // 
            // componentComboBox
            // 
            this.componentComboBox.FormattingEnabled = true;
            this.componentComboBox.Location = new System.Drawing.Point(192, 95);
            this.componentComboBox.Name = "componentComboBox";
            this.componentComboBox.Size = new System.Drawing.Size(134, 21);
            this.componentComboBox.TabIndex = 5;
            // 
            // stationComboBox
            // 
            this.stationComboBox.FormattingEnabled = true;
            this.stationComboBox.Location = new System.Drawing.Point(389, 95);
            this.stationComboBox.Name = "stationComboBox";
            this.stationComboBox.Size = new System.Drawing.Size(125, 21);
            this.stationComboBox.TabIndex = 6;
            // 
            // siteLabel
            // 
            this.siteLabel.AutoSize = true;
            this.siteLabel.Location = new System.Drawing.Point(21, 82);
            this.siteLabel.Name = "siteLabel";
            this.siteLabel.Size = new System.Drawing.Size(25, 13);
            this.siteLabel.TabIndex = 7;
            this.siteLabel.Text = "Site";
            // 
            // componentLabel
            // 
            this.componentLabel.AutoSize = true;
            this.componentLabel.Location = new System.Drawing.Point(200, 81);
            this.componentLabel.Name = "componentLabel";
            this.componentLabel.Size = new System.Drawing.Size(61, 13);
            this.componentLabel.TabIndex = 8;
            this.componentLabel.Text = "Component";
            // 
            // stationLabel
            // 
            this.stationLabel.AutoSize = true;
            this.stationLabel.Location = new System.Drawing.Point(392, 82);
            this.stationLabel.Name = "stationLabel";
            this.stationLabel.Size = new System.Drawing.Size(40, 13);
            this.stationLabel.TabIndex = 9;
            this.stationLabel.Text = "Station";
            // 
            // defualtListView
            // 
            this.defualtListView.HideSelection = false;
            this.defualtListView.Location = new System.Drawing.Point(20, 174);
            this.defualtListView.Name = "defualtListView";
            this.defualtListView.Size = new System.Drawing.Size(558, 337);
            this.defualtListView.TabIndex = 10;
            this.defualtListView.UseCompatibleStateImageBehavior = false;
            this.defualtListView.View = System.Windows.Forms.View.Details;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(506, 553);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(83, 29);
            this.runButton.TabIndex = 11;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 594);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.defualtListView);
            this.Controls.Add(this.stationLabel);
            this.Controls.Add(this.componentLabel);
            this.Controls.Add(this.siteLabel);
            this.Controls.Add(this.stationComboBox);
            this.Controls.Add(this.componentComboBox);
            this.Controls.Add(this.sitesComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.preformActionByComboBox);
            this.Controls.Add(this.actionComboBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox actionComboBox;
        private System.Windows.Forms.ComboBox preformActionByComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sitesComboBox;
        private System.Windows.Forms.ComboBox componentComboBox;
        private System.Windows.Forms.ComboBox stationComboBox;
        private System.Windows.Forms.Label siteLabel;
        private System.Windows.Forms.Label componentLabel;
        private System.Windows.Forms.Label stationLabel;
        private System.Windows.Forms.Button runButton;
        public System.Windows.Forms.ListView defualtListView;
    }
}

