using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPSMonitor
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static bool onlyInstance;
        static Mutex mtx = new Mutex(true, "VVA UPSMonitor", out onlyInstance); // используйте имя вашего приложения
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (onlyInstance)
                Application.Run(new MainForm());
            else
                MessageBox.Show("UPSMonitor уже запущен!");
        }
    }
}
