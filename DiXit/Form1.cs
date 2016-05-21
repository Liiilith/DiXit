using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
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
        PlayerL f3pl; 
 
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
            ss = new Server(pl);
            ss.socketSart();
            // czekamy na odbiór wyników
            msg2.Data = ss.getMSG();
            if (msg2.Data != null)
            {
                processMSG(msg2);
                // label3.Text = ppp.lista[0].playerID;
                UPD_plList(plData.getList());
                PlayerL sss = new PlayerL();
            
                    sss.lista= plData.getList();
               Message m = response(sss);
                ss.sendMSG(m);
               /* button5.Invoke(new Action(delegate ()
                {
                    button5.performClick();
                }));*/


            }
            //ss.socketClose();
        }

        public Message response(PlayerL p)
        {
            Message d = SRL.Serialize(p);

            return d;
        }

        private void clientStart()
        {
        
            ppp = new PlayerL(pl);

            for (int i = 0; i < 8; i++)
            { 
            Player p = new Player("127.0.0."+ i.ToString(), "aaa" + i.ToString());

            ppp.AddToPL(p);
               
            }
            Message msg1 = SRL.Serialize(ppp); // w msg.Data jest obiekt do wysłania 

            cc = new Client(pl);
            Message ms = new Message();
            cc.cltStart("89.70.34.25", 50201);
            ms.Data = cc.runClient(msg1.Data);
            processMSG(ms);
            //  String s= ppp2.getPlayers();
           
            UPD_plList(plData.getList());
         //   startGame();
            ms.Data=cc.checkIfGameStarted();
            PlayerL togame = SRL.takeM(ms);
            f3pl = SRL.takeM(ms);
                //
                 button5.Invoke(new Action(delegate ()
                 {
                     button5.PerformClick();
                 }));

          //  check_MSG(togame);
            /*   Thread serwerThread = new Thread(new ThreadStart(run_F3));     
               serwerThread.Start();
            //   
            /*   this.Invoke(new Action(delegate ()
               {
                   this.Hide();
               }));*/

        }
        public void run_F3()
        {
            check_MSG(f3pl);
        }

        private void processMSG(Message m)
        {
            PlayerL ppppp2 = SRL.takeM(m);
            if (ppppp2 != null)
            {
                if (ppppp2.lista != null)
                {
                    if (ppppp2.lista.Count > 0)
                    {
                        foreach (Player p in ppppp2.lista)
                        {
                           if(p.iPadd != pl.iPadd)
                            {
                                //trzeba zrobic logike dodawania
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

        public void button2_Click(object sender, EventArgs e)
        {

            if (server) { 
            PlayerL sss = new PlayerL();
            sss.type = msgType.startGame;

            Message m = response(sss);
            ss.sendMSG(m);
            }
            Form F3;
            if (server)
                F3 = new Form3(pl, this.Location, server);//, ss);
            else
            {
                F3 = new Form3(pl, this.Location, server);//, cc);
            }
                // this.Hide();
                F3.Enabled = true;
                F3.Visible = true;//
                F3.Show();
            
          //  startGame();
            //this.Hide();}
           
        }


        private void buttonsLook(string gameIP, string plID)
        {
            button2.Visible = server;
            button2.Enabled = server;
            if (server)
            {
                this.BackgroundImage = global::DiXit.Properties.Resources.aw2;

            }
            else
            {
                this.BackgroundImage = global::DiXit.Properties.Resources.aw;
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
            for (int i = 0; i < 14; i++)
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

        public void UPD_plList(List<Player> p)
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
                    if (p.Count > i)
                    {
                        button.UseVisualStyleBackColor = true;
                        button.BackColor = p[i].Color;
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void startGame()
        {
            Form F3;
            if (server)
                F3 = new Form3(pl, this.Location, server);//, ss);
            else { F3 = new Form3(pl, this.Location, server);//, cc);

            // this.Hide();
            F3.Enabled = true;
            F3.Visible = true;//
            F3.Show();
                }

        }


        public void check_MSG(PlayerL plL)
        {

            
            switch (plL.type)
            {

                case msgType.startGame:
                    //startGame();
                    button2.PerformClick();
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }


        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form F3;
            if (server)
                F3 = new Form3(pl, this.Location, server);//, ss);
            else
            {
                F3 = new Form3(pl, this.Location, server);//, cc);

                // this.Hide();
                F3.Enabled = true;
                F3.Visible = true;//
                F3.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form F3;
            if (server)
                F3 = new Form3(pl, this.Location, server);//, ss);
            else
            {
                F3 = new Form3(pl, this.Location, server);//, cc);

                // this.Hide();
                F3.Enabled = true;
                F3.Visible = true;//
                F3.Show();
            }
        }
    }
}
