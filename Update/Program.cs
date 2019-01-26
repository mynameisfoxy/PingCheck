using System;
using System.Windows.Forms;

namespace Update
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        [STAThread]
        static void Main(string[] param)
        {
            bool versParam = false;
            CheckAndUpdate chk = new CheckAndUpdate();
            chk.InitializeSettingsFile();
            if (param.Length > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    switch (param[i].ToString())
                    {
                        case ("/version"):
                            Console.WriteLine(Application.ProductName + " [" + Application.ProductVersion.ToString() + "]");
                            versParam = true;
                            FreeConsole();
                            break;
                    }
                }
            }
            if (param.Length < 1 && !versParam)
            {
                FreeConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            
        }
    }
}
