using System;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using UPSCls;
using System.Diagnostics;

namespace UPSMonitor
{
    public partial class MainForm : Form
    {
        string portName;
        UPS ups;
        FileStream logStream;
        public MainForm()
        {
            InitializeComponent();
            ups = UPS.Instance;
            ups.UPSConnectStatusUpdated += this.UPSStatusUpdated;
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
        }

        void OnApplicationExit(object sender, EventArgs e)
        {
            if (ups.IsPortOpen)
                ups.StopExchange();
            if (logStream != null)
                logStream.Close();
        }

        void UPSStatusUpdated(object sender, UPSConnectEventArgs e)
        {
            if (e.IsOK)
            {
                if (logStream != null)
                {
                     ups.status.SaveStatus(logStream); 
                }
                lblErr.Visible = false;
                lbInVoltage.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0}V", ups.status.InputVoltage);
                lbCurrent.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), "Нагрузка: {0}%", ups.status.Loading);
                lbFrequency.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), "Частота: {0}Hz", ups.status.Frequency);
                lbAccVoltage.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), "Напр. аккумулятора: {0}V", ups.status.BattaryVoltage);
                lbTemperature.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), "Температура: {0}C", ups.status.Temperature);
                lbOutVoltage.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), "Выходное напряжение: {0}V", ups.status.OutputVoltage);
                notifyIcon1.Text = "UPSStatus" + "\n" + string.Format(CultureInfo.GetCultureInfo("en-US"), "Vin = {0}V, Vout = {1}V, {2}%, {3}C", ups.status.InputVoltage, ups.status.OutputVoltage, ups.status.Loading, ups.status.Temperature);
            }
            else
            {
                lblErr.Visible = true;
                notifyIcon1.Text = "UPSStatus" + "\n" + "Сбой подключения к ИБП...";
            }
        }



        private void MinimizeToTray()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        private void ShowFromTray()
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Properties.Settings.Default.Position = this.Location;
            Properties.Settings.Default.Save();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                MinimizeToTray();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ups.UpdateStatus();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                MinimizeToTray();

            }
            else if (WindowState == FormWindowState.Minimized)
            {
                ShowFromTray();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Application.Exit();
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            //для вкладке "О программе"
            FileVersionInfo cls = FileVersionInfo.GetVersionInfo(@"UPSCls.dll");
            FileVersionInfo vwr = FileVersionInfo.GetVersionInfo(@"UPSLogViewer.exe");
            string about = string.Format("Версии сборок:" + "\nUPSMonitor: {0}\nUPSCls: {1}\nUPSLogViewer: {2}\n\n© Vyalichkin V.A. \nviktor70@protonmail.com", Application.ProductVersion, cls.ProductVersion, vwr.ProductVersion);
            this.about.Text = about;
            //--------------------------

            this.Location = Properties.Settings.Default.Position;
            //для настроек
            nudInterval.Value = Properties.Settings.Default.Interval;
            timer1.Interval = Properties.Settings.Default.Interval;
            cbLog.Checked = Properties.Settings.Default.LogOnStart;
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            if(ports.Length == 0)
            {
                MessageBox.Show("COM-порты не найдены, продолжение невозможно.");
               // return;
            }
            else
            {
                cbPortName.Items.AddRange(ports);
                
                portName = Properties.Settings.Default.PortName;
                if(!String.IsNullOrWhiteSpace(portName))
                {
                    portName = ports[0];
                    cbPortName.SelectedItem = portName;
                }
            }
            //----------------------------

            //TODO: учесть ситуацию, что пользователь изменил размер экрана


            Start();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.LogOnStart = cbLog.Checked;
            Properties.Settings.Default.Interval = Convert.ToInt32(nudInterval.Value);

            if (!String.IsNullOrWhiteSpace((string)cbPortName.SelectedItem))
                Properties.Settings.Default.PortName = (string)cbPortName.SelectedItem;

            Properties.Settings.Default.Save();

            //--------------------------------

            Stop();
            Start();
        }

        private void Stop()
        {
            timer1.Stop();
            ups.StopExchange();
            if (logStream != null)
                logStream.Close();
        }

        private void Start()
        {
            if(!String.IsNullOrWhiteSpace(portName))
            {
                ups.StartExchange(Properties.Settings.Default.PortName);
                if (ups.IsPortOpen)
                {
                    if (Properties.Settings.Default.LogOnStart) //пользователь хочет писать лог при запуске программы
                    {
                        logStream = new FileStream("log-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".upslog", FileMode.Create, FileAccess.Write, FileShare.Read, 8);
                    }
                    timer1.Enabled = true;
                    timer1.Start();
                }
            }
            else
            {
                lblErr.Visible = true;
            }
        }
    }
}
