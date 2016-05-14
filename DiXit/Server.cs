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
    class Server:Game
    {
        protected List<Thread> threadList;
        public Server(Player pl) : base(pl)
        {

        }
        public byte[] runServer()
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse(gameIP);
                ipAd = IPAddress.Parse("192.168.1.10");
               
                // use local m/c IP address, and 
                // use the same in the client

                /* Initializes the Listener */
                TcpListener myList = new TcpListener(ipAd, 21);

                /* Start Listeneting at the specified port */
                myList.Start();

                //      Console.WriteLine("The server is running at port 8001...");
                //      Console.WriteLine("The local End point is  :" +
                //                        myList.LocalEndpoint);
                //      Console.WriteLine("Waiting for a connection.....");

                Socket s = myList.AcceptSocket();
                //      Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

             

                byte[] b = new byte[65535];
                int k = s.Receive(b);
               


                ASCIIEncoding asen = new ASCIIEncoding();
               // s.Send(asen.GetBytes("The string was recieved by the server."));
                //   Console.WriteLine("\nSent Acknowledgement");
                /* clean up */
                s.Close();
                myList.Stop();

                return b;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                byte[] c = new byte[1];
                return c;

            }
        }

    }
}
