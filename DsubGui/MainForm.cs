///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// dsub Project
/// http://www.swvincent.com/dsub
/// 
/// Copyright (c) 2019 Scott W. Vincent
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in
/// all copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
/// THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dsub;                               //ComPort class
using DsubGui.Properties;                 //for Application settings
using System.Text.RegularExpressions;       //RegEx
using scooter;                              //Utility code

namespace DsubGui
{
    public partial class MainForm : Form
    {

        #region Declarations

        private int sequence = 0;
        private delegate void AccessFormMarshalDelegate(ComPort.ComPortEvent userInterfaceAction, string comPortData);
        private ComPort comPort;
        char fieldDelimiter = '\0';     //I use \0 to indicate there is no delimiter since it shouldn't actually get used

        #endregion Declarations

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Access Form

        /// <summary>
        /// Perform functions in form from a different thread.  See AccessFormMarshal.
        /// </summary>
        /// <remarks>
        /// Based on methods used by Jan Axelson in Serial Port Complete 2nd edition.  I break it out a little further
        /// as I handle exceptions seperately.
        /// </remarks>
        /// <param name="userInterfaceAction">Enum that indicates which action to perform</param>
        /// <param name="comPortData">Com Port data being returned if applicable</param>
        private void AccessForm(ComPort.ComPortEvent userInterfaceAction, string comPortData)
        {
            switch (userInterfaceAction)
            {
                case ComPort.ComPortEvent.ReturnNewData:
                    //Retrieve new com port data.
                    AddComPortDataToGrid(comPortData);
                    break;
                case ComPort.ComPortEvent.ReportStatus:
                    //Display ComPort status.
                    comPortStatusLabel.Text = comPortData;
                    break;
                case ComPort.ComPortEvent.ReportSettings:
                    //Display ComPort settings.
                    comPortSettingsLabel.Text = comPortData;
                    break;
                case ComPort.ComPortEvent.ReportException:
                    //Exception occured in ComPort.
                    MessageBox.Show(comPortData, "Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ComPort.ComPortEvent.ReportSerialError:
                    //Serial error occured in ComPort.
                    comPortErrorsTextBox.AppendText($"{DateTime.Now.ToString()} - {comPortData}\n");
                    break;
                case ComPort.ComPortEvent.ReportSerialSuccess:
                    //Success occured on write
                    comPortErrorsTextBox.AppendText($"{DateTime.Now.ToString()} - Write succeeded with message: {comPortData}\n");
                    break;
            }
        }


        /// <summary>
        /// Enables accessing the form from another thread.  The parameters match those of AccessForm.
        /// </summary>
        /// <remarks>
        /// Based on methods used by Jan Axelson in Serial Port Complete 2nd edition, except I use
        /// BeginInvoke rather than Invoke, to avoid the deadlock issue.
        /// </remarks>
        /// <param name="userInterfaceAction">Enum that indicates which action to perform</param>
        /// <param name="comPortData">Com Port data being returned if applicable</param>
        private void AccessFormMarshal(ComPort.ComPortEvent userInterfaceAction, string comPortData)
        {
            AccessFormMarshalDelegate accessFormMarshalDelegate = new AccessFormMarshalDelegate(AccessForm);

            object[] args = { userInterfaceAction, comPortData };

            //Call AccessForm, passing the parameters in args. I use BeginInvoke, after reading several sources indicating
            //it should be used to avoid a deadlock when the port is closed. Initially I saw it in Alex F.'s comments here:
            //http://forums.codeguru.com/showthread.php?516248-RESOLVED-Serial-port-hangs-on-form-close, more explanation here:
            //http://blogs.msdn.com/b/bclteam/archive/2006/10/10/top-5-serialport-tips-_5b00_kim-hamilton_5d00_.aspx. I'm not
            //sure if there are any downsides yet, but it fixed the deadlock for me, and everything still seems to work okay.
            base.BeginInvoke(accessFormMarshalDelegate, args);
        }

        #endregion Access Form

        #region Event Handlers

        /// <summary>
        /// Runs required actions on form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_Load(object sender, EventArgs e)
        {
            //Specify routine that executes on events in other modules.  The routine can receive data from other modules.
            //Based on methods used by Jan Axelson in Serial Port Complete 2nd edition.
            ComPort.UserInterfaceData += new ComPort.UserInterfaceDataEventHandler(AccessFormMarshal);

            //Create ComPort.
            comPort = new ComPort();
            RefreshPortSettings();

            if (Settings.Default.ConnectOnStartup)
                comPort.OpenPort();

            ToggleFormMode();
        }


        /// <summary>
        /// Close port before form closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //I originally added this to see if it would fix the deadlock when port is closed;
            //it didn't. Probably still not a bad idea to have it, so I left it in.
            ComPort.UserInterfaceData -= AccessFormMarshal;

            comPort.ClosePort();
        }


        /// <summary>
        /// Write text in writeTextBox to COM port, in separate thread.
        /// ComPort.WriteText can be used instead if you want to send in the GUI thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void writeButton_Click(object sender, EventArgs e)
        {
            if (comPort.IsOpen && writeTextBox.Text.Length > 0)
            {
                IAsyncResult ar = null;
                string dateStamp = DateTime.Now.ToString();
                
                try
                {
                    //Must "unescape" the text to write so user can specify CR, LF, etc.
                    string textToWrite = Regex.Unescape(writeTextBox.Text);

                    comPort.writeDelegate = new ComPort.WriteTextDelegate(comPort.WriteText);
                    ar = comPort.writeDelegate.BeginInvoke(textToWrite, new AsyncCallback(comPort.WriteCompleted), dateStamp);
                }
                catch (ArgumentException caught)
                {
                    //Unrecognized escape sequence
                    MessageBox.Show($"The entered text cannot be parsed. Details:\n\n{caught.Message}" +
                        "\n\nPlease correct the text and try again.", "Parse error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion Event Handlers

        #region Menu Code

        /// <summary>
        /// Exit application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /// <summary>
        /// Open Port.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comPort.OpenPort();
            ToggleFormMode();
        }


        /// <summary>
        /// Close Port.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comPort.ClosePort();
            ToggleFormMode();
        }


        /// <summary>
        /// Display Options form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComPortSettingsForm f = new ComPortSettingsForm(this);
            f.Show();
        }


        /// <summary>
        /// Clear errors text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comPortErrorsTextBox.Clear();
        }


        /// <summary>
        /// Remove all columns from grid so it can start over if data changed, etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comPortDataGridView.Columns.Clear();
            sequence = 0;
        }


        /// <summary>
        /// Show about form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        #endregion Menu Code

        #region Private Methods

        /// <summary>
        /// Toggle form between editing/not editing to determine which controls are enabled/disabled.
        /// </summary>
        /// <param name="editing"></param>
        private void ToggleFormMode()
        {
            bool portIsOpen = comPort.IsOpen;

            WinFormHelper.ToggleMenuStripItems(mainMenuStrip, portIsOpen);
            writeTextBox.Enabled = portIsOpen;
            writeButton.Enabled = portIsOpen;
        }


        /// <summary>
        /// Write COM port data to grid.  If Grid is currently empty, determine column structure automatically.
        /// </summary>
        /// <param name="comPortData">Data from COM port</param>
        private void AddComPortDataToGrid(string comPortData)
        {
            string[] splitComPortData = comPortData.Split(fieldDelimiter);
            
            if (comPortDataGridView.ColumnCount == 0)
                SetupDataGridColumns(splitComPortData);

            //Add sequence and datestamp to data and write to grid.
            string[] rowData = new string[] { (++sequence).ToString(), DateTime.Now.ToString() }.Concat(splitComPortData).ToArray();
            comPortDataGridView.Rows.Add(rowData);

            //Move to new row (for scrolling)
            //http://stackoverflow.com/questions/5749983/c-sharp-datagridview-stay-focused-on-last-record helped
            comPortDataGridView.CurrentCell = comPortDataGridView.Rows[comPortDataGridView.Rows.Count - 1].Cells[0];
        }


        /// <summary>
        /// Setup columns in data grid based on incoming data's needs.
        /// </summary>
        /// <param name="splitComPortData"></param>
        private void SetupDataGridColumns(string[] splitComPortData)
        {
            //Initialize based on column count.
            DataGridViewTextBoxColumn c1 = new DataGridViewTextBoxColumn();
            c1.Name = "sequenceColumn";
            c1.HeaderText = "Seq.";
            c1.Width = 40;
            comPortDataGridView.Columns.Add(c1);

            DataGridViewTextBoxColumn c2 = new DataGridViewTextBoxColumn();
            c2.Name = "dateStampColumn";
            c2.HeaderText = "Date Stamp";
            c2.Width = 130;
            comPortDataGridView.Columns.Add(c2);

            for (int i = 0; i < splitComPortData.Length; i++)
            {
                DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                c.Name = "dataColumn" + i.ToString();
                c.HeaderText = "Field " + i.ToString();
                c.Width = 100;
                comPortDataGridView.Columns.Add(c);
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Refresh port settings from application settings.
        /// </summary>
        public void RefreshPortSettings()
        {
            if (!(comPort == null))
            {
                //Must "unescape" the NewLine setting so it is interpreted correctly.  Took me awhile to figure out how.
                string newLine = Regex.Unescape(Settings.Default.NewLine);

                comPort.ChangeSettings(Settings.Default.ComPort, Settings.Default.BaudRate, Settings.Default.Parity, Settings.Default.DataBits,
                    Settings.Default.StopBits, Settings.Default.Handshake, newLine, Settings.Default.ReadTimeout, Settings.Default.WriteTimeout,
                    Settings.Default.DtrEnable, Settings.Default.RtsEnable);
            }

            //Get field delimiter setting.
            string fieldDelimiterSetting = Settings.Default.FieldDelimiter;
            if (fieldDelimiterSetting.Length > 0)
                fieldDelimiter = Regex.Unescape(fieldDelimiterSetting)[0];
            else
                fieldDelimiter = '\0';
        }

        #endregion Public Methods

    }
}