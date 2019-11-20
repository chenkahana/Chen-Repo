namespace JFTES_Configurator
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.optionButton = new System.Windows.Forms.Button();
            this.updateXMLnDBButton = new System.Windows.Forms.Button();
            this.currentInfoLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBarLabel = new System.Windows.Forms.Label();
            this.stationsListView = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StationName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StationID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.isPilot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hasSME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addToListViewButton = new System.Windows.Forms.Button();
            this.editStationButton = new System.Windows.Forms.Button();
            this.updateDBButton = new System.Windows.Forms.Button();
            this.updateXmlButton = new System.Windows.Forms.Button();
            this.fillListViewButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.sitesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // optionButton
            // 
            this.optionButton.Location = new System.Drawing.Point(380, 24);
            this.optionButton.Name = "optionButton";
            this.optionButton.Size = new System.Drawing.Size(36, 26);
            this.optionButton.TabIndex = 0;
            this.optionButton.Text = "...";
            this.optionButton.UseVisualStyleBackColor = true;
            this.optionButton.Click += new System.EventHandler(this.optionButton_Click);
            // 
            // updateXMLnDBButton
            // 
            this.updateXMLnDBButton.Location = new System.Drawing.Point(281, 561);
            this.updateXMLnDBButton.Name = "updateXMLnDBButton";
            this.updateXMLnDBButton.Size = new System.Drawing.Size(133, 25);
            this.updateXMLnDBButton.TabIndex = 1;
            this.updateXMLnDBButton.Text = "Update XML\'s and DB";
            this.updateXMLnDBButton.UseVisualStyleBackColor = true;
            this.updateXMLnDBButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // currentInfoLabel
            // 
            this.currentInfoLabel.AutoSize = true;
            this.currentInfoLabel.Location = new System.Drawing.Point(9, 24);
            this.currentInfoLabel.Name = "currentInfoLabel";
            this.currentInfoLabel.Size = new System.Drawing.Size(35, 13);
            this.currentInfoLabel.TabIndex = 2;
            this.currentInfoLabel.Text = "label1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 516);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(402, 28);
            this.progressBar1.TabIndex = 3;
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.AutoSize = true;
            this.progressBarLabel.Location = new System.Drawing.Point(12, 500);
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(0, 13);
            this.progressBarLabel.TabIndex = 4;
            // 
            // stationsListView
            // 
            this.stationsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.StationName,
            this.IP,
            this.StationID,
            this.isPilot,
            this.hasSME});
            this.stationsListView.FullRowSelect = true;
            this.stationsListView.GridLines = true;
            this.stationsListView.HideSelection = false;
            this.stationsListView.Location = new System.Drawing.Point(12, 184);
            this.stationsListView.Name = "stationsListView";
            this.stationsListView.Size = new System.Drawing.Size(411, 223);
            this.stationsListView.TabIndex = 5;
            this.stationsListView.UseCompatibleStateImageBehavior = false;
            this.stationsListView.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 32;
            // 
            // StationName
            // 
            this.StationName.Text = "Name";
            this.StationName.Width = 82;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.Width = 114;
            // 
            // StationID
            // 
            this.StationID.Text = "Station ID";
            // 
            // isPilot
            // 
            this.isPilot.Text = "isPilot";
            // 
            // hasSME
            // 
            this.hasSME.Text = "Has SME";
            // 
            // addToListViewButton
            // 
            this.addToListViewButton.Location = new System.Drawing.Point(334, 413);
            this.addToListViewButton.Name = "addToListViewButton";
            this.addToListViewButton.Size = new System.Drawing.Size(80, 27);
            this.addToListViewButton.TabIndex = 6;
            this.addToListViewButton.Text = "Add";
            this.addToListViewButton.UseVisualStyleBackColor = true;
            this.addToListViewButton.Click += new System.EventHandler(this.addToListViewButton_Click);
            // 
            // editStationButton
            // 
            this.editStationButton.Location = new System.Drawing.Point(253, 413);
            this.editStationButton.Name = "editStationButton";
            this.editStationButton.Size = new System.Drawing.Size(75, 27);
            this.editStationButton.TabIndex = 7;
            this.editStationButton.Text = "Edit";
            this.editStationButton.UseVisualStyleBackColor = true;
            this.editStationButton.Click += new System.EventHandler(this.editStationButton_Click);
            // 
            // updateDBButton
            // 
            this.updateDBButton.Location = new System.Drawing.Point(133, 561);
            this.updateDBButton.Name = "updateDBButton";
            this.updateDBButton.Size = new System.Drawing.Size(96, 25);
            this.updateDBButton.TabIndex = 8;
            this.updateDBButton.Text = "Update DB";
            this.updateDBButton.UseVisualStyleBackColor = true;
            this.updateDBButton.Click += new System.EventHandler(this.updateDBButton_Click);
            // 
            // updateXmlButton
            // 
            this.updateXmlButton.Location = new System.Drawing.Point(12, 561);
            this.updateXmlButton.Name = "updateXmlButton";
            this.updateXmlButton.Size = new System.Drawing.Size(102, 25);
            this.updateXmlButton.TabIndex = 9;
            this.updateXmlButton.Text = "Update XML\'s";
            this.updateXmlButton.UseVisualStyleBackColor = true;
            this.updateXmlButton.Click += new System.EventHandler(this.updateXmlButton_Click);
            // 
            // fillListViewButton
            // 
            this.fillListViewButton.Location = new System.Drawing.Point(312, 151);
            this.fillListViewButton.Name = "fillListViewButton";
            this.fillListViewButton.Size = new System.Drawing.Size(102, 27);
            this.fillListViewButton.TabIndex = 10;
            this.fillListViewButton.Text = "Fill Table From DB";
            this.fillListViewButton.UseVisualStyleBackColor = true;
            this.fillListViewButton.Click += new System.EventHandler(this.fillListViewButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(172, 413);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 27);
            this.deleteButton.TabIndex = 11;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // sitesComboBox
            // 
            this.sitesComboBox.FormattingEnabled = true;
            this.sitesComboBox.Location = new System.Drawing.Point(12, 157);
            this.sitesComboBox.Name = "sitesComboBox";
            this.sitesComboBox.Size = new System.Drawing.Size(217, 21);
            this.sitesComboBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Choose Site Layout";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 598);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sitesComboBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.fillListViewButton);
            this.Controls.Add(this.updateXmlButton);
            this.Controls.Add(this.updateDBButton);
            this.Controls.Add(this.editStationButton);
            this.Controls.Add(this.addToListViewButton);
            this.Controls.Add(this.stationsListView);
            this.Controls.Add(this.progressBarLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.currentInfoLabel);
            this.Controls.Add(this.updateXMLnDBButton);
            this.Controls.Add(this.optionButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "JFTES Configurator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button optionButton;
        private System.Windows.Forms.Button updateXMLnDBButton;
        private System.Windows.Forms.Label currentInfoLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressBarLabel;
        public System.Windows.Forms.ListView stationsListView;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader StationName;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader StationID;
        private System.Windows.Forms.ColumnHeader isPilot;
        private System.Windows.Forms.Button addToListViewButton;
        private System.Windows.Forms.Button editStationButton;
        private System.Windows.Forms.ColumnHeader hasSME;
        private System.Windows.Forms.Button updateDBButton;
        private System.Windows.Forms.Button updateXmlButton;
        private System.Windows.Forms.Button fillListViewButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ComboBox sitesComboBox;
        private System.Windows.Forms.Label label1;
    }
}

