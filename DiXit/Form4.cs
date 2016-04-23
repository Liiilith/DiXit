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

        public Form4(int playersNumber, bool playerType)                             // konstruktor dla formy 4
        {
            InitializeComponent();

            playersNumb = playersNumber;
            challenger = playerType;

            CreateButtons(playersNumb);                        // tu juz powinienem znac info o ilosci graczy ktorzy uczestnicza w rozgrywce


        }

        protected void setLabels ()

        {
            Label informm = new Label;

            informm.Text = "Zagłosuj na kartę";

        }


        protected void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // identify which button was clicked and perform necessary actions


            //  ColectData(button);

            //    ConfirmButton();




        }

        protected void button_Clk(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // identify which button was clicked and perform necessary actions


            //    ColectDataChallange(button);

            //    ConfirmButton();




        }

        private void CreateButtons(int numbers)
        {
            int top = 50;
            int left = 100;
            int height = 80;
            int widht = 80;

            for (int i = 1; i <= numbers; i++)
            {
                Button button = new Button();
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
            confirm.Top = 500;
            confirm.Left = 100;
            Controls.Add(confirm);
            confirm.Text = "SEND";
            confirm.Width = 300;
            confirm.Height = 100;
            confirm.BackColor = Color.Red;

            confirm.Click += (object send, EventArgs ee) =>
            {



            };


        }

        protected void ColectData(Button b, int glosy)
        {



        }


        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}