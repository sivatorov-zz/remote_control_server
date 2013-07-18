using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace remote_control_server
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new form_remote_control_server());
        }
    }
}
