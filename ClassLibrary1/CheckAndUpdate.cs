using System;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.IO.Compression;

namespace pingCheck
{
    public enum UpdateType
    {
        Application,
        Update
    }
    public class CheckAndUpdate
    {
        private bool UpdateExist = false;
        private string TempPath;
        private string UpdateUrl;
        private string BackupPath;
        private string UpdatePath;
        private string AppFileName;
        private string AppOldName;
        private string AppZipName;
        private string AppZipInTempFolder;
        private string AppInCurrentFolder;
        private string AppOldInBackup;
        private string AppInUpdateFolder;
        private string AppOldInCurrentFolder;
        private string AppProcessName;
        private string UpdateFileName;
        private string UpdateOldName;
        private string UpdateZipName;
        private string UpdZipInTempFolder;
        private string UpdateInCurrentFolder;
        private string UpdateOldInBackup;
        private string UpdInUpdateFolder;
        private string UpdateOldInCurrentFolder;
        private string UpdateProcassName;
        private string MyDocumentsFolder;
        private string SettingsXmlFromUsersFolder;
        private UpdateType TypeOfUpdate;
        XmlSerializer XmlSetts = new XmlSerializer(typeof(ProgInfo[]));



        public void SetTempPath(string path)
        {
            TempPath = path + "\\";
            if (BackupPath == null)
            {
                BackupPath = TempPath + "Backup\\";
            }
            if (UpdatePath == null)
            {
                UpdatePath = TempPath + "Update\\";
            }
        }
        public bool IfUpdateExist()
        {
            return UpdateExist;
        }
        public void SetUpdateUrl(string url)
        {
            UpdateUrl = url;
        }
        public void SetBackupPath(string backup)
        {
            BackupPath = backup;
            if (UpdateOldName != null)
            {
                UpdateOldInBackup = backup + UpdateOldName;
            }
            if (AppOldName != null)
            {
                AppOldInBackup = backup + AppOldName;
            }
        }
        public void SetUpdatePath(string updpath)
        {
            UpdatePath = updpath;
        }
        public void SetUpdateFileName(string updfilename)
        {
            UpdateProcassName = updfilename;
            UpdateFileName = updfilename + ".exe";
            UpdateOldName = updfilename + ".old";
            UpdateZipName = updfilename + ".zip";
            UpdateInCurrentFolder = AppDomain.CurrentDomain.BaseDirectory + UpdateFileName;
            
            if (BackupPath!=null)
            {
                UpdateOldInBackup = BackupPath + UpdateOldName;
            }
            if (UpdatePath != null)
            {
                UpdInUpdateFolder = UpdatePath + UpdateFileName;
            }
            if (TempPath != null)
            {
                UpdZipInTempFolder = TempPath + UpdateZipName;
            }
        }
        public void SetAppFileName(string appfilename)
        {
            AppProcessName = appfilename;
            AppFileName = appfilename + ".exe";
            AppOldName = appfilename + ".old";
            AppZipName = appfilename + ".zip";
            AppInCurrentFolder = AppDomain.CurrentDomain.BaseDirectory + AppFileName;
            AppOldInCurrentFolder = AppDomain.CurrentDomain.BaseDirectory + AppOldName;
            if (BackupPath != null)
            {
                AppOldInBackup = BackupPath + AppOldName;
            }
            if (UpdatePath != null)
            {
                AppInUpdateFolder = UpdatePath + AppFileName;
            }
            if (TempPath != null)
            {
                AppZipInTempFolder = TempPath + AppZipName;
            }
        }
        public void SetTypeOfUpdate(UpdateType type)
        {
            TypeOfUpdate = type;
        }

        void BeginCheckProcess(UpdateType type, string SettingsFromTempFolder, Uri SettingsFromUri)
        {
            if (type == UpdateType.Update)
            {
                if (File.Exists(AppOldInCurrentFolder))
                {
                    File.Delete(AppOldInCurrentFolder);
                }
            }
            if (!File.Exists(UpdateInCurrentFolder)) //проверка апдейт.ехе в папке с приложением
            {
                MessageBox.Show(UpdateInCurrentFolder +" is not exist. Probably application broken during installation.");
            }
            if (!Directory.Exists(TempPath)) //проверка наличия папки приложения в темпе
            {
                Directory.CreateDirectory(TempPath);
                Directory.CreateDirectory(BackupPath);
            }
            else if (!Directory.Exists(UpdatePath))
            {
                Directory.CreateDirectory(UpdatePath); //и в ней создаем апдейт
            }
            if (File.Exists(SettingsFromTempFolder))
            {
                File.Delete(SettingsFromTempFolder);
            }
            WebClient ClientUpdate = new WebClient();
            ClientUpdate.DownloadFile(SettingsFromUri, SettingsFromTempFolder);
        }
        void InitializeSettingsFile(UpdateType type, string SettingsInAppFolder)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Vrsn[])); //инициализирую сериализатор
            Vrsn updateProgram = new Vrsn();
            updateProgram.Upd.File = UpdateZipName;
            updateProgram.App.File = AppZipName;
            if (!File.Exists(SettingsXmlFromUsersFolder))
            {
                if (!File.Exists(MyDocumentsFolder))
                {
                    Directory.CreateDirectory(MyDocumentsFolder);
                }
                SetThisVersionSkipped();
            }
            if (type == UpdateType.Update)
            {
                updateProgram.App.Vers = Application.ProductVersion.ToString();
                updateProgram.Upd.Vers = AssemblyName.GetAssemblyName(UpdateInCurrentFolder).Version.ToString();
                Vrsn[] file = new Vrsn[] { updateProgram };
                using (FileStream fs = new FileStream(SettingsInAppFolder, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, file);
                }
            }
            else if (type == UpdateType.Application)
            {
                if (File.Exists(AppInCurrentFolder))
                {
                    updateProgram.App.Vers = AssemblyName.GetAssemblyName(AppInCurrentFolder).Version.ToString();
                }
                else
                {
                    MessageBox.Show("ничего не найдено");
                }
                updateProgram.Upd.Vers = Application.ProductVersion.ToString();
                Vrsn[] file = new Vrsn[] { updateProgram };
                using (FileStream fs = new FileStream(SettingsInAppFolder, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, file);
                }
            }
        }
        bool VerifyProgramVersion(UpdateType type, string SettingsFromTempFolder)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Vrsn[]));
            using (FileStream fs = new FileStream(SettingsFromTempFolder, FileMode.Open))
            {
                Vrsn[] fileVersion = (Vrsn[])jsonFormatter.ReadObject(fs);
                bool ret = false;
                if (type == UpdateType.Application)
                {
                    if (Version.Parse(fileVersion[0].Upd.Vers) > Version.Parse(Application.ProductVersion) ||
                    Version.Parse(fileVersion[0].App.Vers) > AssemblyName.GetAssemblyName(AppInCurrentFolder).Version) {
                        ret = true;
                    }
                }
                else if (type == UpdateType.Update)
                {
                    if (Version.Parse(fileVersion[0].Upd.Vers) > AssemblyName.GetAssemblyName(UpdateInCurrentFolder).Version || 
                        Version.Parse(fileVersion[0].App.Vers) > Version.Parse(Application.ProductVersion)) {
                        ret = true;
                    }
                }
                UpdateExist = ret;
                return ret;
            }
        }
        void InitializeDownload(UpdateType type, Uri AppZipFromUri, Uri UpdateZipFromUri)
        {
            WebClient ClientUpdate = new WebClient();
            
            if (type == UpdateType.Application)
            {
                if (File.Exists(AppZipInTempFolder))
                {
                    File.Delete(AppZipInTempFolder);
                }
                ClientUpdate.DownloadFile(AppZipFromUri, AppZipInTempFolder);
            }
            else if (type == UpdateType.Update)
            {
                if (File.Exists(UpdZipInTempFolder))
                {
                    File.Delete(UpdZipInTempFolder);
                }
                ClientUpdate.DownloadFile(UpdateZipFromUri, UpdZipInTempFolder);
            }
        }
        void InitializeUnzip(UpdateType type)
        {
            if (type == UpdateType.Application)
            {
                if (File.Exists(AppZipInTempFolder))
                {
                    if (File.Exists(AppInUpdateFolder))
                    {
                        File.Delete(AppInUpdateFolder);
                    }
                    ZipFile.ExtractToDirectory(AppZipInTempFolder, UpdatePath);
                }
            }
            else if (type == UpdateType.Update)
            {
                if (File.Exists(UpdZipInTempFolder))
                {
                    if (File.Exists(UpdInUpdateFolder))
                    {
                        File.Delete(UpdInUpdateFolder);
                    }
                    ZipFile.ExtractToDirectory(UpdZipInTempFolder, UpdatePath);
                }
            }
        }
        void InitializeReplase(UpdateType type)
        {
            if (type == UpdateType.Application)
            {
                if (File.Exists(AppOldInBackup))
                {
                    File.Delete(AppOldInBackup);
                }
                if (Directory.Exists(BackupPath))
                {
                    if (File.Exists(AppOldInCurrentFolder))
                    {
                        File.Delete(AppOldInCurrentFolder);
                    }
                    File.Move(AppInCurrentFolder, AppOldInCurrentFolder);
                    if (File.Exists(AppOldInCurrentFolder))
                    {
                        File.Move(AppOldInCurrentFolder, AppOldInBackup);
                    }
                    else MessageBox.Show("не нашелся " + AppOldInCurrentFolder);
                }
                else
                {
                    Directory.CreateDirectory(BackupPath);
                    File.Move(AppInCurrentFolder, AppOldInBackup);
                }
                File.Move(AppInUpdateFolder, AppInCurrentFolder);
            }
            else if (type == UpdateType.Update)
            {
                if (File.Exists(UpdateOldInBackup))
                {
                    File.Delete(UpdateOldInBackup);
                }
                if (Directory.Exists(BackupPath))
                {
                    File.Move(UpdateInCurrentFolder, UpdateOldInBackup);
                }
                else
                {
                    Directory.CreateDirectory(BackupPath);
                    File.Move(UpdateInCurrentFolder, UpdateOldInBackup);
                }
                File.Move(UpdInUpdateFolder, UpdateInCurrentFolder);
            }
        }
        private void InitializeClean(string SettingsFromTempFolder)
        {
            if (File.Exists(AppZipInTempFolder))
            {
                File.Delete(AppZipInTempFolder);
            }
            if (File.Exists(UpdZipInTempFolder))
            {
                File.Delete(UpdZipInTempFolder);
            }
            if (File.Exists(SettingsFromTempFolder))
            {
                File.Delete(SettingsFromTempFolder);
            }
        }
        void SwitchApp(UpdateType type)
        {
            if (type == UpdateType.Application)
            {
                MessageBox.Show("Обновление завершено");
                ProcessStartInfo SwtchApp = new ProcessStartInfo(AppInCurrentFolder);
                SwtchApp.UseShellExecute = true;
                Process.Start(SwtchApp);
            }
            else if (type == UpdateType.Update)
            {
                ProcessStartInfo SwtchApp = new ProcessStartInfo(UpdateInCurrentFolder);
                SwtchApp.UseShellExecute = true;
                Process.Start(SwtchApp); //подтверждая, запускается приложение апдейт.ехе
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
            string SettingsInAppFolder = AppDomain.CurrentDomain.BaseDirectory + "UpdateSettings.json";
            string SettingsFromTempFolder = TempPath + "settings.json";
            MyDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\PingCheck\\";
            SettingsXmlFromUsersFolder = MyDocumentsFolder + "Settings.xml";

            Uri SettingsFromUri = new Uri(UpdateUrl + "settings.json");
            Uri UpdateZipFromUri = new Uri(UpdateUrl + "\\" + UpdateZipName);
            Uri AppZipFromUri = new Uri(UpdateUrl + "\\" + AppZipName);

            BeginCheckProcess(type, SettingsFromTempFolder, SettingsFromUri);
            InitializeSettingsFile(type, SettingsInAppFolder);
            FindAndKillProcess(type);
            if (VerifyProgramVersion(type, SettingsFromTempFolder))
            {
                InitializeDownload(type, AppZipFromUri, UpdateZipFromUri);
                InitializeUnzip(type);
                InitializeReplase(type);
                InitializeClean(SettingsFromTempFolder);
                if (type == UpdateType.Application)
                {
                    SwitchApp(type);
                }
            }
        }

        public void Restart()
        {
            SwitchApp(UpdateType.Update);
        }

        public void SetThisVersionSkipped()
        {
            ProgInfo ProgramInfo = new ProgInfo();
            ProgramInfo.SkippedVersion = AssemblyName.GetAssemblyName(UpdateInCurrentFolder).Version.ToString();
            ProgInfo[] file = new ProgInfo[] { ProgramInfo };
            using (FileStream fs = new FileStream(SettingsXmlFromUsersFolder, FileMode.Create))
            {
                XmlSetts.Serialize(fs, file);
            }
        }

        public bool IsThisVersionSkipped()
        {
            bool ret = false;
            ProgInfo[] file;
            using (FileStream fs = new FileStream(SettingsXmlFromUsersFolder, FileMode.Open))
            {
                file = (ProgInfo[])XmlSetts.Deserialize(fs);
            }
            if (new Version(file[0].SkippedVersion) > new Version(Application.ProductVersion))
            {
                ret = true;
            }

            return ret;
        }
    }
}
