namespace JFTES_Configurator
{
    partial class EditStation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditStation));
            this.label1 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StationIDTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.isPilotCheckBox = new System.Windows.Forms.CheckBox();
            this.hasSMECheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(13, 55);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(134, 55);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(100, 20);
            this.IPTextBox.TabIndex = 3;
            this.IPTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPTextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP";
            // 
            // StationIDTextBox
            // 
            this.StationIDTextBox.Location = new System.Drawing.Point(254, 55);
            this.StationIDTextBox.Name = "StationIDTextBox";
            this.StationIDTextBox.Size = new System.Drawing.Size(100, 20);
            this.StationIDTextBox.TabIndex = 5;
            this.StationIDTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StationIDTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Station ID";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(299, 144);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // isPilotCheckBox
            // 
            this.isPilotCheckBox.AutoSize = true;
            this.isPilotCheckBox.Location = new System.Drawing.Point(70, 97);
            this.isPilotCheckBox.Name = "isPilotCheckBox";
            this.isPilotCheckBox.Size = new System.Drawing.Size(57, 17);
            this.isPilotCheckBox.TabIndex = 11;
            this.isPilotCheckBox.Text = "Is Pilot";
            this.isPilotCheckBox.UseVisualStyleBackColor = true;
            // 
            // hasSMECheckBox
            // 
            this.hasSMECheckBox.AutoSize = true;
            this.hasSMECheckBox.Location = new System.Drawing.Point(217, 97);
            this.hasSMECheckBox.Name = "hasSMECheckBox";
            this.hasSMECheckBox.Size = new System.Drawing.Size(71, 17);
            this.hasSMECheckBox.TabIndex = 12;
            this.hasSMECheckBox.Text = "Has SME";
            this.hasSMECheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "label4";
            // 
            // EditStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 179);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hasSMECheckBox);
            this.Controls.Add(this.isPilotCheckBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.StationIDTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditStation";
            this.Text = "Edit Station";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StationIDTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox isPilotCheckBox;
        private System.Windows.Forms.CheckBox hasSMECheckBox;
        private System.Windows.Forms.Label label4;
    }
}