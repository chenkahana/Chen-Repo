namespace ExceptionFilter
{
    partial class ExceptionFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionFilter));
            this.runButton = new System.Windows.Forms.Button();
            this.folderDialogText = new System.Windows.Forms.TextBox();
            this.folderDialogButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(124, 55);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 1;
            this.runButton.Text = "Run Filter";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // folderDialogText
            // 
            this.folderDialogText.Location = new System.Drawing.Point(12, 29);
            this.folderDialogText.Name = "folderDialogText";
            this.folderDialogText.Size = new System.Drawing.Size(251, 20);
            this.folderDialogText.TabIndex = 2;
            // 
            // folderDialogButton
            // 
            this.folderDialogButton.Location = new System.Drawing.Point(269, 27);
            this.folderDialogButton.Name = "folderDialogButton";
            this.folderDialogButton.Size = new System.Drawing.Size(45, 23);
            this.folderDialogButton.TabIndex = 3;
            this.folderDialogButton.Text = "...";
            this.folderDialogButton.UseVisualStyleBackColor = true;
            this.folderDialogButton.Click += new System.EventHandler(this.folderDialogButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 84);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(301, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // ExceptionFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 136);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.folderDialogButton);
            this.Controls.Add(this.folderDialogText);
            this.Controls.Add(this.runButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExceptionFilter";
            this.Text = "Exception Filter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox folderDialogText;
        private System.Windows.Forms.Button folderDialogButton;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

