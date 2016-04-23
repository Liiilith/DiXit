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
    public partial class Form1 : Form
    {
        //bedzie już istnieć
        bool server = false;
       // Player player1 = new Player("22", "gracz1");       
      //  Player player2 = new Player("22", "gracz2");   
            
        

        public Form1(string gameIP,string plID,bool srv)
        {
            server = srv;
            InitializeComponent();
            buttonsLook(gameIP,plID);
            // nazwy buttonów i inne
            this.Show();
        }

      

        public void serverSet(bool set)
        {
            server = set;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Player player1 = new Player("22", "gracz1");
            Button bt = sender as Button;
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {   //trzeba bedzie sprawdzic jakos czy kolor jest dostepny
                //if( gra.Check_rabbit(colorDialog1.Color)){
                bt.BackColor = colorDialog1.Color;
                player1.Color=colorDialog1.Color;    
                // }
            }
            updatePlayerList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 F3 = new Form3();
            F3.Show();
            this.Hide();
            F3.Visible = true;
        }

        private void buttonsLook(string gameIP, string plID)
        {
            button2.Visible = server;
            button2.Enabled = server;
            button2.Text = "START";
            label1.Text = gameIP;
            label2.Text = plID;
        }
           public void updatePlayerList()// List<Player> players) vs nie przyjmuje listy jako arg przez ograniczenia dostepu (?)
            {
            Player player1 = new Player("22", "gracz1");
            Player player2 = new Player("22", "gracz2");
            player2.Color = System.Drawing.Color.Black;
            List<Player> tempeGracze = new List<Player>();
            tempeGracze.Add(player1);
            tempeGracze.Add(player2);
            tempeGracze.Remove(player1);
        
            foreach(Player p in tempeGracze)
             {
                //wyswietlanie listy graczy
              }
//tymczasowe wyswietlanie
             for(int i = 0; i < 12; i++)
            {
                System.Windows.Forms.Button button;
                System.Windows.Forms.Label label;
                button = new System.Windows.Forms.Button();
                label = new System.Windows.Forms.Label();

                button.Location = new System.Drawing.Point(379, 10 + i * 60);
                button.Name = "button";
                button.Size = new System.Drawing.Size(60, 39);

                button.UseVisualStyleBackColor = true;
                button.BackColor = player2.Color;
                button.Text = "";


                label.AutoSize = true;
                label.BackColor = System.Drawing.Color.Transparent;
                label.Cursor = System.Windows.Forms.Cursors.AppStarting;
                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                label.Location = new System.Drawing.Point(10, 10+i*60);
                label.Name = "label";
                label.RightToLeft = System.Windows.Forms.RightToLeft.No;
                label.Size = new System.Drawing.Size(128, 39);
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                label.Text = player1.PlayerID;
                panel1.Controls.Add(label);
                panel1.Controls.Add(button);
            }
                 
            


        }
           
     
    }
}
