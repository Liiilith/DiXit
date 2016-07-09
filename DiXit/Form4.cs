using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiXit
{
    public partial class Form4 : Form
    {

        int playersNumb;                                             // forma 4 to ekran głosowania, liczba graczy powinna się zawrzeć w konstruktorze 
        bool challenger;                                             // zmienna informująca czy gracz zadaje kartę czy zgaduje 
        List<Button> listOfButtons = new List<Button>();             // lista buttonow do lepszego zarządzania
        Player myPlayer;
        Server ss;
        Client cc;


        public Form4(int playersNumber, bool playerType, Player pl, Point locat, Server serv)       // konstruktor dla formy 4
        {
            InitializeComponent();
            ss = serv;
            myPlayer = pl;
            playersNumb = playersNumber;
            challenger = playerType;
            setLabels(playerType);
            this.Location = locat;
            CreateButtons(playersNumb);                        // tu juz powinienem znac info o ilosci graczy ktorzy uczestnicza w rozgrywce


        }

        public Form4(int playersNumber, bool playerType, Player pl, Point locat, Client client)                             // konstruktor dla formy 4
        {
            InitializeComponent();
            cc = client;
            myPlayer = pl;
            playersNumb = playersNumber;
            challenger = playerType;
            setLabels(playerType);
            this.Location = locat;
            CreateButtons(playersNumb);                        // tu juz powinienem znac info o ilosci graczy ktorzy uczestnicza w rozgrywce


        }


        protected void setLabels (bool type)

        {
            Label informm = new Label();
            informm.Left = 100;
            informm.Top = 20;
           

            this.Controls.Add(informm);

            if (type == false)
                informm.Text = "VOTE";
            else

            {
                informm.Text = "MARK YOUR CARD";
            }

        }

        protected void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;              // tutaj identyfikacja kliknietego buttona

            if (challenger)
            {
                markChoiseChallange(button);
            }
                else
            {
                markChoise(button);
            }

            ConfirmButton();
        }

        private void CreateButtons(int numbers)
        {
            int top = 70;
            int left = 100;
            int height = 80;
            int widht = 80;

            for (int i = 1; i <= numbers; i++)
            {
                Button button = new Button();
                listOfButtons.Add(button);
                button.BackColor = Color.GreenYellow;
                button.Width = widht;
                button.Height = height;
                button.Left = left;
                button.Top = top;
                button.Text = i.ToString();
                button.Name = i.ToString();
                this.Controls.Add(button);
                button.Click += new EventHandler(button_Click);            // podpinamy zdarzenie, króre jest reakcją na kliknienicie w strorzony button
                left += button.Height + 2 + widht / 3;
                int a = (int)Math.Round(Math.Sqrt(numbers));
                if (i % a == 0)
                {
                    top += button.Height + 2 + height / 3;
                    left = 100;
                }
            }
        }

        protected void ConfirmButton()
        {
            Button confirm = new Button();
            confirm.Top = 520;
            confirm.Left = 100;
            Controls.Add(confirm);
            confirm.Text = "SEND";
            confirm.Width = 300;
            confirm.Height = 100;
            confirm.BackColor = Color.Red;

            confirm.Click += (object send, EventArgs ee) =>
            {

                if (challenger)
                {
                    myPlayer.updatePlayer(collectData(), playerType.challanger);          // w przypadku podającego kartę
                }

                else
                {
                    collectVoteData();                           // głosy oddane przez playera zostają zapisane  
                    Form5 showCardGuesser = new Form5(playersNumb, myPlayer,  this.Location);
                    showCardGuesser.Show();
                    this.Hide();
                }                                      
            };


        }

        protected int collectData ()

        {
            int vote = 0;
            for (int i = 0; i < playersNumb; i++)
            {
                if (listOfButtons[i].BackColor == Color.Blue)

                {
                  vote = Int32.Parse(listOfButtons[i].Name);                 
                }

            }

            return vote;
        }

        protected int collectVoteData ()

        {
            List<int> vote = new List<int>();

            for (int i = 0; i<playersNumb; i++)
            {
                if (listOfButtons[i].BackColor == Color.Blue)

                {
                    string temp = listOfButtons[i].Name;
                    vote.Add(Int32.Parse(temp));
                }
            }

            int s = vote.Count();

            switch (s)
            {
              case 1:
                    myPlayer.updateVote(vote[0]);
                    break;
               case 2:
                    myPlayer.updateVote(vote[0],vote[1]);    
                    break;
             
            }

            return vote.Count();


        }

        protected void markChoiseChallange(Button b)

        {
            if (b.BackColor == Color.Blue)
            {
                // cofamy głos kolor niebieski staje sie zielonym
                manageColors(b, true);
            }
            else
            {
                if (scanColors() < 1)
                {
                    //  zaznaczamy głos kolor zielony staje się niebieski
                    manageColors(b, false);
                }
            }
        }

        protected void markChoise(Button b)
        {
            if (b.BackColor == Color.Blue)
            {
                                               
                manageColors(b,true);                     // cofamy głos kolor niebieski staje sie zielonym     
            }

           
            else {
                if (scanColors() < 2)
                {
                    //  zaznaczamy głos kolor zielony staje się niebieski
                 manageColors(b, false);
                }
            }



        }

        protected int scanColors ()


        {
            int blueButtonsCount=0;

            for (int i=0; i < playersNumb; i++)
            {
                if (listOfButtons[i].BackColor == Color.Blue)


                {
                    blueButtonsCount++;
                }

            }

            return blueButtonsCount;

        }

        protected void manageColors (Button v,bool change)

        {

            if (change)
            {
                v.BackColor = Color.GreenYellow;
            }

            else
            {

                v.BackColor = Color.Blue;
            }


        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}