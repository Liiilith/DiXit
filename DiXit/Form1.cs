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
        PlayerL ppp;//włąsny gracz do wysyłania
        Message sendMsg; // wiadomosc wysyłana przez clienta
        PlayerL pppp2;
        playersData plData;
        PlayerL f3pl;
        bool activegame = false;
        System.Drawing.Color kolor;
        // Player player1 = new Player("22", "gracz1");       
        //  Player player2 = new Player("22", "gracz2");   
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads. 
        private volatile bool _shouldStop = false;
        Thread thread;



        public Form1(bool srv, Player p, Point loc, Form2 f2)
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

                thread = new Thread(new ThreadStart(serverStart1));     // wyrzucamy serwer do innego wątku 

            }
            else
            {
                sendMsg = preparesMSG(msgType.addPlayer);
                thread = new Thread(new ThreadStart(clientStart1));     // wyrzucamy serwer do innego wątku 


            }
            thread.Start();
            //   waitForPushGame.WaitOne();                       // czekamy aż spełnione zostaną warunki 
            //  runForm3();

        }

        public Message preparesMSG(msgType m)
        {

            PlayerL player = new PlayerL(pl);
            player.type = m;
            /*if (m == msgType.colorUpd)//do testów
              {
                  player.Lista[0].playerID = "test";
                  player.Lista[0].iPadd = "127.0.1.0";// na potrzeby testów
                  player.Lista[0].Color = kolor;
               }*/

            Message msg1 = SRL.Serialize(player); // w msg.Data jest obiekt do wysłania 
            return msg1;
        }


        public void RequestStop()//to sptop threads
        {
            if (server)
            {
                Message msg2 = preparesMSG(msgType.goOn);
                ss.sendMSG(msg2);

               
            }
            else
            {

            }
            _shouldStop = true;
        }

        public void sendClientMSG()
        {
            Message ms = new Message();
            ms.Data = cc.runClient(sendMsg.Data);
            PlayerL ppppp2 = SRL.takeM(ms);
            check_MSG(ppppp2);
        }


        public void processMSG()
        {
            Message msg2 = new Message();
            msg2.Data = ss.getMSG();
            if (msg2.Data != null)
            {
                PlayerL ppppp2 = SRL.takeM(msg2);
                check_MSG(ppppp2);
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
                PlayerL ppppp2 = SRL.takeM(msg2);
                check_MSG(ppppp2);
            }


           
           
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

            /*  for (int i = 0; i < 8; i++)
              { 
              Player p = new Player("127.0.0."+ i.ToString(), "aaa" + i.ToString());

              ppp.AddToPL(p);

              }*/
            Message msg1 = SRL.Serialize(ppp); // w msg.Data jest obiekt do wysłania 

            cc = new Client(pl);
            Message ms = new Message();
            cc.cltStart("89.70.34.25", 50201);
            ms.Data = cc.runClient(msg1.Data);
            PlayerL ppppp2 = SRL.takeM(ms);
            check_MSG(ppppp2);

            UPD_plList(plData.getList());
            //   startGame();
            Message ms1 = new Message();
            ms1.Data = cc.checkIfGameStarted();
            PlayerL togame = SRL.takeM(ms1);
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
            thread.Abort();

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







        public bool check_Start(PlayerL plL)
        {


            if (plL.type == msgType.startGame)
                return true;
            else return false;




        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)//button START
        {
            RequestStop();
          
           // runForm3();
           

        }

        private void start_the_game()
        {
           
            if (server)
            {
                PlayerL sss = new PlayerL();
                sss.type = msgType.startGame;

                Message m = response(sss);
                ss.sendMSG(m);
            }
        }
        private void runForm3()
        {
            PlayerL p = new PlayerL();
            p.lista = plData.getList();

            Form F3;
           // RequestStop();
        thread.Abort();
            if (server)

                F3 = new Form3(pl, this.Location, server, ss, p);
            else

                F3 = new Form3(pl, this.Location, server, cc);

            HideMe();
            F3.Enabled = true;
            F3.Visible = true;
            F3.Show();
        }


        private void button5_Click(object sender, EventArgs e)//START GRY
        {

            runForm3();
          //  HideMe();
            /*
            PlayerL p = new PlayerL();
            p.lista = plData.getList();
            Form F3;
            if (server)
                F3 = new Form3(pl, this.Location, server, ss, p);
            else
            {
                F3 = new Form3(pl, this.Location, server, cc);
            }
            HideMe();
            F3.Enabled = true;
            F3.Visible = true;//
            F3.Show();*/

        }



        private void HideMe()
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    this.Hide();
                }));

            }
            else
            {
                this.Hide();
            }
        }

		
		  private void clientStart1()
        {
            cc = new Client(pl);
            Message ms = new Message();
            cc.cltStart("89.70.34.25", 50201);
         
            sendClientMSG();

            UPD_plList(plData.getList());
            //   startGame();
            while (!_shouldStop)
            {
                Message ms1 = new Message();
                ms1.Data = cc.checkIfGameStarted();

                PlayerL togame = SRL.takeM(ms1);
                //      activegame = true;
                check_MSG(togame);
                if (_shouldStop) break;
            }

        }
		 private void serverStart1()
        {
            Message msg2 = new Message();
            ss = new Server(pl);
            ss.socketSart();
            while (!_shouldStop)
            {
                processMSG();
                if (_shouldStop) break;
            }


        }

		
    }

	     
}
