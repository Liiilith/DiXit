using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace DiXit
{
  public class Server:Game                       // tutaj zmienilem na public - etomryc
    {
        protected List<Thread> threadList;
        TcpListener myList;
        IPAddress ipAd;
        Socket s;
        public Server(Player pl) : base(pl)
        {
           // ipAd = IPAddress.Parse(gameIP);
            ipAd = IPAddress.Parse("192.168.1.10");
            
            myList = new TcpListener(ipAd, 21);
            
        }
        public byte[] getMSG()
        {
            try
            {
                //IPAddress ipAd = IPAddress.Parse(gameIP);
                // ipAd = IPAddress.Parse("192.168.1.10");
                // Message d = new Message();
                // use local m/c IP address, and 
                // use the same in the client

                /* Initializes the Listener */
                Message d = new Message();
               
             



                byte[] b = new byte[65535];
                int k = s.Receive(b);

                if (k == 0) return null;

              
                
                return b;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                byte[] c = new byte[1];
                return c;

            }
        }

        public bool sendMSG(Message d)
        {
            try
            {
               

               s.Send(d.Data);
                return true;
               

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
               
                return false;

            }
        }


        public bool sendMSG_single(Message d)
        {
            try
            {


                s.Send(d.Data);
                return true;


            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);

                return false;

            }
        }

        public void srvClose()
        {

            myList.Stop();
        }

        public void socketClose()
        {

            s.Close();
        }
        public void socketSart()
        {
            myList.Start();
           s = myList.AcceptSocket();
            
        }

    }
}
