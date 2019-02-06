using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.IO.Compression;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Security.AccessControl;

namespace Update
{
    public enum UpdateType
    {
        Application,
        Update
    }
    public class CheckAndUpdate
    {
        private string TempPath;
        private string UpdateUrl;
        private string BackupPath;
        private string UpdatePath;
        private string UpdateFileName;
        private string AppFileName;
        private UpdateType TypeOfUpdate;

        public void SetTempPath (string path)
        {
            TempPath = path;
        }
        public void SetUpdateUrl (string url)
        {
            UpdateUrl = url;
        }
        public void SetBackupPath(string backup)
        {
            BackupPath = backup;
        }
        public void SetUpdatePath(string updpath)
        {
            UpdatePath = updpath;
        }
        public void SetUpdateFileName(string updfilename)
        {
            UpdateFileName = updfilename + ".exe";
        }
        public void SetAppFileName(string appfilename)
        {
            AppFileName = appfilename + ".exe";
        }
        public void SetTypeOfUpdate (UpdateType type)
        {
            TypeOfUpdate = type;
        }

        void BeginCheckProcess(UpdateType type) {
            //System.Security.AccessControl.DirectorySecurity sec = System.IO.Directory.GetAccessControl(AppDomain.CurrentDomain.BaseDirectory);
            //FileSystemAccessRule accRule = new FileSystemAccessRule(AppDomain.CurrentDomain.BaseDirectory,
            //    FileSystemRights.FullControl, AccessControlType.Allow);
            //sec.AddAccessRule(accRule);
            if (type == UpdateType.Update)
            {
                try
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Update.exe.old"))
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Update.exe.old");
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("UnauthorizedAccessException");
                }
            }
            
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Update.exe")) //проверка апдейт.ехе в папке с приложением
            {
                MessageBox.Show("update.exe is not exist. probably application broken during installation");
            }
            if (!Directory.Exists(TempPath)) //проверка наличия папки приложения в темпе
            {
                Directory.CreateDirectory(TempPath);
                Directory.CreateDirectory(TempPath + "\\Backup");
            }
            else if (!Directory.Exists(TempPath + "\\Update"))
            {
                Directory.CreateDirectory(TempPath + "\\Update"); //и в ней создаем апдейт
            }
            if (File.Exists(TempPath + "\\settings.json"))
            {
                File.Delete(TempPath + "\\settings.json");
            }
            WebClient ClientUpdate = new WebClient();
            ClientUpdate.DownloadFile(new Uri((UpdateUrl + "settings.json").ToString()), TempPath + "\\settings.json");
        }
        void InitializeSettingsFile(UpdateType type)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Vrsn[])); //инициализирую сериализатор
            Vrsn updateProgram = new Vrsn();
            updateProgram.Upd.File = "Update.zip";
            updateProgram.App.File = "pingCheck.zip";
            if (type == UpdateType.Update)
            {
                updateProgram.App.Vers = Application.ProductVersion.ToString();
                updateProgram.Upd.Vers = AssemblyName.GetAssemblyName(AppDomain.CurrentDomain.BaseDirectory + 
                    "Update.exe").Version.ToString();
                Vrsn[] file = new Vrsn[] { updateProgram };
                using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + 
                    "UpdateSettings.json", FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, file);
                }
            }
            else if (type == UpdateType.Application)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "pingCheck.exe"))
                {
                    updateProgram.App.Vers = AssemblyName.GetAssemblyName(AppDomain.CurrentDomain.BaseDirectory +
                        "pingCheck.exe").Version.ToString();
                }
                else if (File.Exists(".\\Debug\\pingCheck.exe"))
                {
                    updateProgram.App.Vers = AssemblyName.GetAssemblyName(".\\Debug\\pingCheck.exe").Version.ToString();
                }
                else
                {
                    MessageBox.Show("ничего не найдено");
                }
                updateProgram.Upd.Vers = Application.ProductVersion.ToString();
                Vrsn[] file = new Vrsn[] { updateProgram };
                using (FileStream fs = new FileStream("UpdateSettings.json", FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, file);
                }
            }
        }
        bool VerifyProgramVersion (UpdateType type)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Vrsn[]));
            using (FileStream fs = new FileStream(TempPath + "\\settings.json", FileMode.Open))
            {
                Vrsn[] fileVersion = (Vrsn[])jsonFormatter.ReadObject(fs);
                bool ret = false;
                if (type == UpdateType.Application)
                {
                    if (Version.Parse(fileVersion[0].Upd.Vers) > Version.Parse(Application.ProductVersion) ||
                    Version.Parse(fileVersion[0].App.Vers) > AssemblyName.GetAssemblyName(".\\pingCheck.exe").Version) {
                        ret = true;
                    }
                }
                else if (type == UpdateType.Update)
                {
                    if (Version.Parse(fileVersion[0].Upd.Vers) > AssemblyName.GetAssemblyName(AppDomain.CurrentDomain.BaseDirectory +
                        "Update.exe").Version || Version.Parse(fileVersion[0].App.Vers) > Version.Parse(Application.ProductVersion)) {
                        ret = true;
                    }
                }
                return ret;
            }
        }
        void InitializeDownload (UpdateType type)
        {
            WebClient ClientUpdate = new WebClient();
            
            if (type == UpdateType.Application)
            {
                if (File.Exists(TempPath + "\\pingCheck.zip"))
                {
                    File.Delete(TempPath + "\\pingCheck.zip");
                }
                ClientUpdate.DownloadFile(new Uri(UpdateUrl + "\\pingCheck.zip"),
                TempPath + "\\pingCheck.zip");
            }
            else if (type == UpdateType.Update)
            {
                if (File.Exists(TempPath + "\\Update.zip"))
                {
                    File.Delete(TempPath + "\\Update.zip");
                }
                ClientUpdate.DownloadFile(new Uri(UpdateUrl + "\\Update.zip"),
                TempPath + "\\Update.zip");
            }
        }
        void InitializeUnzip(UpdateType type)
        {
            if (type == UpdateType.Application)
            {
                if (File.Exists(TempPath + "\\pingCheck.zip"))
                {
                    if (File.Exists(UpdatePath + "\\pingCheck.exe"))
                    {
                        File.Delete(UpdatePath + "\\pingCheck.exe");
                    }
                    ZipFile.ExtractToDirectory(TempPath + "\\pingCheck.zip", UpdatePath);
                }
            }
            else if (type == UpdateType.Update)
            {
                if (File.Exists(TempPath + "\\Update.zip"))
                {
                    if (File.Exists(UpdatePath + "\\Update.exe"))
                    {
                        File.Delete(UpdatePath + "\\Update.exe");
                    }
                    ZipFile.ExtractToDirectory(TempPath + "\\Update.zip", UpdatePath);
                }
            }
        }
        void InitializeReplase(UpdateType type)
        {
            if (type == UpdateType.Application)
            {
                if (File.Exists(BackupPath + "\\pingCheck.exe.old"))
                {
                    File.Delete(BackupPath + "\\pingCheck.exe.old");
                }
                if (Directory.Exists(BackupPath))
                {
                    File.Move(AppDomain.CurrentDomain.BaseDirectory + "pingCheck.exe", BackupPath + "\\pingCheck.exe.old");
                }
                else
                {
                    Directory.CreateDirectory(BackupPath);
                    File.Move(AppDomain.CurrentDomain.BaseDirectory + "pingCheck.exe", BackupPath + "\\pingCheck.exe.old");
                }
                File.Move(UpdatePath + "\\pingCheck.exe", AppDomain.CurrentDomain.BaseDirectory + "pingCheck.exe");
            }
            else if (type == UpdateType.Update)
            {
                if (File.Exists(BackupPath + "\\Update.exe.old"))
                {
                    File.Delete(BackupPath + "\\Update.exe.old");
                }
                if (Directory.Exists(BackupPath))
                {
                    File.Move(AppDomain.CurrentDomain.BaseDirectory + 
                        "Update.exe", AppDomain.CurrentDomain.BaseDirectory + "Update.exe.old");
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Update.exe.old"))
                    {
                        File.Move(AppDomain.CurrentDomain.BaseDirectory + "Update.exe.old", BackupPath + "\\Update.exe.old");
                        //DirectoryInfo fi = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                        //foreach (var di in fi.EnumerateFiles())
                        //    MessageBox.Show(di.Name); //эта херня находит файл
                    }
                    else MessageBox.Show("не нашелся " + AppDomain.CurrentDomain.BaseDirectory + "Update.exe.old");
                }
                else
                {
                    Directory.CreateDirectory(BackupPath);
                    File.Move(AppDomain.CurrentDomain.BaseDirectory + "Update.exe", BackupPath + "\\Update.exe.old");
                }
                File.Move(UpdatePath + "\\Update.exe", AppDomain.CurrentDomain.BaseDirectory + "Update.exe");
            }
        }
        private void InitializeClean()
        {
            if (File.Exists(TempPath + "pingCheck.zip"))
            {
                File.Delete(TempPath + "pingCheck.zip");
            }
            if (File.Exists(TempPath + "Update.zip"))
            {
                File.Delete(TempPath + "Update.zip");
            }
            if (File.Exists(TempPath + "settings.json"))
            {
                File.Delete(TempPath + "settings.json");
            }
        }
        void SwitchApp(UpdateType type)
        {
            if (type == UpdateType.Application)
            {
                MessageBox.Show("Обновление завершено");
                ProcessStartInfo SwtchApp = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "pingCheck.exe");
                SwtchApp.UseShellExecute = true;
                Process.Start(SwtchApp);
            }
            else if (type == UpdateType.Update)
            {
                if (DialogResult.Yes == MessageBox.Show("Обнаружено обновление! Загрузить?", "Обновление", MessageBoxButtons.YesNo))
                {
                    ProcessStartInfo SwtchApp = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "Update.exe");
                    SwtchApp.UseShellExecute = true;
                    Process.Start(SwtchApp); //подтверждая, запускается приложение апдейт.ехе
                }
            }
        }
        static bool IsProcessOpen(string name) //проверяет, открыт ли процесс
        {
            return Process.GetProcesses().Any(clsProcess => clsProcess.ProcessName.Contains(name));
        }
        static bool FindAndKillProcess(UpdateType type) //убивает процессы, в зависимости от запущенного приложения
        {
            bool ret = false;
            if (type == UpdateType.Application)
            {
                if (IsProcessOpen("pingCheck")) //ищу процесс пингчек и убиваю его
                {
                    foreach (Process clsProcess in Process.GetProcesses())
                    {
                        if (clsProcess.ProcessName.StartsWith("pingCheck"))
                        {
                            clsProcess.Kill();
                            ret = true;
                        }
                    }
                }
            }
            else if (type == UpdateType.Update)
            {
                if (IsProcessOpen("Update")) //ищу процесс апдейт и убиваю его
                {
                    foreach (Process clsProcess in Process.GetProcesses())
                    {
                        if (clsProcess.ProcessName.StartsWith("Update"))
                        {
                            clsProcess.Kill();
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }
        

        public void RunUpdate(UpdateType type)
        {
            BeginCheckProcess(type);
            InitializeSettingsFile(type);
            FindAndKillProcess(type);
            if (VerifyProgramVersion(type))
            {
                InitializeDownload(type);
                InitializeUnzip(type);
                InitializeReplase(type);
                InitializeClean();
                SwitchApp(type);
            }
        }
    }
}
