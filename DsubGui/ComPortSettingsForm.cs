using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dsub;                               //Serial port communications
using DsubGui.Properties;                 //For application settings
using scooter;                              //Utility code
using System.Text.RegularExpressions;       //RegEx

namespace DsubGui
{
    public partial class ComPortSettingsForm : Form
    {

        #region Private declarations

        private MainForm mainForm;

        #endregion Private declarations

        #region Constuctors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ComPortSettingsForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Constructor which accepts mainForm reference
        /// </summary>
        /// <param name="mainForm"></param>
        public ComPortSettingsForm(MainForm mainForm) : this()
        {
            this.mainForm = mainForm;
        }

        #endregion Constructors

        #region Event Handlers

        /// <summary>
        /// Load datasources and saved options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsForm_Load(object sender, EventArgs e)
        {
            //Set combobox data sources
            comPortComboBox.DataSource = ComPort.RetrieveComPortList();
            baudRateComboBox.DataSource = ComPort.RetrieveBaudRateList();
            dataBitsComboBox.DataSource = ComPort.RetrieveDataBitsList();
            parityComboBox.DataSource = ComPort.RetrieveParityList();
            stopBitsComboBox.DataSource = ComPort.RetrieveStopBitsList();
            handshakeComboBox.DataSource = ComPort.RetrieveHandshakeList();

            //Retrieve saved options
            comPortComboBox.SelectedItem = Settings.Default.ComPort;
            baudRateComboBox.SelectedItem = Settings.Default.BaudRate;
            dataBitsComboBox.SelectedItem = Settings.Default.DataBits;
            parityComboBox.SelectedItem = Settings.Default.Parity;
            stopBitsComboBox.SelectedItem = Settings.Default.StopBits;
            handshakeComboBox.SelectedItem = Settings.Default.Handshake;
            newLineTextBox.Text = Settings.Default.NewLine;
            fieldDelimiterTextBox.Text = Settings.Default.FieldDelimiter;
            readTimeoutTextBox.Text = Settings.Default.ReadTimeout.ToString();
            writeTimeoutTextBox.Text = Settings.Default.WriteTimeout.ToString();
            dtrEnableCheckBox.Checked = Settings.Default.DtrEnable;
            rtsEnableCheckBox.Checked = Settings.Default.RtsEnable;
            connectOnStartupCheckBox.Checked = Settings.Default.ConnectOnStartup;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidateFormData())
            {
                Settings.Default.ComPort = comPortComboBox.SelectedItem.ToString();
                Settings.Default.BaudRate = (int)baudRateComboBox.SelectedItem;
                Settings.Default.DataBits = (int)dataBitsComboBox.SelectedItem;
                Settings.Default.Parity = (System.IO.Ports.Parity)parityComboBox.SelectedItem;
                Settings.Default.StopBits = (System.IO.Ports.StopBits)stopBitsComboBox.SelectedItem;
                Settings.Default.Handshake = (System.IO.Ports.Handshake)handshakeComboBox.SelectedItem;
                Settings.Default.NewLine = newLineTextBox.Text;
                Settings.Default.FieldDelimiter = fieldDelimiterTextBox.Text;
                Settings.Default.ReadTimeout = int.Parse(readTimeoutTextBox.Text);
                Settings.Default.WriteTimeout = int.Parse(writeTimeoutTextBox.Text);
                Settings.Default.DtrEnable = dtrEnableCheckBox.Checked;
                Settings.Default.RtsEnable = rtsEnableCheckBox.Checked;
                Settings.Default.ConnectOnStartup = connectOnStartupCheckBox.Checked;
                Settings.Default.Save();

                //Refresh port settings for comPort on main form.
                if (!(mainForm == null))
                    mainForm.RefreshPortSettings();

                this.Close();
            }
        }

        #endregion Event Handlers

        #region Private Methods

        private bool ValidateFormData()
        {
            bool validation = true;
            
            if (!Validata.ValueSelected(comPortComboBox, comPortSettingsErrorProvider, "COM Port"))
                validation = false;
            if (!Validata.ValueSelected(baudRateComboBox, comPortSettingsErrorProvider, "Baud Rate"))
                validation = false;
            if (!Validata.ValueSelected(dataBitsComboBox, comPortSettingsErrorProvider, "Data Bits"))
                validation = false;
            if (!Validata.ValueSelected(parityComboBox, comPortSettingsErrorProvider, "Parity"))
                validation = false;
            if (!Validata.ValueSelected(stopBitsComboBox, comPortSettingsErrorProvider, "Stop Bits"))
                validation = false;
            if (!Validata.ValueSelected(handshakeComboBox, comPortSettingsErrorProvider, "Handshake"))
                validation = false;
            if (!Validata.ContainsInteger(readTimeoutTextBox, comPortSettingsErrorProvider, "Read Timeout"))
                validation = false;
            if (!Validata.ContainsInteger(writeTimeoutTextBox, comPortSettingsErrorProvider, "Write Timeout"))
                validation = false;

            //Newline is required, and must contain a valid character or escape sequence.
            if (!Validata.ContainsValue(newLineTextBox, comPortSettingsErrorProvider, "New Line"))
                validation = false;
            else
            {
                try
                {
                    Regex.Unescape(newLineTextBox.Text);
                }
                catch (ArgumentException)
                {
                    //Unrecognized escape sequence in field delimiter
                    comPortSettingsErrorProvider.SetError(newLineTextBox, "This is an invalid escape sequence and cannot be used.");
                    validation = false;
                }
            }

            //Field delimiter can contain a single character or valid escape sequence.
            if (fieldDelimiterTextBox.TextLength > 0)
            {
                try
                {
                    if (Regex.Unescape(fieldDelimiterTextBox.Text).Length > 1)
                    {
                        comPortSettingsErrorProvider.SetError(fieldDelimiterTextBox, "Field delimiter must be a single character or escape sequence.");
                        validation = false;
                    }
                    else
                        comPortSettingsErrorProvider.SetError(fieldDelimiterTextBox, "");
                }
                catch (ArgumentException)
                {
                    //Unrecognized escape sequence in field delimiter
                    comPortSettingsErrorProvider.SetError(fieldDelimiterTextBox, "This is an invalid escape sequence and cannot be used.");
                    validation = false;
                }
            }
            else
                comPortSettingsErrorProvider.SetError(fieldDelimiterTextBox, "");

            return validation;
        }

        #endregion Private Methods

    }
}
