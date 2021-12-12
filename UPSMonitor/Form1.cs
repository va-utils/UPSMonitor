using System;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using UPSCls;
using System.Diagnostics;
using HidLibrary;
using System.Linq;
using System.Collections.Generic;
namespace UPSMonitor
{
    public partial class MainForm : Form
    {
        bool ready;
        UPS ups;
        FileStream logStream;

        public void SaveSettings()
        {
         
            if (WindowState != FormWindowState.Minimized)
                Properties.Settings.Default.Position = this.Location;
            Properties.Settings.Default.PortName = (string)cbPortName.SelectedItem;
            Properties.Settings.Default.LogOnStart = cbLog.Checked;
            Properties.Settings.Default.Interval = (int)nudInterval.Value;
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.PortName))
                Properties.Settings.Default.Save();
            else
                MessageBox.Show("Не выбран COM-порт, продолжение невозможно.");
        }

        public void LoadSettings()
        {
            Properties.Settings.Default.Reload();
            Location = Properties.Settings.Default.Position;

            nudInterval.Value = Properties.Settings.Default.Interval;
            cbLog.Checked = Properties.Settings.Default.LogOnStart;
            List<string> ports = System.IO.Ports.SerialPort.GetPortNames().ToList(); // получим список всех портов в системе

            HidDevice dev; 
            if ( (dev = HidDevices.Enumerate(0x665, 0x5161).FirstOrDefault()) != null)
            {
                ports.Add("USB 0x665 0x5161");
            }
            else if ((dev = HidDevices.Enumerate(0x6da, 0x3).FirstOrDefault()) != null)
            {
                ports.Add("USB 0x6DA 0x3");
            }

            cbPortName.Items.Clear();
            cbPortName.Items.AddRange(ports.ToArray());

            if (String.IsNullOrWhiteSpace(Properties.Settings.Default.PortName)) //если COM-порт не сохранен в настройках
            {
                if (ports.Count > 0) 
                {
                    tabControl1.SelectedIndex = 1;
                }
                else // если нет ругаемся и запрещает обмен
                {
                    MessageBox.Show("COM-порты недоступны. Продолжение невозможно.");
                    ready = false;
                }
            }
            else // COM-порт сохранен в настройках, выбираем его, разрешаем обмен
            {
                cbPortName.SelectedItem = Properties.Settings.Default.PortName;
                ready = true;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            ups = UPS.Instance;
            logStream = null;
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
                timer1.Stop();
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
            if(WindowState != FormWindowState.Minimized)
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
            string about = string.Format("Версии сборок:" + "\nUPSMonitor: {0}\nUPSCls: {1}\nUPSLogViewer: {2}\n\n© Vyalichkin V.A. \nviktor70@protonmail.com\n\nИконка: icon lauk", Application.ProductVersion, cls.ProductVersion, vwr.ProductVersion);
            this.about.Text = about;
            //--------------------------

            LoadSettings();

            Start();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            btSaveButton.Enabled = false;
            Stop();
            SaveSettings();
            LoadSettings();
            Start();
            btSaveButton.Enabled = true;
        }

        private void Stop()
        {
            timer1.Stop();
            if (ups.IsPortOpen)
            {
                ups.StopExchange();
                if (logStream != null)
                {
                    logStream.Close();
                    logStream = null;
                }
            }
        }

        private void Start()
        {
            if(ready==true)
            {
                ups.StartExchange(Properties.Settings.Default.PortName);
                if (ups.IsPortOpen)
                {
                    if (Properties.Settings.Default.LogOnStart) //пользователь хочет писать лог при запуске программы
                    {
                        string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "UPSMonitor");
                        if(Directory.Exists(dir))
                            logStream = new FileStream(Path.Combine(dir, "log-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".upslog"), FileMode.Create, FileAccess.Write, FileShare.Read, 8);
                        else
                        {
                            Directory.CreateDirectory(dir);
                            logStream = new FileStream(Path.Combine(dir,"log-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".upslog"), FileMode.Create, FileAccess.Write, FileShare.Read, 8);
                        }
                    }
                    timer1.Interval = Properties.Settings.Default.Interval;
                    timer1.Enabled = true;
                    timer1.Start();
                }
            }
            else
            {
                lblErr.Visible = true;
            }
        }

        private void updateLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://github.com/va-utils/UPSMonitor/releases");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось запустить браузер для посещения страницы загрузок: " + ex.Message);
            }
            
        }
    }
}
