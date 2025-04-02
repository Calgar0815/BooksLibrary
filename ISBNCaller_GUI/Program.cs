using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISBNCaller_GUI
{
    internal static class Program
    {

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string fileNameWOutExtension = System.IO.Path.GetFileNameWithoutExtension(location);
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(fileNameWOutExtension);
            if (processes.Count() > 1)
            {
                MessageBox.Show("Das Programm läuft schon.");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.SetDefaultFont(new Font(new FontFamily("Microsoft Sans Serif"), 8f));
                Application.Run(new Form1());
            } // else
        }
    }
}
