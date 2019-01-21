using System.Runtime.Serialization;

namespace pingCheck
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
