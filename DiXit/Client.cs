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
        TcpClient tcpclnt = new TcpClient();



        public Client(Player pl) : base(pl)
        {
        }


        public TcpClient cltStart(string IPtoConnect, int port)
        {
            
            tcpclnt.Connect(IPtoConnect, port);
            return tcpclnt;
        }



        public void cltStop()

        { tcpclnt.Close(); }






        public byte[] runClient(byte[] data)            // do komuikacji zapytanie - odpowiedz
        {

            try
            {
                Stream stm = tcpclnt.GetStream();                  // streamer (?) który prześle dane po połączeniu              
                stm.Write(data, 0, data.Length);                   // tu już wrzucamy dane wczesniej zserializowane do buffora
                byte[] bb = new byte[65535];                        // nowa tablica do przechowania danych od serwera
                int k = stm.Read(bb, 0, 65535);                    //  zczytamy to co zostawił nam serwer w bufforze         
                if (k == 0) return null;                         //  sprawdzimy czy wogóle coś zostawił 
           
                return bb;                                     // oddamy to co odebraliśmy od serwera do serializacji (lista playerów).
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                return null;
            }
        }


        public byte[] checkIfGameStarted()            // do komuikacji sprawdzaj czy coś zostało wpisane
        {

            try
            {
                Stream stm = tcpclnt.GetStream();                  // streamer (?) który prześle dane po połączeniu              
                byte[] bb = new byte[65535];                        // nowa tablica do przechowania danych od serwera
                int k = 0;
                while (k == 0)
                {k = stm.Read(bb, 0, 65535); }                    //  zczytamy to co zostawił nam serwer w bufforze        
                return bb;

            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                return null;
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
