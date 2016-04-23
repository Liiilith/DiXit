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
            buttonsLook();   
            // nazwy buttonów i inne
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

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

        }

        private void buttonsLook()
        {
            button2.Visible = server;
            button2.Enabled = server;
            button2.Text = "START";
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
                 System.Windows.Forms.Button button;
                 System.Windows.Forms.Label label;
           button = new System.Windows.Forms.Button();
            label = new System.Windows.Forms.Label();

            button.Location = new System.Drawing.Point(379, 14);
            button.Name = "button";
            button.Size = new System.Drawing.Size(60, 39);
          
            button.UseVisualStyleBackColor = true;
            button.BackColor = player2.Color;
            button.Text = "";


            label.AutoSize = true;
            label.BackColor = System.Drawing.Color.Transparent;
            label.Cursor = System.Windows.Forms.Cursors.AppStarting;
            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            label.Location = new System.Drawing.Point(19, 14);
            label.Name = "label";
            label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            label.Size = new System.Drawing.Size(128, 39);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Text = player1.PlayerID;
            splitContainer1.Panel2.Controls.Add(label);
            splitContainer1.Panel2.Controls.Add(button);
            


        }
           
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
