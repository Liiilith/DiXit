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
        Form7 F7;
        Player pl;
        PlayerL ppp;
        PlayerL pppp2;
        playersData plData;
        PlayerL f3pl;
        System.Drawing.Color kolor;
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
        public void setColor(System.Drawing.Color k)//ustaw kolor dla gracza
        {
            kolor = k;
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
                PlayerL ppppp2 = SRL.takeM(msg2);
                MSGAddPlayers(ppppp2);
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
/*
            msg2.Data = ss.getMSG();
            if (msg2.Data != null)
            {
                PlayerL ppppp2 = SRL.takeM(msg2);
                check_MSG(ppppp2);
            }*/
            //ss.socketClose();
        }

        public Message response(PlayerL p)//serializuje dane do wysłania
        {
            Message d = SRL.Serialize(p);

            return d;
        }
        public bool srv_Check()
        {
            return server;
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
            PlayerL ppppp2 = SRL.takeM(ms);
            MSGAddPlayers(ppppp2);
            //  String s= ppp2.getPlayers();

            UPD_plList(plData.getList());
         //   startGame();
            ms.Data=cc.checkIfGameStarted();
            PlayerL togame = SRL.takeM(ms);
            check_MSG(togame);


        }


       
        private void MSGAddPlayers(PlayerL ppppp2)//updatuje playersData o otrzymaną listę graczy
        {
             
            if (ppppp2 != null)
            {
                if (ppppp2.lista != null)
                {
                    if (ppppp2.lista.Count > 0)
                    {
                        foreach (Player p in ppppp2.lista)
                        {
                            if (p.iPadd != pl.iPadd)
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


       

        private void MSGUpdPlayers(PlayerL ppppp2)//updatuje playersData o otrzymaną listę graczy
        {
            
            if (ppppp2 != null)
            {
                if (ppppp2.lista != null)
                {
                    if (ppppp2.lista.Count > 0)
                    {
                        foreach (Player p in ppppp2.lista)
                        {
                            plData.UpdatePlayerID(p);
                        }

                    }
                }
            }
        }

        public void serverSet(bool set)
        {
            server = set;
        }

        public bool updOwnColor()
        {
            Player p = plData.getPlayerByLogin(pl.playerID);
            // Player p = plData.getPlayerByIp(pl.playerID);
            bool res = false;
            if (plData.checkColor(kolor))
            {
                pl.rabbitColor = kolor;
               
              //  plData.UpdatePlayerID(pl);
                plData.Change_Color(pl, kolor);
                res = true;
                
            }
            return res;

        }

        public void SendColorUpd()
        {
            Thread clientThread = new Thread(new ThreadStart(SENDcolor));     // wyrzucamy serwer do innego wątku 
            clientThread.Start();
        }

       
        public void SENDcolor()
        {
            if (!server)
            {
                PlayerL sss = new PlayerL(pl);

                sss.type = msgType.colorUpd;
                Message m = response(sss);
                Message ms = new Message();
                ms.Data = cc.runClient(m.Data);

            }
        }

        public void SendColorRes(msgType cres)//zwrotka na zmianę koloru
        {
            if (server)
            {
                PlayerL sss = new PlayerL();
                sss.Lista = plData.getList();
                sss.type = cres;
                Message m = response(sss);
                Message ms = new Message();
                ss.sendMSG(m);
            }
        }


        public void updButtonColor()
        {
            button1.Invoke(new Action(delegate ()
            {
                button1.BackColor = kolor;
                button1.Text = "";
            }));
        }
        public void updButtonColorWRONG()
        {
            kolor = System.Drawing.Color.LemonChiffon;
           
            
            button1.Invoke(new Action(delegate ()
            {
                button1.BackColor = kolor;
                button1.Text = "X";
            }));
        }

        private void bcolor_Click(object sender, EventArgs e)//wybór koloru
        {
            List<System.Drawing.Color> col = plData.getColors();
            F7 = new Form7(col,this);
            F7.Show();
            
                
        }

        public void UPD_players_F7()
        {
            UPD_plList(plData.getList());
        }

        public void button2_Click(object sender, EventArgs e)//button START
        {

            PlayerL p = new PlayerL();
            p.lista = plData.getList();
            if (server) { 
            PlayerL sss = new PlayerL();
            sss.type = msgType.startGame;

            Message m = response(sss);
            ss.sendMSG(m);
            }
            Form F3;
            this.Hide();
            if (server)
                
                F3 = new Form3(pl, this.Location, server, ss,p);
            else
            
                F3 = new Form3(pl, this.Location, server, cc);
            
               
                F3.Enabled = true;
                F3.Visible = true;
                F3.Show();
            
           
        }


        private void buttonsLook(string gameIP, string plID)//update formy
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
            button5.Hide();
        }
       
        public void updatePlayerList2(List<Player> p)//update listy graczy
        {
           
            
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

        public void UPD_plList(List<Player> p)//update listy graczy z innego wątku
        {
            panel1.Invoke(new Action(delegate ()
            {

                foreach (Control item in panel1.Controls.OfType<Button>())
                {
                    panel1.Controls.Remove(item);
                }
                foreach (Control item in panel1.Controls.OfType<Label>())
                {
                    panel1.Controls.Remove(item);
                }

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

        public void updatee(bool srv, Player p, Point loc, Form2 f1)//update pozycji
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
            PlayerL p = new PlayerL();
            p.lista = plData.getList();
            Form F3;
            if (server)
                F3 = new Form3(pl, this.Location, server, ss,p);
            else { F3 = new Form3(pl, this.Location, server, cc);

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
                    button5.Invoke(new Action(delegate ()
                    {
                        button5.Show();
                        button5.PerformClick();
                        button5.Hide();
                    }));


                    this.Invoke(new Action(delegate ()
                    {
                        this.Hide();
                    }));
                    break;

                case msgType.colorUpd:
                    if (plData.checkColor(plL.Lista[0].Color)){
                        MSGUpdPlayers(plL);
                        UPD_plList(plData.getList());
                        SendColorRes(msgType.okColor);
                    }
                    else
                    {
                        SendColorRes(msgType.wrongColor);
                    }
                    
                    break;

                case msgType.wrongColor:
                    
                    updButtonColorWRONG();
                    MSGUpdPlayers(plL);

                    break;

                case msgType.okColor:

                    updOwnColor();
                    updButtonColor();
                    MSGUpdPlayers(plL);

                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }


        }




        public bool check_Start(PlayerL plL)
        {


            if (plL.type == msgType.startGame)
                return true;
            else return false;
                   
            


        }


        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)//START GRY
        {
            PlayerL p = new PlayerL();
            p.lista = plData.getList();
            Form F3;
            if (server)
                F3 = new Form3(pl, this.Location, server, ss, p);
            else
            {
                F3 = new Form3(pl, this.Location, server, cc);
            }
                 this.Hide();
                F3.Enabled = true;
                F3.Visible = true;//
                F3.Show();
            
        }
    }
}
