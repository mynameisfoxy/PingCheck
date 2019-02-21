using System;
using System.IO;
using System.Windows.Forms;
using UpdaterClass;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool versParam = false;
            CheckAndUpdate chk = new CheckAndUpdate();
            chk.SetTempPath(Path.GetTempPath() + "PingCheck");
            chk.SetAppFileName("pingCheck");
            chk.SetUpdateFileName("Update");
            chk.SetUpdateUrl("https://raw.githubusercontent.com/mynameisfoxy/PingCheckUpdate/master/");

            chk.RunUpdate(UpdateType.Application);
            
            if (param.Length > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    switch (param[i].ToString())
                    {
                        case ("/version"):
                            Console.WriteLine(Application.ProductName + " [" + Application.ProductVersion.ToString() + "]");
                            versParam = true;
                            //FreeConsole();
                            break;
                    }
                }
            }
            if (param.Length < 1 && !versParam)
            {
                FreeConsole();
                
                Application.Run(new Form1());
            }
            
        }
    }
}
