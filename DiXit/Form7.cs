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
    public partial class Form7 : Form
    {
        Form1 F1;
        public System.Drawing.Color kolor = System.Drawing.Color.Red;
        public Form7(List<System.Drawing.Color> cl,Form1 F)
        {
            InitializeComponent();
             doButtons(cl);
            F1 = F;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
           F1.setColor( b.BackColor);
            if (F1.updOwnColor())
            {
                F1.updButtonColor();
                if (!F1.srv_Check()) F1.SendColorUpd();
                else F1.UPD_srv_col();
            }
           
            this.Close();
        }
        public Color getColor()
        {
           
           return kolor ;
            
        }

        public Color getColor3()
        {

            return kolor;

        }

        private void doButtons(List<System.Drawing.Color> c)
        {
            if (c.Count() > 0)
            {
                foreach (Color col in c)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (button[i].BackColor == col)
                        {
                            button[i].Enabled = false;
                            button[i].Visible = false;
                        }
                    }
                }
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
