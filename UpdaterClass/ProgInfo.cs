using System;
using System.Runtime.Serialization;


namespace UpdaterClass
{
    [Serializable]
    public class ProgInfo
    {        
        public string SkippedVersion { get; set; }
        public ProgInfo(string verskip)
        {
            SkippedVersion = verskip;
        }
        public ProgInfo() { }
    }
}
