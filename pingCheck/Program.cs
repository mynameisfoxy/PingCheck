using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace pingCheck
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
            short c = 0, error = 0;
            bool versParam = false;

            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey softwareKey = currentUserKey.OpenSubKey("Software", true);
            RegistryKey pingCheckKey = softwareKey.CreateSubKey("PingCheck");
            Version currentVersion = new Version(Application.ProductVersion);
            
            pingCheckKey.SetValue("Last run", DateTimeOffset.Now.ToUnixTimeSeconds(), RegistryValueKind.QWord);
            if (pingCheckKey.GetValue("Version") != null)
            {
                Version registryVersion = new Version(pingCheckKey.GetValue("Version").ToString());
                if (currentVersion > registryVersion)
                {
                    pingCheckKey.SetValue("Version", Application.ProductVersion.ToString());
                }
            }
            else
                pingCheckKey.SetValue("Version", Application.ProductVersion.ToString());
            pingCheckKey.Close();
            softwareKey.Close();

            if (param.Length > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    switch (param[i].ToString())
                    {
                        case "/ip":
                            if (param.Length>i+1)
                            {
                                if (Regex.IsMatch(param[i + 1].ToLower(), @"^(http[s]?:\/\/\[a-z]*\.[a-z]{3}\.[a-z]{2})|([a-z]*\.[a-z]{3})") ||
                                ipValid.IsValidIP(param[i + 1].ToLower()))
                                {
                                    c++;
                                }
                                else
                                {
                                    if (error < 1)
                                    {
                                        Console.WriteLine("Неверный параметр");
                                        Console.WriteLine("/help для справки");
                                        error++;
                                    }
                                    c = 0;
                                }
                            }
                            else
                            {
                                if (error < 1)
                                {
                                    Console.WriteLine("Неверный параметр");
                                    Console.WriteLine("/help для справки");
                                    error++;
                                }
                                c = 0;
                            }
                            
                            break;
                        case ("/time"):
                            if (param.Length > i + 1)
                            {
                                if (int.TryParse(param[i + 1], out int result))
                                    if (int.Parse(param[i + 1]) > 3 && int.Parse(param[i + 1]) < 10)
                                    {
                                    c++;
                                    }
                                    else
                                    {
                                        if (error < 1)
                                        {
                                            Console.WriteLine("Неверный параметр");
                                            Console.WriteLine("/help для справки");
                                            error++;
                                        }
                                        c = 0;
                                    }
                            }
                            else
                            {
                                if (error < 1)
                                {
                                    Console.WriteLine("Неверный параметр");
                                    Console.WriteLine("/help для справки");
                                    error++;
                                }
                                c = 0;
                            }
                            break;
                        case ("/version"):
                            Console.WriteLine(Application.ProductName+" ["+Application.ProductVersion.ToString()+"]");
                            versParam = true;
                            FreeConsole();
                            break;
                        case ("/help"):
                            Console.WriteLine("/ip <DNS|IpV4|IpV6> - IP или DNS адресс ресурса");
                            Console.WriteLine("/time <3-10> - Интервал пинга в секундах");
                            Console.WriteLine("/version - Версия программы");
                            Console.WriteLine("/help - Справка по коммандам");
                            versParam = true;
                            FreeConsole();
                            break;
                        default:
                            //Console.WriteLine("Неверный параметр");
                            //c = 0;
                            //Console.WriteLine(error.ToString());
                            break;
                    }
                }
            } else
            {
                FreeConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(param));
            }
            if (c>0 && !versParam)
            {
                //FreeConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(param));
            }
        }
    }
}
