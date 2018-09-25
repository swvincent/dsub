namespace DsubGui
{
    partial class ComPortSettingsForm
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
            this.components = new System.ComponentModel.Container();
            this.baudRateComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.handshakeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comPortComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataBitsComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.parityComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.stopBitsComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.newLineTextBox = new System.Windows.Forms.TextBox();
            this.comPortSettingsErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.fieldDelimiterTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.connectOnStartupCheckBox = new System.Windows.Forms.CheckBox();
            this.readTimeoutTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.writeTimeoutTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtrEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.rtsEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.comPortSettingsErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // baudRateComboBox
            // 
            this.baudRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudRateComboBox.FormattingEnabled = true;
            this.baudRateComboBox.Location = new System.Drawing.Point(98, 48);
            this.baudRateComboBox.Name = "baudRateComboBox";
            this.baudRateComboBox.Size = new System.Drawing.Size(95, 21);
            this.baudRateComboBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Baud Rate:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Handshaking:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // handshakeComboBox
            // 
            this.handshakeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.handshakeComboBox.FormattingEnabled = true;
            this.handshakeComboBox.Location = new System.Drawing.Point(98, 156);
            this.handshakeComboBox.Name = "handshakeComboBox";
            this.handshakeComboBox.Size = new System.Drawing.Size(146, 21);
            this.handshakeComboBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Port:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comPortComboBox
            // 
            this.comPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPortComboBox.FormattingEnabled = true;
            this.comPortComboBox.Location = new System.Drawing.Point(98, 21);
            this.comPortComboBox.Name = "comPortComboBox";
            this.comPortComboBox.Size = new System.Drawing.Size(95, 21);
            this.comPortComboBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Data Bits:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dataBitsComboBox
            // 
            this.dataBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataBitsComboBox.FormattingEnabled = true;
            this.dataBitsComboBox.Location = new System.Drawing.Point(98, 75);
            this.dataBitsComboBox.Name = "dataBitsComboBox";
            this.dataBitsComboBox.Size = new System.Drawing.Size(95, 21);
            this.dataBitsComboBox.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Parity:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // parityComboBox
            // 
            this.parityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parityComboBox.FormattingEnabled = true;
            this.parityComboBox.Location = new System.Drawing.Point(98, 102);
            this.parityComboBox.Name = "parityComboBox";
            this.parityComboBox.Size = new System.Drawing.Size(95, 21);
            this.parityComboBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Stop Bits:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // stopBitsComboBox
            // 
            this.stopBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopBitsComboBox.FormattingEnabled = true;
            this.stopBitsComboBox.Location = new System.Drawing.Point(98, 129);
            this.stopBitsComboBox.Name = "stopBitsComboBox";
            this.stopBitsComboBox.Size = new System.Drawing.Size(95, 21);
            this.stopBitsComboBox.TabIndex = 9;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(185, 266);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 27;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(276, 266);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "New Line:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // newLineTextBox
            // 
            this.newLineTextBox.Location = new System.Drawing.Point(98, 183);
            this.newLineTextBox.Name = "newLineTextBox";
            this.newLineTextBox.Size = new System.Drawing.Size(52, 20);
            this.newLineTextBox.TabIndex = 13;
            // 
            // comPortSettingsErrorProvider
            // 
            this.comPortSettingsErrorProvider.ContainerControl = this;
            // 
            // fieldDelimiterTextBox
            // 
            this.fieldDelimiterTextBox.Location = new System.Drawing.Point(98, 209);
            this.fieldDelimiterTextBox.Name = "fieldDelimiterTextBox";
            this.fieldDelimiterTextBox.Size = new System.Drawing.Size(25, 20);
            this.fieldDelimiterTextBox.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Field Delimiter:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(323, 183);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Connect on Startup:";
            // 
            // connectOnStartupCheckBox
            // 
            this.connectOnStartupCheckBox.AutoSize = true;
            this.connectOnStartupCheckBox.Location = new System.Drawing.Point(433, 183);
            this.connectOnStartupCheckBox.Name = "connectOnStartupCheckBox";
            this.connectOnStartupCheckBox.Size = new System.Drawing.Size(15, 14);
            this.connectOnStartupCheckBox.TabIndex = 26;
            this.connectOnStartupCheckBox.UseVisualStyleBackColor = true;
            // 
            // readTimeoutTextBox
            // 
            this.readTimeoutTextBox.Location = new System.Drawing.Point(431, 70);
            this.readTimeoutTextBox.Name = "readTimeoutTextBox";
            this.readTimeoutTextBox.Size = new System.Drawing.Size(43, 20);
            this.readTimeoutTextBox.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(347, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Read Timeout:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // writeTimeoutTextBox
            // 
            this.writeTimeoutTextBox.Location = new System.Drawing.Point(431, 97);
            this.writeTimeoutTextBox.Name = "writeTimeoutTextBox";
            this.writeTimeoutTextBox.Size = new System.Drawing.Size(43, 20);
            this.writeTimeoutTextBox.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(349, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Write Timeout:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dtrEnableCheckBox
            // 
            this.dtrEnableCheckBox.AutoSize = true;
            this.dtrEnableCheckBox.Location = new System.Drawing.Point(433, 129);
            this.dtrEnableCheckBox.Name = "dtrEnableCheckBox";
            this.dtrEnableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.dtrEnableCheckBox.TabIndex = 22;
            this.dtrEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(355, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "DTR Enable:";
            // 
            // rtsEnableCheckBox
            // 
            this.rtsEnableCheckBox.AutoSize = true;
            this.rtsEnableCheckBox.Location = new System.Drawing.Point(433, 156);
            this.rtsEnableCheckBox.Name = "rtsEnableCheckBox";
            this.rtsEnableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.rtsEnableCheckBox.TabIndex = 24;
            this.rtsEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(356, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "RTS Enable:";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(268, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1, 242);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(287, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(225, 14);
            this.label14.TabIndex = 29;
            this.label14.Text = "(Timeouts ≤ 0 are treated as infinite timeout)";
            // 
            // ComPortSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 303);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtsEnableCheckBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dtrEnableCheckBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.writeTimeoutTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.readTimeoutTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.connectOnStartupCheckBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.fieldDelimiterTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.newLineTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.stopBitsComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataBitsComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.parityComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comPortComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.handshakeComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.baudRateComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComPortSettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "COM Port Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.optionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comPortSettingsErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox baudRateComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox handshakeComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comPortComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dataBitsComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox parityComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox stopBitsComboBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox newLineTextBox;
        private System.Windows.Forms.ErrorProvider comPortSettingsErrorProvider;
        private System.Windows.Forms.TextBox fieldDelimiterTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox connectOnStartupCheckBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox writeTimeoutTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox readTimeoutTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox rtsEnableCheckBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox dtrEnableCheckBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
    }
}