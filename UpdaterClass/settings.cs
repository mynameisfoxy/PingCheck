using System.Runtime.Serialization;

namespace UpdaterClass
{
    [DataContract]
    public class settings
    {
        [DataMember]
        public string site;
        [DataMember]
        public int timer;
    }
}
