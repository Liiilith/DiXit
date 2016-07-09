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
    public partial class Form3 : Form // 220516 aaa
    {
        // deklaracja zmiennych dla Ekranu 3
        Player player1;        // to tak naprawdę zostanie stworzone wyżej
        bool serwer;
        Server ss;
        Client cc;
        PlayerL recentList;

        public Form3(Player pl, Point location,bool connection, Server serv, PlayerL actualList)
        {
            InitializeComponent();

            recentList = actualList;
            player1 = pl;
            ss = serv;
            serwer = connection;             // czy jestem serwerem czy clientem     
            this.Location = location;
            this.Show();
            buttonsLook();         // nazwy buttonów i inne
                                  
            button1.Click += startClick_EventHandler;

            Thread communication = new Thread(new ThreadStart(exchangePlayerData));     // wyrzucamy serwer do innego wątku 
            communication.Start();
        }

        private readonly ManualResetEvent waitForclick = new ManualResetEvent(false);   

        private void startClick_EventHandler(object sender, EventArgs e)
        {
            waitForclick.Set();
        }

        public Form3(Player pl, Point location, bool connection, Client cli)
        {
            InitializeComponent();

            player1 = pl;
            cc = cli;
            serwer = connection;             // czy jestem serwerem czy clientem

            this.Location = location;
            this.Show();
            buttonsLook();         // nazwy buttonów i inne
                                   //   this.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Click += startClick_EventHandler;

            Thread communication  = new Thread(new ThreadStart(exchangePlayerData));     // wyrzucamy serwer do innego wątku 
            communication.Start();

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void exchangePlayerData ( )

        {
            if (serwer)
            {

                Message TypeData = new Message();

               TypeData.Data =  ss.getMSG();        // czekamy na message, odbieramy // deserializujemy

                processMSG(TypeData);                // wrzucamy message do obrobki 

                waitForclick.WaitOne();             // poczekaj z weryfikacja az serwer kliknie !!!

                PlayerL confirmGame = new PlayerL();

                if (veryfyList(recentList))             // veryfikujemy liste (to bedzie inaczej wygladac w przypadku wielu graczy)
                {
                     // ( message z lista gdzie jest pozwolenie na gre )
                        // jak w porzadku to wysylamy do klient ze lecimy dalej
                    createNewForm(true);
                }
 
                 else
                {
                    confirmGame.type = msgType.empty;
                    //         ss.sendMSG ( Message z lista gdzie nie zezwala sie na gre       
                    // a jak nie to gra jest wsrzymana i wybieramy jeszcze raz 
                }             
            }

            else

            {

                waitForclick.WaitOne();     // zanim klient sie ruszy to musi poczekac az zostana dane wpisane

                PlayerL listTosend = new PlayerL(player1);          // do seralizacji zabieramy aktualnego playera 

                Message singlePlayer = SRL.Serialize(listTosend);   // serializujemygo i budujemy message  

                cc.sendToServer(singlePlayer.Data);                // wysylamy go 

                Message dataVer = new Message();

                dataVer.Data = cc.checkIfGameStarted();     // tu jest tablica z danymi do zweryfikowania 

                PlayerL ver = SRL.takeM(dataVer);


                if (checkD(ver))                      // vczekamy na weryfikacje danych 

                { createNewForm(false); }

                else
                { } // wybierzcie jeszcze raz}  
            }
           
        }

        protected bool checkD (PlayerL pla)

        {
            if (pla.type == msgType.startGame)
                return true;
            else
                return false;
        }

        private void processMSG(Message m)
        {
            PlayerL recivedList = SRL.takeM(m);            // tutaj tylko jednoelementowa lista 
            if (recivedList != null)
            {
                if (recivedList.lista != null)             // sprawdzamy czy otrzymana lista jest niepusta
                {
                         playersData data = new playersData(recentList.lista);

                    for (int i=0; i < recivedList.lista.Count();i++ )

                        {                
                            data.udpdateData(recivedList.lista[i]);                  // aktualizujemy to co dostalismy
                        }

                            recentList.lista = data.getPlayersList();               // zrzucamy liste do listy (za duzo tego)
                        //   updatePlayerList2(ppp.lista);
                    }
                }
            }
        
        protected void createNewForm (bool server)


        {

            if (server)
            {
                if (player1.getType() == playerType.challanger)
                {

                    Form votingScreen = new Form4(12, true, player1, this.Location, ss);             // ta liczna graczy musi byc wzieta z serwera
                    votingScreen.Show();
                    this.Hide();

                    // startujemy nową forme 4, z ustawianiami w zależności od typu gracza 
                }

                else if (player1.getType() == playerType.guesser)
                {
                    Form votingScreen = new Form4(12, false, player1, this.Location,ss);             // ta liczna graczy musi byc wzieta z serwera
                    votingScreen.Show();
                    this.Hide();
                    // jw  
                }
            }
            else
            {
                if (player1.getType() == playerType.challanger)
                {

                    Form votingScreen = new Form4(12, true, player1, this.Location, cc);             // ta liczna graczy musi byc wzieta z serwera
                    votingScreen.Show();
                    this.Hide();

                    // startujemy nową forme 4, z ustawianiami w zależności od typu gracza 
                }

                else if (player1.getType() == playerType.guesser)
                {
                    Form votingScreen = new Form4(12, false, player1, this.Location, cc);             // ta liczna graczy musi byc wzieta z serwera
                    votingScreen.Show();
                    this.Hide();
                    // jw  
                }
            }

        }

        private bool veryfyList (PlayerL actuallist)

        {
            playersData listForVer = new playersData(actuallist.lista);

            return listForVer.veryfiPlayersTypes();           
        }

        private bool veryfiStart(Message m)

        {
            PlayerL recivedList = SRL.takeM(m);

            if (recivedList != null)
            {
                if (recivedList.lista != null)             // sprawdzamy czy otrzymana lista jest niepusta
                {
                    playersData data = new playersData(recentList.lista);
                    if (true)
                    { return false; }
                         
                }
            }
        
            return true;
        }

        private void button1_Click(object sender, EventArgs e)          // to button startu, lecą dane do serwera i od clienta   
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switchButtons(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switchButtons(true);
        }

        protected void switchButtons (bool change)


        {
            if (change)
            {

                player1.setTyp(playerType.challanger);

                
                

                button2.BackColor = Color.Blue;
                button3.BackColor = Color.DarkGray;
            }

            else


            {
                player1.setTyp(playerType.guesser);
                button3.BackColor = Color.Blue;
                button2.BackColor = Color.DarkGray;
            }

        }

        private void buttonsLook()
        {
            button1.Text = "Start";
            button2.Text = "CHALLANGE";
            button3.Text = "GUESS";
            button1.BackColor = Color.DarkGray;
            button2.BackColor = Color.DarkGray;
            button3.BackColor = Color.DarkGray;
        }
    }

}
