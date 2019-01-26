using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.IO;


namespace Update
{
    [DataContract]
    public class Version
    {

        [DataMember]
        public App App { get; set; }
        [DataMember]
        public Upd Upd { get; set; }

        public Version (App mainApp, Upd updPrg)
        {
            App = mainApp;
            Upd = updPrg;
        }
    }
    
    public class App {
        public string Vers { get; set; }
        public string File { get; set; }

        public App()
        {

        }
        public App(string vrs, string file)
        {
            Vers = vrs;
            File = file;
        }
    }
    public class Upd
    {
        public string Vers { get; set; }
        public string File { get; set; }

        public Upd ()
        {

        }
        public Upd (string vrs, string file)
        {
            Vers = vrs;
            File = file;
        }
    }
}
