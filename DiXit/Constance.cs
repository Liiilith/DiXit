using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;

namespace DiXit
{
    public static class Constance
    {
        public const int formSeixeX = 500; //nie działa przy wrzucaniu do form.seize
        public const int formSeixeY = 700;
        public const string gameName = "DiXiT";
        //  System.Drawing.Size butttonSeize = new System.Drawing.Size(420, 60);
        public static bool IsIPv4(string value)
        {
            IPAddress address;

            if (IPAddress.TryParse(value, out address))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
