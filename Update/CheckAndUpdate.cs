using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Reflection;

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
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Version[])); //инициализирую сериализатор
            Version updateProgram = new Version(new App(AssemblyName.GetAssemblyName("..\\pingCheck.exe").Version.ToString(), "pingCheck.zip"), 
                new Upd(Application.ProductVersion.ToString(), "Update.zip"));

            

            Version[] file = new Version[] { updateProgram }; //пихаю в массив
            using (FileStream fs = new FileStream("settings.json", FileMode.Create)) //запись в файл
            {
                jsonFormatter.WriteObject(fs, file);
            }
        }
    }
}
