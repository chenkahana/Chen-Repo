namespace Desributer
{
    partial class Destributer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Destributer));
            this.sourceFolderText = new System.Windows.Forms.TextBox();
            this.sourceFolderButton = new System.Windows.Forms.Button();
            this.distanceComputerDestination = new System.Windows.Forms.TextBox();
            this.networkComputerCheckBox = new System.Windows.Forms.CheckBox();
            this.localDestinationText = new System.Windows.Forms.TextBox();
            this.destinationButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sourceFolderText
            // 
            this.sourceFolderText.Location = new System.Drawing.Point(21, 29);
            this.sourceFolderText.Name = "sourceFolderText";
            this.sourceFolderText.Size = new System.Drawing.Size(231, 20);
            this.sourceFolderText.TabIndex = 0;
            // 
            // sourceFolderButton
            // 
            this.sourceFolderButton.Location = new System.Drawing.Point(268, 29);
            this.sourceFolderButton.Name = "sourceFolderButton";
            this.sourceFolderButton.Size = new System.Drawing.Size(35, 20);
            this.sourceFolderButton.TabIndex = 1;
            this.sourceFolderButton.Text = "...";
            this.sourceFolderButton.UseVisualStyleBackColor = true;
            this.sourceFolderButton.Click += new System.EventHandler(this.sourceFolderButton_Click);
            // 
            // distanceComputerDestination
            // 
            this.distanceComputerDestination.Location = new System.Drawing.Point(21, 198);
            this.distanceComputerDestination.Name = "distanceComputerDestination";
            this.distanceComputerDestination.Size = new System.Drawing.Size(231, 20);
            this.distanceComputerDestination.TabIndex = 2;
            // 
            // networkComputerCheckBox
            // 
            this.networkComputerCheckBox.AutoSize = true;
            this.networkComputerCheckBox.Location = new System.Drawing.Point(21, 78);
            this.networkComputerCheckBox.Name = "networkComputerCheckBox";
            this.networkComputerCheckBox.Size = new System.Drawing.Size(114, 17);
            this.networkComputerCheckBox.TabIndex = 3;
            this.networkComputerCheckBox.Text = "Network Computer";
            this.networkComputerCheckBox.UseVisualStyleBackColor = true;
            this.networkComputerCheckBox.CheckedChanged += new System.EventHandler(this.networkComputerCheckBox_CheckedChanged);
            // 
            // localDestinationText
            // 
            this.localDestinationText.Location = new System.Drawing.Point(21, 140);
            this.localDestinationText.Name = "localDestinationText";
            this.localDestinationText.Size = new System.Drawing.Size(230, 20);
            this.localDestinationText.TabIndex = 4;
            // 
            // destinationButton
            // 
            this.destinationButton.Location = new System.Drawing.Point(268, 140);
            this.destinationButton.Name = "destinationButton";
            this.destinationButton.Size = new System.Drawing.Size(35, 20);
            this.destinationButton.TabIndex = 5;
            this.destinationButton.Text = "...";
            this.destinationButton.UseVisualStyleBackColor = true;
            this.destinationButton.Click += new System.EventHandler(this.destinationButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Local Destination";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Network Destination";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(80, 254);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(134, 21);
            this.runButton.TabIndex = 9;
            this.runButton.Text = "Go";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // Destributer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 295);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.destinationButton);
            this.Controls.Add(this.localDestinationText);
            this.Controls.Add(this.networkComputerCheckBox);
            this.Controls.Add(this.distanceComputerDestination);
            this.Controls.Add(this.sourceFolderButton);
            this.Controls.Add(this.sourceFolderText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Destributer";
            this.Text = "Destributer";
            this.Load += new System.EventHandler(this.Distributer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourceFolderText;
        private System.Windows.Forms.Button sourceFolderButton;
        private System.Windows.Forms.TextBox distanceComputerDestination;
        private System.Windows.Forms.CheckBox networkComputerCheckBox;
        private System.Windows.Forms.TextBox localDestinationText;
        private System.Windows.Forms.Button destinationButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button runButton;
    }
}

