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
        Player player1 = new Player("22", "11");        // to tak naprawdę zostanie stworzone wyżej
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {   //trzeba bedzie sprawdzic jakos czy kolor jest dostepny
                //if( gra.Check_rabbit(colorDialog1.Color)){
                this.BackColor = colorDialog1.Color;
                player1.selectColor(colorDialog1.Color);    
                // }
            }
        }
    }
}
