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
    public partial class Form5 : Form
    {
        List<Button> listOfButtons = new List<Button>();             // lista buttonow do lepszego zarządzania
        Player myPlayer;
        int playersNumb;

        public Form5(int playersNumber, Player pl, Point location)
        {
            myPlayer = pl;
            playersNumb = playersNumber;

            this.Location = location;

            InitializeComponent();

            CreateButtons(playersNumb);

        }


        protected void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;              // tutaj identyfikacja kliknietego buttona

                markChoise(button);                 // dziala dokladnie jak w trybie challange
                ConfirmButton();                   
        }


        protected void setLabels()

        {
            Label informm = new Label();
            informm.Left = 100;
            informm.Top = 20;


            this.Controls.Add(informm);

                informm.Text = " SHOW YOUR CARD";
          

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
                    myPlayer.updatePlayer(collectData(), playerType.guesser);          // w przypadku podającego kartę
            };


        }

        protected int collectData()

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

     

        protected void markChoise(Button b)

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


        protected int scanColors()


        {
            int blueButtonsCount = 0;

            for (int i = 0; i < playersNumb; i++)
            {
                if (listOfButtons[i].BackColor == Color.Blue)


                {
                    blueButtonsCount++;
                }

            }

            return blueButtonsCount;

        }

        protected void manageColors(Button v, bool change)

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


        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
