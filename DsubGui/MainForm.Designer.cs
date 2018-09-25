namespace DsubGui
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.comPortStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.comPortSettingsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.comPortErrorsTextBox = new System.Windows.Forms.TextBox();
            this.comPortDataGridView = new System.Windows.Forms.DataGridView();
            this.writeTextBox = new System.Windows.Forms.TextBox();
            this.writeButton = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comPortDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.comPortToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(604, 24);
            this.mainMenuStrip.TabIndex = 4;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetGridToolStripMenuItem,
            this.clearErrorsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // resetGridToolStripMenuItem
            // 
            this.resetGridToolStripMenuItem.Name = "resetGridToolStripMenuItem";
            this.resetGridToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.resetGridToolStripMenuItem.Text = "&Reset Grid";
            this.resetGridToolStripMenuItem.Click += new System.EventHandler(this.resetGridToolStripMenuItem_Click);
            // 
            // clearErrorsToolStripMenuItem
            // 
            this.clearErrorsToolStripMenuItem.Name = "clearErrorsToolStripMenuItem";
            this.clearErrorsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.clearErrorsToolStripMenuItem.Text = "Clear &Errors";
            this.clearErrorsToolStripMenuItem.Click += new System.EventHandler(this.clearErrorsToolStripMenuItem_Click);
            // 
            // comPortToolStripMenuItem
            // 
            this.comPortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.comPortToolStripMenuItem.Name = "comPortToolStripMenuItem";
            this.comPortToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.comPortToolStripMenuItem.Text = "Com &Port";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.openToolStripMenuItem.Tag = "enableNotEditing";
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.closeToolStripMenuItem.Tag = "enableEditing";
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Tag = "enableNotEditing";
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comPortStatusLabel,
            this.toolStripStatusLabel2,
            this.comPortSettingsLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 538);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(604, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "mainStatusStrip";
            // 
            // comPortStatusLabel
            // 
            this.comPortStatusLabel.AutoSize = false;
            this.comPortStatusLabel.Name = "comPortStatusLabel";
            this.comPortStatusLabel.Size = new System.Drawing.Size(175, 17);
            this.comPortStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(114, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // comPortSettingsLabel
            // 
            this.comPortSettingsLabel.AutoSize = false;
            this.comPortSettingsLabel.Name = "comPortSettingsLabel";
            this.comPortSettingsLabel.Size = new System.Drawing.Size(300, 17);
            this.comPortSettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comPortErrorsTextBox
            // 
            this.comPortErrorsTextBox.Location = new System.Drawing.Point(12, 430);
            this.comPortErrorsTextBox.Multiline = true;
            this.comPortErrorsTextBox.Name = "comPortErrorsTextBox";
            this.comPortErrorsTextBox.ReadOnly = true;
            this.comPortErrorsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.comPortErrorsTextBox.Size = new System.Drawing.Size(580, 74);
            this.comPortErrorsTextBox.TabIndex = 3;
            this.comPortErrorsTextBox.TabStop = false;
            // 
            // comPortDataGridView
            // 
            this.comPortDataGridView.AllowUserToAddRows = false;
            this.comPortDataGridView.AllowUserToDeleteRows = false;
            this.comPortDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.comPortDataGridView.Location = new System.Drawing.Point(12, 27);
            this.comPortDataGridView.Name = "comPortDataGridView";
            this.comPortDataGridView.ReadOnly = true;
            this.comPortDataGridView.Size = new System.Drawing.Size(580, 392);
            this.comPortDataGridView.TabIndex = 2;
            // 
            // writeTextBox
            // 
            this.writeTextBox.Location = new System.Drawing.Point(12, 510);
            this.writeTextBox.Name = "writeTextBox";
            this.writeTextBox.Size = new System.Drawing.Size(529, 20);
            this.writeTextBox.TabIndex = 0;
            // 
            // writeButton
            // 
            this.writeButton.Location = new System.Drawing.Point(547, 508);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(45, 23);
            this.writeButton.TabIndex = 1;
            this.writeButton.Text = "&Write";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.writeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 560);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.writeTextBox);
            this.Controls.Add(this.comPortDataGridView);
            this.Controls.Add(this.comPortErrorsTextBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "dsub GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comPortDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel comPortStatusLabel;
        private System.Windows.Forms.TextBox comPortErrorsTextBox;
        private System.Windows.Forms.DataGridView comPortDataGridView;
        private System.Windows.Forms.ToolStripMenuItem comPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel comPortSettingsLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TextBox writeTextBox;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearErrorsToolStripMenuItem;
    }
}

