using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace pingCheck
{
    class ipValid
    {
        public static bool IsValidIP(string Address)
        {
            IPAddress ip;
            if (IPAddress.TryParse(Address, out ip))
            {
                switch (ip.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        if (Address.Length > 6 && Address.Contains("."))
                        {
                            string[] s = Address.Split('.');
                            if (s.Length == 4 && s[0].Length > 0 && s[1].Length > 0 && s[2].Length > 0 && s[3].Length > 0)
                                return true;
                        }
                        break;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        if (Address.Contains(":") && Address.Length > 15)
                            return true;
                        break;
                    default:
                        break;
                }
            }
            return false;
        }
    }
}
