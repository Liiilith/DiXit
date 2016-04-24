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

using System.Text.RegularExpressions;

namespace DiXit
{
    public partial class Form2 : Form
    {
        private bool isServer=false;
        private Server srv;
        private Player pl= new Player("unknown", "unknown");
        Form1 F1;
        public Form2()
        {


            InitializeComponent();
          
            textBox2.Text = Constance.GetLocalIPAddress();
            F1 = new Form1(isServer, pl, this.Location, this);
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
            if (Constance.IsIPv4(textBox2.Text))
            {
                Player p = new Player(textBox1.Text, textBox2.Text);
                pl = p;
                if (b.Tag == "SRV") isServer = true;
                if (textBox1.Text != "")
                {
                    F1.updatee(isServer, pl, this.Location, this);
                    F1.Update();
                    F1.Show();
                    this.Hide();
                    F1.Visible = true;
                    srv = new Server(pl);
                   
                }
            }
            else { textBox2.Text = "No valid IP"; }
            

        }

        public void goBack(bool srv, Player p, Point loc, Form1 f1)
        {
            isServer = srv;
            this.Show();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = loc;
            pl = p;
            F1 = F1;
            this.Visible = true;

        }
        private void button3_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
