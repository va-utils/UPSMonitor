using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using HidLibrary;
namespace UPSCls
{
    public struct UPSStatus
    {
        public float Frequency { get; set; }
        public float Temperature { get; set; }
        public float BattaryVoltage { get; set; }
        public float InputVoltage { get; set; }
        public float OutputVoltage { get; set; }
        public float FaultVoltage { get; set; }
        public int Loading { get; set; }
        public DateTime LastUpdateDateTime { get; set; }

        public void SaveStatus(FileStream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(LastUpdateDateTime.ToBinary());
            writer.Write(InputVoltage);
            writer.Write(OutputVoltage);
            writer.Write(BattaryVoltage);
            writer.Write(Frequency);
            writer.Write(Temperature);
            writer.Write(Loading);
            writer.Flush();
        }

        public void LoadStatus(FileStream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            LastUpdateDateTime = DateTime.FromBinary(reader.ReadInt64());
            InputVoltage = reader.ReadSingle();
            OutputVoltage = reader.ReadSingle();
            BattaryVoltage = reader.ReadSingle();
            Frequency = reader.ReadSingle();
            Temperature = reader.ReadSingle();
            Loading = reader.ReadInt32();
        }
    }

    public class UPSConnectEventArgs : EventArgs
    {
        public bool IsOK { get; set; }
        public UPSConnectEventArgs(bool isOK)
        {
            this.IsOK = isOK;
        }
    }
    public class UPS
    {
        public event EventHandler<UPSConnectEventArgs> UPSConnectStatusUpdated;
        SerialPort serialPort;
        HidDevice usbDevice;
        public UPSStatus status;
        bool isUSB = false;
        //---костыли для usb--
        int cnt = 0;
        StringBuilder sb = new StringBuilder();
        bool usb_flag = false; //флаг разрешающий работу
        bool usb_read_mode = false;
        //--------------------

        public bool IsPortOpen
        {
            get
            {
                if(isUSB)
                {
                    if (usbDevice != null)
                    {
                        return usbDevice.IsOpen;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (serialPort != null)
                    {
                        return serialPort.IsOpen;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }

        private UPS()
        {
          //  Trace.Listeners.Clear();
          //  Trace.Listeners.Add(new TextWriterTraceListener("upscls.log"));
            Trace.AutoFlush = true;
            string aboutSystem = "";
            aboutSystem += "OS:" + Environment.OSVersion + "\n";
            aboutSystem += "COM-ports:\n";
            foreach(string s in SerialPort.GetPortNames())
            {
                aboutSystem += s + "\n";
            }
            Trace.WriteLine(aboutSystem);
            
        }

        private static UPS instance = new UPS();

        public static UPS Instance
        {
            get { return instance; }
        }

        protected virtual void OnUPSConnectStatusUpdated(bool b)
        {
            if (UPSConnectStatusUpdated != null)
            {
                UPSConnectStatusUpdated(this, new UPSConnectEventArgs(b));
            }
        }

        private string CreateLogString(string s)
        {
            return DateTime.Now.ToLongTimeString() + " : " + s;
        }

        private void Parse(string strValues)
        {
            try
            { 
                strValues = strValues.Remove(0, 1);
                String[] values = strValues.Split(' ');
                status.InputVoltage = float.Parse(values[0], NumberStyles.Any, CultureInfo.InvariantCulture);
                status.FaultVoltage = float.Parse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture);
                status.OutputVoltage = float.Parse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture);
                status.Loading = int.Parse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture);
                status.Frequency = float.Parse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture);
                status.BattaryVoltage = float.Parse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture);
                status.Temperature = float.Parse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch(FormatException fe)
            {
                Trace.WriteLine(CreateLogString(" из Parse() " + fe.Message));
            }
            catch (Exception e)
            {
                Trace.WriteLine(CreateLogString(" из Parse() " + e.Message));
            }

        }

        private void TextReport(HidReport report)
        {
            sb.Append(Encoding.ASCII.GetString(report.Data));
            cnt++;
            if (cnt == 6)
            {
                cnt = 0;
                string result = sb.ToString();
                usb_read_mode = false;
                sb.Clear();
                Parse(result);
                Trace.WriteLine(CreateLogString(" << " + result));
                status.LastUpdateDateTime = DateTime.Now;
                OnUPSConnectStatusUpdated(true);
            }
            else
            {
                usbDevice.ReadReport(TextReport);
            }
        }

        public void UpdateStatus_USB()
        {
            if(usb_flag==false) //если останавливаем работу, новых запросов не делаем.
            {
                return;
            }
            if(usb_read_mode==true) //читаем старое, не готовы к новым посылкам
            {
                return;
            }
            try
            {
                byte[] forWrite = Encoding.ASCII.GetBytes("Q1\r");
                Trace.WriteLine(CreateLogString(" >> " + "Q1"));
              
                //int bytes = serialPort.BytesToRead;

                //---посылка через HID---
                HidReport r_forWrite = new HidReport(forWrite.Length);
                r_forWrite.Data = forWrite;
                usbDevice.WriteReport(r_forWrite);
                //-----------------------------------

                //---прием через HID---
                usb_read_mode = true;
                usbDevice.ReadReport(TextReport);   
            }
            catch (InvalidOperationException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine(e.Message);
                OnUPSConnectStatusUpdated(false);
            }
            catch (TimeoutException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine(e.Message);
                OnUPSConnectStatusUpdated(false);
            }
            catch (FormatException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine(e.Message);
                OnUPSConnectStatusUpdated(false);
            }
        }

        public void UpdateStatus()
        {
            if(isUSB)
            {
                UpdateStatus_USB();
                return;
            }
            try
            {
                byte[] forWrite = Encoding.ASCII.GetBytes("Q1\r");
                Trace.WriteLine(CreateLogString(" >> " + "Q1"));
                serialPort.Write(forWrite, 0, forWrite.Length);
                int bytes = serialPort.BytesToRead;

                Trace.WriteLine(CreateLogString("Bytes to Read: " + bytes));
                if (bytes > 0)
                {
                    byte[] ans = new byte[bytes];
                    serialPort.Read(ans, 0, bytes);
                    String result = Encoding.ASCII.GetString(ans);
                    Trace.WriteLine(CreateLogString(" << " + result));
                    Parse(result);
                    status.LastUpdateDateTime = DateTime.Now;
                    OnUPSConnectStatusUpdated(true);
                }
            }
            catch (InvalidOperationException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine(e.Message);
                OnUPSConnectStatusUpdated(false);
            }
            catch (TimeoutException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine(e.Message);
                OnUPSConnectStatusUpdated(false);
            }
            catch (FormatException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine(e.Message);
                OnUPSConnectStatusUpdated(false);
            }
        }

        public void StartExchange_USB()
        {
            try
            {
                Trace.WriteLine(CreateLogString("Port: USB"));
                Trace.WriteLine(CreateLogString("Open port..."));
                usbDevice.OpenDevice();
                usb_flag = true;
            }
            catch (UnauthorizedAccessException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine("USB Error: " + e.Message);
                OnUPSConnectStatusUpdated(false);
            }
            catch (IOException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine("USB Error: " + e.Message);
                OnUPSConnectStatusUpdated(false);
            }
            catch (Exception e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine("USB Error: " + e.Message);
                OnUPSConnectStatusUpdated(false);
            }
        }

        public void StartExchange(string portName)
        {
            if(portName.Contains("USB"))
            {
                string[] tokens = portName.Split(' ');
                int vendorId = Convert.ToInt32(tokens[1],16);
                int productId = Convert.ToInt32(tokens[2],16);
                Trace.WriteLine(CreateLogString("Trying to start the exchange USB"));
                Trace.WriteLine(CreateLogString(String.Format("USB Vendor ID {0:X} Product ID: {1:X}",vendorId,productId)));
                usbDevice = HidDevices.Enumerate(vendorId, productId).FirstOrDefault();
                if (usbDevice != null)
                {
                    isUSB = true;
                    Trace.WriteLine(CreateLogString("Режим USB-HID"));
                    StartExchange_USB();
                }
                return;
            }
            try
            {
                Trace.WriteLine(CreateLogString("Port:" + portName));
                Trace.WriteLine(CreateLogString("Trying to start the exchange RS"));
                serialPort = new SerialPort(portName, 2400, Parity.None, 8, StopBits.One);
                Trace.WriteLine(CreateLogString("Open port..."));
                serialPort.Open();
                serialPort.WriteTimeout=500;
                serialPort.ReadTimeout=500;
              //  OnUPSConnectStatusUpdated(true);
            }
            catch (UnauthorizedAccessException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine("RS Error: " + e.Message);
                OnUPSConnectStatusUpdated(false);
            }
            catch (IOException e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine("RS Error: " + e.Message);
                OnUPSConnectStatusUpdated(false);
            }
            catch(Exception e)
            {
                Trace.WriteLine(CreateLogString(e.Message));
                Console.WriteLine("RS Error: " + e.Message);
                OnUPSConnectStatusUpdated(false);
            }
        }

        public void StopExchange()
        {
            try
            {
                if (isUSB)
                {
                    Trace.WriteLine(CreateLogString("Trying to stop the exchange USB"));
                    usb_flag = false;
                    Thread.Sleep(500);
                    usbDevice.CloseDevice();
                    Trace.WriteLine(CreateLogString("Closed port (USB)"));

                }
                else
                {
                    Trace.WriteLine(CreateLogString("Trying to stop the exchange RS"));
                    serialPort.Close();
                    Trace.WriteLine(CreateLogString("Closed port (COM)"));
                }
            }
            catch(IOException ex)
            {
                Trace.WriteLine(CreateLogString(ex.Message));
            }

        }
    }
}
