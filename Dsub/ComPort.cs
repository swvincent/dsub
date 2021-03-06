﻿///////////////////////////////////////////////////////////////////////////////////////////////////
/// dsub
/// www.swvincent.com/dsub
/// www.github.com/swvincent/dsub
/// 
/// This class has been written for .NET Framework 4.7.
/// 
/// MIT License
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
///////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;

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

        public delegate void UserInterfaceDataEventHandler(ComPortEvent comPortEvent, string message);
        public static event UserInterfaceDataEventHandler UserInterfaceData;
        public delegate bool WriteTextDelegate(string textToWrite);
        public WriteTextDelegate writeDelegate;
        private SerialPort serialPort;
        private readonly SerialDataReceivedEventHandler dataReceivedEventHandler;
        private readonly SerialErrorReceivedEventHandler errorReceivedEventHandler;

        //Local variables available as properties

        private string portName = string.Empty;
        private int baudRate = 9600;
        private Parity parity = Parity.None;
        private int dataBits = 8;
        private StopBits stopBits = StopBits.One;

        #endregion Declarations

        #region Properties

        public string PortName { get { return portName; } set { portName = value; ReportSettings(); } }
        public int BaudRate { get { return baudRate; } set { baudRate = value; ReportSettings(); } }
        public Parity Parity { get { return parity; } set { parity = value; ReportSettings(); } }
        public int DataBits { get { return dataBits; } set { dataBits = value; ReportSettings(); } }
        public StopBits StopBits { get { return stopBits; } set { stopBits = value; ReportSettings(); } }
        public Handshake Handshake { get; set; } = Handshake.None;
        public string NewLine { get; set; } = Environment.NewLine;
        public int ReadTimeout { get; set; } = 500;
        public int WriteTimeout { get; set; } = 500;
        public bool DtrEnable { get; set; } = false;
        public bool RtsEnable { get; set; } = false;
        public bool IsOpen => serialPort.IsOpen;

        #endregion Properties

        #region Constructors

        public ComPort()
        {
            //Create the new port and report that it's closed.
            serialPort = new SerialPort();

            dataReceivedEventHandler = new SerialDataReceivedEventHandler(DataReceived);
            errorReceivedEventHandler = new SerialErrorReceivedEventHandler(ErrorReceived);
            serialPort.DataReceived += dataReceivedEventHandler;
            serialPort.ErrorReceived += errorReceivedEventHandler;
            
            ReportStatus("Port Closed");
        }


        public ComPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits,
            Handshake handshake, string newLine) : this()
        {
            ChangeSettings(portName, baudRate, parity, dataBits, stopBits, handshake, newLine);
        }


        public ComPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits,
            Handshake handshake, string newLine, int readTimeout, int writeTimeout, bool dtrEnable, bool rtsEnable)
            : this()
        {
            ChangeSettings(portName, baudRate, parity, dataBits, stopBits, handshake, newLine,
                readTimeout, writeTimeout, dtrEnable, rtsEnable);
        }

        #endregion Constructors

        #region Static Methods

        public static bool PortExists(string portName)
        {
            return SerialPort.GetPortNames().Contains(portName);
        }

        public static List<string> RetrieveComPortList()
        {
            List<string> comPortList = new List<string>(
                SerialPort.GetPortNames().OrderBy(p => p));

            if (comPortList.Any())
                return comPortList;
            else
                return null;
        }


        /// <remarks>
        /// I used http://www.developerfusion.com/article/22/com-ports-technical-information/2/ as a guide.
        /// </remarks>
        public static List<int> RetrieveBaudRateList()
        {
            return new List<int>(new int[] { 110, 300, 600, 1200, 2400, 4800, 9600,
                14400, 19200, 28800, 38400, 56000, 57600, 115200, 128000, 256000 });
        }


        /// <remarks>
        /// http://stackoverflow.com/questions/1167361/how-do-i-convert-an-enum-to-a-list-in-c was helpful.
        /// </remarks>
        public static List<Handshake> RetrieveHandshakeList()
        {
            return Handshake.GetValues(typeof(Handshake))
                .Cast<Handshake>().ToList();
        }


        /// <remarks>
        /// I used http://www.developerfusion.com/article/22/com-ports-technical-information/4/ as a guide.
        /// </remarks>
        public static List<int> RetrieveDataBitsList()
        {
            return new List<int>(new int[] { 4, 5, 6, 7, 8 });
        }

        
        public static List<Parity> RetrieveParityList()
        {
            return Parity.GetValues(typeof(Parity))
                .Cast<Parity>().ToList();
        }

        
        public static List<StopBits> RetrieveStopBitsList()
        {
            return StopBits.GetValues(typeof(StopBits))
                .Cast<StopBits>().ToList();
        }

        #endregion Static Methods

        #region Public Methods

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
                    serialPort.Handshake = this.Handshake;
                    serialPort.NewLine = this.NewLine;
                    //For timeouts, I accept anything <=0 to mean infinite timeout. This negates the need to check it
                    //when the user enter it, every value will work now. Otherwise you get ArgumentOutOfRangeException.
                    serialPort.ReadTimeout = this.ReadTimeout <= 0 ? SerialPort.InfiniteTimeout : this.ReadTimeout;
                    serialPort.WriteTimeout = this.WriteTimeout <= 0 ? SerialPort.InfiniteTimeout : this.WriteTimeout;
                    serialPort.DtrEnable = this.DtrEnable;
                    serialPort.RtsEnable = this.RtsEnable;
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
                string errorMessage = $"Access to {serialPort.PortName} is denied. " +
                        "It may already be in use by another application or process.";
                UserInterfaceData?.Invoke(ComPortEvent.ReportException, errorMessage);
                return false;
            }
            catch (System.IO.IOException caught)
            {
                //Port doesn't exist, or a parameter isn't supported, such as baud rate, etc.
                string errorMessage = "An I/O error occured. The specified port may not exist, " +
                    "or one or more of the specified parameters may not be supported. More details:" +
                    $"\n\n{caught.Message}";
                UserInterfaceData?.Invoke(ComPortEvent.ReportException, errorMessage);
                return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                //There is an invalid setting. Shouldn't happen if GUI limits
                //settings to values provided by this class.
                string errorMessage = $"Cannot open {serialPort.PortName}: " +
                        "One or more settings are invalid. Please verify " +
                        "that the settings are correct and try again.";
                UserInterfaceData?.Invoke(ComPortEvent.ReportException, errorMessage);
                return false;
            }
            catch (Exception caught)
            {
                ReportException(caught);
                return false;
            }
        }


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


        public void ChangeSettings(string portName, int baudRate, Parity parity, int dataBits,
            StopBits stopBits, Handshake handshake, string newLine)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.parity = parity;
            this.dataBits = dataBits;
            this.stopBits = stopBits;
            this.Handshake = handshake;
            this.NewLine = newLine;

            ReportSettings();
        }


        public void ChangeSettings(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits,
            Handshake handshake, string newLine, int readTimeout, int writeTimeout, bool dtrEnable, bool rtsEnable)
        {
            this.ReadTimeout = readTimeout;
            this.WriteTimeout = writeTimeout;
            this.DtrEnable = dtrEnable;
            this.RtsEnable = rtsEnable;

            ChangeSettings(portName, baudRate, parity, dataBits, stopBits, handshake, newLine);
        }


        public bool WriteText(string textToWrite)
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
                UserInterfaceData?.Invoke(ComPortEvent.ReportSerialError, "Time out occured on write");
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
            WriteTextDelegate deleg = null;
            string msg = null;
            bool success = false;

            //Extract the value returned by BeginInvoke (optional)
            msg = ar.AsyncState.ToString();

            //Get the value returned by the delegate.
            deleg = ((WriteTextDelegate)(((AsyncResult)(ar)).AsyncDelegate));

            msg = ar.AsyncState.ToString();

            success = deleg.EndInvoke(ar);

            if (success)
                UserInterfaceData?.Invoke(ComPortEvent.ReportSerialSuccess, msg);
            else
                UserInterfaceData?.Invoke(ComPortEvent.ReportSerialError,
                    $"Write operation started {msg} did not return success");
        }


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

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string inData = null;

            try
            {
                inData = serialPort.ReadLine();
                UserInterfaceData?.Invoke(ComPortEvent.ReturnNewData, inData);
            }
            catch (TimeoutException)
            {
                //Timeout error handler
                UserInterfaceData?.Invoke(ComPortEvent.ReportSerialError,
                    $"Timeout Occured. Buffer contents: '{serialPort.ReadExisting()}'");
            }
            catch (Exception caught)
            {
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

            string message;

            switch (SerialErrorReceived1)
            {
                case SerialError.Frame:
                    message = $"Framing error on {serialPort.PortName}";
                    break;
                case SerialError.Overrun:
                    message = $"Character buffer overrun on {serialPort.PortName}";
                    break;
                case SerialError.RXOver:
                    message = $"Input buffer overflow on {serialPort.PortName}";
                    break;
                case SerialError.RXParity:
                    message = $"Parity error on {serialPort.PortName}";
                    break;
                case SerialError.TXFull:
                    message = $"Output buffer full on {serialPort.PortName}";
                    break;
                default:
                    message = $"Unknown error on {serialPort.PortName}";
                    break;
            }

            UserInterfaceData?.Invoke(ComPortEvent.ReportSerialError, message);
        }


        /// <summary>
        /// Report exception using UserInterfaceData.
        /// </summary>
        /// <param name="ex">Exception to report on</param>
        private void ReportException(Exception ex)
        {
            string errorMessage = $"Exception occured in ComPort ({serialPort.PortName}): {ex.Message}\n\n{ex.ToString()}";
            UserInterfaceData?.Invoke(ComPortEvent.ReportException, errorMessage);
         
        }


        /// <summary>
        /// Report settings using UserInterfaceData.
        /// </summary>
        private void ReportSettings()
        {
            string settings = $"{portName}, {baudRate}, {dataBits}, {parity}, {stopBits}, {Handshake}";
            UserInterfaceData?.Invoke(ComPortEvent.ReportSettings, settings);
        }


        /// <summary>
        /// Report status using UserInterfaceData.
        /// </summary>
        /// <param name="statusText"></param>
        private void ReportStatus(string statusText)
        {
            UserInterfaceData?.Invoke(ComPortEvent.ReportStatus, statusText);
        }

        #endregion Private Methods

    }
}
