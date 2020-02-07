using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using UPSCls;
using System.Diagnostics;

namespace UPSLogViewer
{
    public partial class MainForm : Form
    {
        List<UPSStatus> log;
        string fileName;
        public MainForm()
        {
            InitializeComponent();
            lvLog.VirtualMode = true;
        }

        void GenerateTable(string fname)
        {
            lvLog.VirtualListSize = 0;

            using (FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) 
            {
                
                log = new List<UPSStatus>(3600);

                while (fs.Position < fs.Length)
                {
                    UPSStatus u = new UPSStatus();
                    u.LoadStatus(fs);
                    log.Add(u);
                }

                lvLog.VirtualListSize = log.Count;
            }
        }


        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.DefaultExt = ".upslog";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    fileName = ofd.FileName;
                    GenerateTable(fileName);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(fileName))
            {
                GenerateTable(fileName);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string help = @"Программа UPSLogViewer предназначена для просмотра отчетов, сформированных UPSMonitor.
Файл отчета представляет собой бинарный файл, в котором записаны дата и время, входное и выходное напряжение и напряжение батареи, частота, температура и нагрузка.
UPSLogViewer показывает эту информацию в табличном виде. Разбор больших файлов может занять время.";
            MessageBox.Show(help);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileVersionInfo cls = FileVersionInfo.GetVersionInfo(@"UPSCls.dll");
            FileVersionInfo mnt = FileVersionInfo.GetVersionInfo(@"UPSMonitor.exe");
            string about = string.Format("Версии сборок:\nUPSLogViewer: {0}\nUPSCls: {1}\nUPSMonitor: {2}\nАвтор: Vyalichkin V.A. E - mail: viktor70 @protonmail.com\n\nИконка: icon lauk", Application.ProductVersion, cls.ProductVersion,mnt.ProductVersion);
            MessageBox.Show(about);
        }

        private void lvLog_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        { 
            e.Item = new ListViewItem(log[e.ItemIndex].LastUpdateDateTime.ToLongTimeString());
            e.Item.SubItems.Add(Convert.ToString(log[e.ItemIndex].InputVoltage));
            e.Item.SubItems.Add(Convert.ToString(log[e.ItemIndex].OutputVoltage));
            e.Item.SubItems.Add(Convert.ToString(log[e.ItemIndex].BattaryVoltage));
            e.Item.SubItems.Add(Convert.ToString(log[e.ItemIndex].Frequency));
            e.Item.SubItems.Add(Convert.ToString(log[e.ItemIndex].Temperature));
            e.Item.SubItems.Add(Convert.ToString(log[e.ItemIndex].Loading));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if(args.Length > 1)
            {
                GenerateTable(args[1]);
            }
        }
    }
}
