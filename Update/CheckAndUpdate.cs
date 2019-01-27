using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.IO.Compression;

namespace Update
{
    public class CheckAndUpdate
    {
        public void BeginCheckProcess() {
            if (!File.Exists("Update.exe")) //проверка апдейт.ехе в папке с приложением
            {
                MessageBox.Show("update.exe is not exist");
            }
            if (!Directory.Exists(Path.GetTempPath() + "PingCheck")) //проверка наличия папки приложения в темпе
            {
                Directory.CreateDirectory(Path.GetTempPath() + "PingCheck\\Update"); //и в ней создаем апдейт
            }
            if (!File.Exists(Path.GetTempPath() + "PingCheck\\settings.json"))
            {
                WebClient ClientUpdate = new WebClient();
                ClientUpdate.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/mynameisfoxy/PingCheckUpdate/master/settings.json"), Path.GetTempPath() + "PingCheck\\settings.json");
            }
        }
        public void InitializeSettingsFile ()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Vrsn[])); //инициализирую сериализатор
            Vrsn updateProgram = new Vrsn(new App(AssemblyName.GetAssemblyName("..\\pingCheck.exe").Version.ToString(), "pingCheck.zip"), 
                new Upd(Application.ProductVersion.ToString(), "Update.zip")); //инициализирую версии входными параметрами

            Vrsn[] file = new Vrsn[] { updateProgram };
            using (FileStream fs = new FileStream("settings.json", FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, file);
            }
        }
        public bool VerifyProgramVersion ()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Vrsn[]));
            using (FileStream fs = new FileStream(Path.GetTempPath() + "PingCheck\\settings.json", FileMode.Open))
            {
                Vrsn[] fileVersion = (Vrsn[])jsonFormatter.ReadObject(fs);
                
                if (Version.Parse(fileVersion[0].Upd.Vers) > Version.Parse(Application.ProductVersion) && 
                    Version.Parse(fileVersion[0].App.Vers) > AssemblyName.GetAssemblyName("..\\pingCheck.exe").Version)
                    return true;
                else
                    return false;
            }
        }
        public void InitializeDownload ()
        {
            if (VerifyProgramVersion()) //если венуло труЪ, то качаем в темп
            {
                WebClient ClientUpdate = new WebClient();
                ClientUpdate.DownloadFile(new Uri("https://raw.githubusercontent.com/mynameisfoxy/PingCheckUpdate/master/pingCheck.zip"), 
                    Path.GetTempPath() + "PingCheck\\pingCheck.zip");
                ClientUpdate.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/mynameisfoxy/PingCheckUpdate/master/Update.zip"), 
                    Path.GetTempPath() + "PingCheck\\Update.zip");
            }
        }
        public void InitializeUnzip()
        {
            ZipFile.ExtractToDirectory(Path.GetTempPath() + "PingCheck\\pingCheck.zip", Path.GetTempPath() + "PingCheck\\Update");
            ZipFile.ExtractToDirectory(Path.GetTempPath() + "PingCheck\\Update.zip", Path.GetTempPath() + "PingCheck\\Update");
        }
    }
}
