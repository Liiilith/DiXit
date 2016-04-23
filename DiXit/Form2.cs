using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiXit
{
    public partial class Form2 : Form
    {
        private bool isServer=false;
        private Server srv;
        public Form2()
        {


            InitializeComponent();
          
            textBox2.Text = GetLocalIPAddress();
        }


        public static string GetLocalIPAddress()
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

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void runForm1(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Player player1 = new Player(textBox1.Text, textBox2.Text);
            if (b.Tag == "SRV") isServer = true;
            if (textBox1.Text != "")
            {
                Form1 F1 = new Form1(player1.PlayerID, player1.getIpAddress(), isServer);
                F1.Show();
                this.Hide();
                F1.Visible = true;
                srv = new Server(player1);
               // Application.Run(new Form1(textBox2.Text, textBox1.Text, isServer)); 
            }
           

        }
    }
}
