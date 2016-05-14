using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace DiXit
{
    class Client:Game
    {
        //private string ownIP;


     public Client(Player pl) : base(pl)
        {
        }

        public void runClient(byte[] data)
        {

            try
            {
               // IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.1.10"), 21);
               TcpClient tcpclnt = new TcpClient();
             
               tcpclnt.Connect("89.70.34.25", 50201);
             //  tcpclnt.Connect("192.168.1.10", 21); // tutaj się łaczymy z serwerem na odpowiednim porcie i z IP            
                Stream stm = tcpclnt.GetStream();                  // streamer (?) który prześle dane po połączeniu
              
                stm.Write(data, 0, data.Length);                   // tu już wrzucamy dane wczesniej zserializowane do buffora
             

                tcpclnt.Close();

            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

        public string GetLocalIPAddress()
        {

            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("Local IP Address Not Found!");
            }
            else return "No connection";
        }

    }
}
