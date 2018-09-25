///////////////////////////////////////////////////////////////////////////////////////////////////
/// dsub
/// www.swvincent.com/dsub
/// www.github.com/swvincent/dsub
/// 
/// This code has been developed for .NET Framework 4.0.
/// 
/// Change History (before using Git)
/// 
/// Date        Desc.
/// =========   ===================================================================================
/// 4/15/2012   Started
/// 8/21/2012   Initial release
/// 8/17/2015   Fixed some bugs. Biggest was if you close/reopen port, timeouts w/ empty buffer
///                 started occuring. It seems it was b/c I reattached events each time port opens,
///                 instead of doing it once when serialPort is created. Also added ability to set
///                 timeouts and DTR/RTS enable, latter is needed for Arduino Leonardo.
/// 9/3/2015    Added ability to report back success from write operation made in separate thread.
///                 Before it only reported back on errors. Also some minor fixes to avoid crashes.
///                 And minor fixes to GUI application, tab order, etc. And added DiscardInBuffer/
///                 DiscardOutBuffer. And switched Invoke to BeginInvoke to avoid deadlock on
///                 close. Phew!
/// 9/8/2015   Nicer error message if port doesn't exist.
///                 Thanks to Kean (http://www.kean.com.au/) for pointing it out!
/// 2/21/2018   Using .Dispose instead of .Close now based on page 161 Serial Port Complete 2nd ed
/// 9/25/2018   Minor code/comments cleanup and initial Git Commit.
/// 
/// MIT License
/// 
/// Copyright (c) 2018 Scott W. Vincent
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
///////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;                          //For all the serial port stuff
using System.Runtime.Remoting.Messaging;        //For Async etc. stuff

namespace Dsub
{
    public class ComPort
    {

        #region Declarations

        public enum ComPortEvent
        {
            ReturnNewData,
            ReportSettings,
            ReportStatus,
            ReportException,
            ReportSerialError,
            ReportSerialSuccess
        }

        //Public members
        public delegate void UserInterfaceDataEventHandler(ComPortEvent comPortEvent, string message);
        public static event UserInterfaceDataEventHandler UserInterfaceData;
        public delegate bool WriteToComPortDelegate(string textToWrite);
        public WriteToComPortDelegate writeDelegate;

        //Private members
        private SerialPort serialPort;
        private SerialDataReceivedEventHandler serialDataReceivedEventHandler1;
        private SerialErrorReceivedEventHandler serialErrorReceivedEventHandler1;

        //Local variables available as properties

        private string portName = string.Empty;
        private int baudRate = 9600;
        private Parity parity = Parity.None;
        private int dataBits = 8;
        private StopBits stopBits = StopBits.One;
        private Handshake handshake = Handshake.None;
        private string newLine = Environment.NewLine;
        private int readTimeout = 500;      //0.5 sec.
        private int writeTimeout = 500;     //0.5 sec.
        private bool dtrEnable = false;
        private bool rtsEnable = false;

        #endregion Declarations

        #region Properties

        public string PortName { get { return portName; } set { portName = value; ReportSettings(); } }
        public int BaudRate { get { return baudRate; } set { baudRate = value; ReportSettings(); } }
        public Parity Parity { get { return parity; } set { parity = value; ReportSettings(); } }
        public int DataBits { get { return dataBits; } set { dataBits = value; ReportSettings(); } }
        public StopBits StopBits { get { return stopBits; } set { stopBits = value; ReportSettings(); } }
        public Handshake Handshake { get { return handshake; } set { handshake = value; } }
        public string NewLine { get { return newLine; } set { newLine = value; } }
        public int ReadTimeout { get { return readTimeout; } set { readTimeout = value; } }
        public int WriteTimeout { get { return writeTimeout; } set { writeTimeout = value; } }
        public bool DtrEnable { get { return dtrEnable; } set { dtrEnable = value; } }
        public bool RtsEnable { get { return rtsEnable; } set { rtsEnable = value; } }
        public bool IsOpen { get { return serialPort.IsOpen; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ComPort()
        {
            //Create the new port and report that it's closed.
            serialPort = new SerialPort();

            serialDataReceivedEventHandler1 = new SerialDataReceivedEventHandler(DataReceived);
            serialErrorReceivedEventHandler1 = new SerialErrorReceivedEventHandler(ErrorReceived);
            serialPort.DataReceived += serialDataReceivedEventHandler1;
            serialPort.ErrorReceived += serialErrorReceivedEventHandler1;
            
            ReportStatus("Port Closed");
        }


        /// <summary>
        /// Constructor which accepts port settings and reports back settings.
        /// </summary>
        /// <param name="portName">Port Name</param>
        /// <param name="baudRate">Baud Rate</param>
        /// <param name="parity">Parity</param>
        /// <param name="dataBits">Data Bits</param>
        /// <param name="stopBits">Stop Bits</param>
        /// <param name="handshake">Handshake</param>
        /// <param name="newLine">New Line character</param>
        public ComPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits,
            Handshake handshake, string newLine) : this()
        {
            ChangeSettings(portName, baudRate, parity, dataBits, stopBits, handshake, newLine);
        }


        /// <summary>
        /// Constructor which accepts port settings and reports back settings.
        /// </summary>
        /// <param name="portName">Port Name</param>
        /// <param name="baudRate">Baud Rate</param>
        /// <param name="parity">Parity</param>
        /// <param name="dataBits">Data Bits</param>
        /// <param name="stopBits">Stop Bits</param>
        /// <param name="handshake">Handshake</param>
        /// <param name="newLine">New Line character</param>
        /// <param name="readTimeout">Read Timeout</param>
        /// <param name="writeTimeout">Write Timeout</param>
        /// <param name="dtrEnable">DTR Enable</param>
        /// <param name="rtsEnable">RTS Enable</param>
        public ComPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits,
            Handshake handshake, string newLine, int readTimeout, int writeTimeout, bool dtrEnable, bool rtsEnable)
            : this()
        {
            ChangeSettings(portName, baudRate, parity, dataBits, stopBits, handshake, newLine,
                readTimeout, writeTimeout, dtrEnable, rtsEnable);
        }

        #endregion Constructors

        #region Static Methods

        /// <summary>
        /// See if a given port exists.
        /// </summary>
        /// <param name="portName">Port to check existence of.</param>
        /// <returns>True if port is found, false otherwise.</returns>
        public static bool PortExists(string portName)
        {
            return SerialPort.GetPortNames().Contains(portName);
        }

        /// <summary>
        /// Retrieve list of COM ports.
        /// </summary>
        /// <returns>List of COM ports.  If no ports exists, returns null.</returns>
        public static List<string> RetrieveComPortList()
        {
            List<string> comPortList = new List<string>(SerialPort.GetPortNames());

            if (comPortList.Count > 0)
            {
                comPortList.Sort();
                return comPortList;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Retrieve list of baud rates.
        /// </summary>
        /// <returns>List of baud rates.</returns>
        /// <remarks>
        /// I used http://www.developerfusion.com/article/22/com-ports-technical-information/2/ as a guide.
        /// </remarks>
        public static List<int> RetrieveBaudRateList()
        {
            return new List<int>(new int[] { 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 28800, 38400, 56000, 57600, 115200, 128000, 256000 });
        }


        /// <summary>
        /// Retrieve list of Handshake settings.
        /// </summary>
        /// <returns>List of handshake settings.</returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/1167361/how-do-i-convert-an-enum-to-a-list-in-c was helpful.
        /// </remarks>
        public static List<Handshake> RetrieveHandshakeList()
        {
            return Handshake.GetValues(typeof(Handshake)).Cast<Handshake>().ToList();
        }


        /// <summary>
        /// Retrieve list of Data Bits settings.
        /// </summary>
        /// <returns>List of Data Bits settings.</returns>
        /// <remarks>
        /// I used http://www.developerfusion.com/article/22/com-ports-technical-information/4/ as a guide.
        /// </remarks>
        public static List<int> RetrieveDataBitsList()
        {
            return new List<int>(new int[] { 4, 5, 6, 7, 8 });
        }

        /// <summary>
        /// Retrieve list of Parity settings.
        /// </summary>
        /// <returns>List of parity settings.</returns>
        public static List<Parity> RetrieveParityList()
        {
            return Parity.GetValues(typeof(Parity)).Cast<Parity>().ToList();
        }


        /// <summary>
        /// Retrieve list of StopBits settings.
        /// </summary>
        /// <returns>List of StopBits settings.</returns>
        public static List<StopBits> RetrieveStopBitsList()
        {
            return StopBits.GetValues(typeof(StopBits)).Cast<StopBits>().ToList();
        }

        #endregion Static Methods

        #region Public Methods

        /// <summary>
        /// Open the serial port
        /// </summary>
        /// <returns>True if success, false otherwise</returns>
        /// <param name="portName">Port Name</param>
        public bool OpenPort()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    //Set serial port settings
                    serialPort.PortName = this.portName;
                    serialPort.BaudRate = this.baudRate;
                    serialPort.Parity = this.parity;
                    serialPort.DataBits = this.dataBits;
                    serialPort.StopBits = this.stopBits;
                    serialPort.Handshake = this.handshake;
                    serialPort.NewLine = this.newLine;
                    //For timeouts, I accept anything <=0 to mean infinite timeout. This negates the need to check it
                    //when the user enter it, every value will work now. Otherwise you get ArgumentOutOfRangeException.
                    serialPort.ReadTimeout = this.readTimeout <= 0 ? SerialPort.InfiniteTimeout : this.readTimeout;
                    serialPort.WriteTimeout = this.writeTimeout <= 0 ? SerialPort.InfiniteTimeout : this.writeTimeout;
                    serialPort.DtrEnable = this.dtrEnable;
                    serialPort.RtsEnable = this.rtsEnable;
                    serialPort.Open();

                    if (serialPort.IsOpen)
                    {
                        ReportStatus("Port Open");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //Serial port is already open.
                    return false;
                }
            }
            catch (UnauthorizedAccessException)
            {
                //Unauthorized Access - port is probably already in use.
                if (UserInterfaceData != null)
                {
                    string errorMessage = "Access to " + serialPort.PortName +
                    " is denied. It may already be in use by another application or process.";
                    UserInterfaceData(ComPortEvent.ReportException, errorMessage);
                }
                return false;
            }
            catch (System.IO.IOException caught)
            {
                //Port likely doesn't exist, which will be in the message.
                if (UserInterfaceData != null)
                {
                    UserInterfaceData(ComPortEvent.ReportException, caught.Message);
                }
                return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                //There is an invalid setting. Shouldn't happen if GUI limits settings to values provided by this class.
                if (UserInterfaceData != null)
                {
                    string errorMessage = "Cannot open " + serialPort.PortName + ": One or more settings are invalid. Please verify " +
                    "that the settings are correct and try again.";
                    UserInterfaceData(ComPortEvent.ReportException, errorMessage);
                }
                return false;
            }
            catch (Exception caught)
            {
                //Other exceptions
                ReportException(caught);
                return false;
            }
        }


        /// <summary>
        /// Close COM port.
        /// </summary>
        public void ClosePort()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Dispose();
                    ReportStatus("Port Closed");
                    ReportSettings();
                }
            }
            catch (Exception caught)
            {
                ReportException(caught);
            }
        }


        /// <summary>
        /// Change several settings at once.
        /// </summary>
        /// <param name="portName">Port Name</param>
        /// <param name="baudRate">Baud Rate</param>
        /// <param name="parity">Parity</param>
        /// <param name="dataBits">Data Bits</param>
        /// <param name="stopBits">Stop Bits</param>
        /// <param name="handshake">Handshake</param>
        /// <param name="newLine">New Line character</param>
        public void ChangeSettings(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, Handshake handshake, string newLine)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.parity = parity;
            this.dataBits = dataBits;
            this.stopBits = stopBits;
            this.handshake = handshake;
            this.newLine = newLine;

            ReportSettings();
        }


        /// <summary>
        /// Change several settings at once.
        /// </summary>
        /// <param name="portName">Port Name</param>
        /// <param name="baudRate">Baud Rate</param>
        /// <param name="parity">Parity</param>
        /// <param name="dataBits">Data Bits</param>
        /// <param name="stopBits">Stop Bits</param>
        /// <param name="handshake">Handshake</param>
        /// <param name="newLine">New Line character</param>
        /// <param name="readTimeout">Read Timeout</param>
        /// <param name="writeTimeout">Write Timeout</param>
        /// <param name="dtrEnable">DTR Enable</param>
        /// <param name="rtsEnable">RTS Enable</param>
        public void ChangeSettings(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, Handshake handshake, string newLine,
            int readTimeout, int writeTimeout, bool dtrEnable, bool rtsEnable)
        {
            this.readTimeout = readTimeout;
            this.writeTimeout = writeTimeout;
            this.dtrEnable = dtrEnable;
            this.rtsEnable = rtsEnable;

            ChangeSettings(portName, baudRate, parity, dataBits, stopBits, handshake, newLine);
        }


        /// <summary>
        /// Write text COM port.
        /// </summary>
        /// <param name="textToWrite">Text to write to port.</param>
        /// <returns>True if operation succeeds, false otherwise.</returns>
        public bool WriteToComPort(string textToWrite)
        {
            bool success = false;

            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Write(textToWrite);
                    success = true;
                }
            }
            catch (TimeoutException)
            {
                if (UserInterfaceData != null)
                    UserInterfaceData(ComPortEvent.ReportSerialError, "Time out occured on write");
            }
            catch (Exception caught)
            {
                ReportException(caught);
            }

            return success;
        }


        /// <summary>
        /// Method that is called after write to COM port is finished.
        /// </summary>
        /// <param name="ar"></param>
        public void WriteCompleted(IAsyncResult ar)
        {
            WriteToComPortDelegate deleg = null;
            string msg = null;
            bool success = false;

            //Extract the value returned by BeginInvoke (optional)
            msg = ar.AsyncState.ToString();

            //Get the value returned by the delegate.
            deleg = ((WriteToComPortDelegate)(((AsyncResult)(ar)).AsyncDelegate));

            msg = ar.AsyncState.ToString();

            success = deleg.EndInvoke(ar);

            if (UserInterfaceData != null)
            {
                if (success)
                    UserInterfaceData(ComPortEvent.ReportSerialSuccess, msg);
                else
                    UserInterfaceData(ComPortEvent.ReportSerialError, "Write operation started  " + msg + " did not return success");
            }
        }


        /// <summary>
        /// Discard in buffer if port is open.
        /// </summary>
        public void DiscardInBuffer()
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.DiscardInBuffer();
                }
                catch (Exception caught)
                {
                    ReportException(caught);
                }
            }
        }


        /// <summary>
        /// Discard out buffer if port is open.
        /// </summary>
        public void DiscardOutBuffer()
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.DiscardOutBuffer();
                }
                catch (Exception caught)
                {
                    ReportException(caught);
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Handle data received from com port.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string inData = null;

            try
            {
                inData = serialPort.ReadLine();

                if (UserInterfaceData != null)
                    UserInterfaceData(ComPortEvent.ReturnNewData, inData);
            }
            catch (TimeoutException)
            {
                //Timeout error handler
                if (UserInterfaceData != null)
                    UserInterfaceData(ComPortEvent.ReportSerialError, "Timeout Occured.  Buffer contents:" + "'" + serialPort.ReadExisting() + "'");
            }
            catch (Exception caught)
            {
                //Generic error handler
                ReportException(caught);
            }
        }


        /// <summary>
        /// Handle error received from com port.  Report using UserInterfaceData.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            SerialError SerialErrorReceived1 = e.EventType;

            if (UserInterfaceData != null)
            {
                string message;

                switch (SerialErrorReceived1)
                {
                    case SerialError.Frame:
                        message = "Framing error on " + serialPort.PortName;
                        break;
                    case SerialError.Overrun:
                        message = "Character buffer overrun on " + serialPort.PortName;
                        break;
                    case SerialError.RXOver:
                        message = "Input buffer overflow on " + serialPort.PortName;
                        break;
                    case SerialError.RXParity:
                        message = "Parity error on " + serialPort.PortName;
                        break;
                    case SerialError.TXFull:
                        message = "Output buffer full on " + serialPort.PortName;
                        break;
                    default:
                        message = "Unknown error on " + serialPort.PortName;
                        break;
                }

                UserInterfaceData(ComPortEvent.ReportSerialError, message);
            }
        }


        /// <summary>
        /// Report exception using UserInterfaceData.
        /// </summary>
        /// <param name="ex">Exception to report on</param>
        private void ReportException(Exception ex)
        {
            if (UserInterfaceData != null)
            {
                string errorMessage = "Exception occured in ComPort (" + serialPort.PortName + "): " + ex.Message +
                    Environment.NewLine + Environment.NewLine + ex.ToString();
                UserInterfaceData(ComPortEvent.ReportException, errorMessage);
            }
        }


        /// <summary>
        /// Report settings using UserInterfaceData.
        /// </summary>
        private void ReportSettings()
        {
            if (UserInterfaceData != null)
            {
                string settings = portName + "," + baudRate.ToString() + "," + dataBits.ToString() + "," +
                    parity.ToString() + "," + stopBits.ToString() + "," + handshake.ToString();
                UserInterfaceData(ComPortEvent.ReportSettings, settings);
            }
        }


        /// <summary>
        /// Report status using UserInterfaceData.
        /// </summary>
        /// <param name="statusText"></param>
        private void ReportStatus(string statusText)
        {
            if (UserInterfaceData != null)
            {
                UserInterfaceData(ComPortEvent.ReportStatus, statusText);
            }
        }

        #endregion Private Methods

    }
}