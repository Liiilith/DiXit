using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DiXit
{
    public partial class Form1 : Form
    {
        //bedzie już istnieć
        Server ss;
        Client cc;
        bool server = false;
        Form2 F2;
        Player pl;
        PlayerL ppp;
        PlayerL pppp2;
        playersData plData;
        Message msg;
        // Player player1 = new Player("22", "gracz1");       
        //  Player player2 = new Player("22", "gracz2");   



        public Form1(bool srv, Player p,Point loc, Form2 f2)
        {
            server = srv;
           
            InitializeComponent();
            plData = new playersData();
           
            F2 = f2;
            buttonsLook(p.getIpAddress(), p.PlayerID);
            this.Show();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = loc;
            pl = p;
            plData.AddToPlayerList(p);
           
            if (server)
            {
               
                Thread serwerThread = new Thread(new ThreadStart(serverStart));     // wyrzucamy serwer do innego wątku 
                serwerThread.Start();
            }
            else
            {
                Thread clientThread = new Thread(new ThreadStart(clientStart));     // wyrzucamy serwer do innego wątku 
                clientThread.Start();

            }
        }
        ////test połączenia
       private void serverStart()
        {
            Message msg2 = new Message();
            ss = new Server(pl);                 // czekamy na odbiór wyników
            msg2.Data = ss.runServer();
            if (msg2 != null)
            {
                processMSG();
                // label3.Text = ppp.lista[0].playerID;
                UPD_srv(pppp2);
            }
        }

        private void clientStart()
        {
            ppp = new PlayerL(pl);
            ppp.AddToPL(pl);
             Message msg1 = SRL.doM(ppp); // w msg.Data jest obiekt do wysłania 

            cc = new Client(pl);
            cc.runClient(msg1.Data);
        //   PlayerL ppp2 = SRL.takeM(msg);
            //  String s= ppp2.getPlayers();
            // label3.Text = s;
            // updatePlayerList2(ppp.lista);
            // label3.Invoke(new Action(delegate () { label3.Text = ppp2.getPlayers(); }));
          //  UPD_srv(ppp2);
            }

        private void processMSG()
        {
            pppp2 = SRL.takeM(msg);
            if (pppp2 != null)
            {
                if (pppp2.lista != null)
                {
                    if (pppp2.lista.Count > 0)
                    {
                        foreach (Player p in pppp2.lista)
                        {
                           if(p.iPadd != pl.iPadd)
                            {
                                plData.AddToPlayerList(p);
                            }
                        }
                     //   updatePlayerList2(ppp.lista);
                    }
                }
            }
        }

   

     

        public void serverSet(bool set)
        {
            server = set;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Button bt = sender as Button;
            System.Drawing.Color c = new System.Drawing.Color();
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {   //trzeba bedzie sprawdzic jakos czy kolor jest dostepny
                //while(! gra.Check_rabbit( c)){//potrzebna jest klasa nadrzedna monitorująca całość
                // result = colorDialog1.ShowDialog();
                // if (result == DialogResult.OK)
                //{

                bt.BackColor = colorDialog1.Color;
                pl.Color = colorDialog1.Color;
                // }

            }

            //  bt.BackColor = c;
            // player1.Color = c;

           // updatePlayerList();
            //test połączenia
       /*     if (!server)
            {
                Player pl = new Player("22", "gracz2");
                Client client = new Client(pl);
                client.runClient(textBox1.Text);
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 F3 = new Form3(pl, this.Location);

            F3.Show();
            this.Hide();
            F3.Visible = true;//
            
        }

        private void buttonsLook(string gameIP, string plID)
        {
            button2.Visible = server;
            button2.Enabled = server;
            if (server)
            {
                this.BackgroundImage = global::DiXit.Properties.Resources.globe;
            }
            button2.Text = "START";
            label1.Text = gameIP;
            label2.Text = plID;
        }
        public void updatePlayerList()// List<Player> players) vs nie przyjmuje listy jako arg przez ograniczenia dostepu (?)
        {
            Player player1 = new Player("22", "gracz");
            /* Player player2 = new Player("22", "gracz2");
             player2.Color = System.Drawing.Color.Black;
             List<Player> tempeGracze = new List<Player>();
             tempeGracze.Add(player1);
             tempeGracze.Add(player2);
             tempeGracze.Remove(player1);

             foreach (Player p in tempeGracze)
             {
                 //wyswietlanie listy graczy
             }*/
            //tymczasowe wyswietlanie
            for (int i = 0; i < 12; i++)
            {
                int posy = (i / 2) * 40;
                int posx = (i % 2) * 250;
                System.Windows.Forms.Button button;
                System.Windows.Forms.Label label;
                button = new System.Windows.Forms.Button();
                label = new System.Windows.Forms.Label();

                button.Location = new System.Drawing.Point(160 + posx, 10 + posy);
                button.Name = "button";
                button.Size = new System.Drawing.Size(30, 30);

                button.UseVisualStyleBackColor = true;
                button.BackColor = System.Drawing.Color.IndianRed;
                button.Text = "";


                label.AutoSize = true;
                label.BackColor = System.Drawing.Color.Transparent;
                label.Cursor = System.Windows.Forms.Cursors.AppStarting;
                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                label.Location = new System.Drawing.Point(10 + posx, 10 + posy);
                label.Name = "label";
                label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                label.RightToLeft = System.Windows.Forms.RightToLeft.No;
                label.Size = new System.Drawing.Size(100, 25);
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                label.Text = player1.PlayerID;
                panel1.Controls.Add(label);
                panel1.Controls.Add(button);
            }
        }
        public void updatePlayerList2(List<Player> p)// List<Player> players) vs nie przyjmuje listy jako arg przez ograniczenia dostepu (?)
        {
           
            /* Player player2 = new Player("22", "gracz2");
             player2.Color = System.Drawing.Color.Black;
             List<Player> tempeGracze = new List<Player>();
             tempeGracze.Add(player1);
             tempeGracze.Add(player2);
             tempeGracze.Remove(player1);

             foreach (Player p in tempeGracze)
             {
                 //wyswietlanie listy graczy
             }*/
            //tymczasowe wyswietlanie
            for (int i = 0; i < 12; i++)
            {
                int posy = (i / 2) * 40;
                int posx = (i % 2) * 250;
                System.Windows.Forms.Button button;
                System.Windows.Forms.Label label;
                button = new System.Windows.Forms.Button();
                label = new System.Windows.Forms.Label();

                button.Location = new System.Drawing.Point(160 + posx, 10 + posy);
                button.Name = "button";
                button.Size = new System.Drawing.Size(30, 30);
                if (p.Count > i)
                {
                    button.UseVisualStyleBackColor = true;
                    button.BackColor = System.Drawing.Color.IndianRed;
                    button.Text = "";


                    label.AutoSize = true;
                    label.BackColor = System.Drawing.Color.Transparent;
                    label.Cursor = System.Windows.Forms.Cursors.AppStarting;
                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                    label.Location = new System.Drawing.Point(10 + posx, 10 + posy);
                    label.Name = "label";
                    label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                    label.RightToLeft = System.Windows.Forms.RightToLeft.No;
                    label.Size = new System.Drawing.Size(100, 25);
                    label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    label.Text = p[i].PlayerID;
                    panel1.Controls.Add(label);
                    panel1.Controls.Add(button);
                }
            }



        }

        public void UPD_srv(PlayerL ppp2)
        {
            panel1.Invoke(new Action(delegate ()
            {
                for (int i = 0; i < 12; i++)
                {
                    int posy = (i / 2) * 40;
                    int posx = (i % 2) * 250;
                    System.Windows.Forms.Button button;
                    System.Windows.Forms.Label label;
                    button = new System.Windows.Forms.Button();
                    label = new System.Windows.Forms.Label();

                    button.Location = new System.Drawing.Point(160 + posx, 10 + posy);
                    button.Name = "button";
                    button.Size = new System.Drawing.Size(30, 30);
                    if (ppp.lista.Count > i)
                    {
                        button.UseVisualStyleBackColor = true;
                        button.BackColor = System.Drawing.Color.IndianRed;
                        button.Text = "";


                        label.AutoSize = true;
                        label.BackColor = System.Drawing.Color.Transparent;
                        label.Cursor = System.Windows.Forms.Cursors.AppStarting;
                        label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                        label.Location = new System.Drawing.Point(10 + posx, 10 + posy);
                        label.Name = "label";
                        label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                        label.RightToLeft = System.Windows.Forms.RightToLeft.No;
                        label.Size = new System.Drawing.Size(100, 25);
                        label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        label.Text = ppp2.lista[i].PlayerID;
                        panel1.Controls.Add(label);
                        panel1.Controls.Add(button);
                    }
                }
            }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
             // przed zamknięciem trzeba ubić wątki
            this.Close();
            F2.Close();
        }

        public void updatee(bool srv, Player p, Point loc, Form2 f1)
        {
            server = srv;
            //this.Show();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = loc;
            pl = p;
            buttonsLook(p.getIpAddress(), p.PlayerID);
            F2 = f1;
            this.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //F2.goBack(server, pl, this.Location, this);
            //trzeba ubic server
        }
    }
}
