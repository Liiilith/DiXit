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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            votedOnMe();
        }

        private void votedOnMe()
        {
            for (int i = 0; i < 12; i++)
            {
                int posy = (i / 2) * 40;
                int posx = (i % 2) * 250;
                System.Windows.Forms.Button button;
                System.Windows.Forms.Label label;
                button = new System.Windows.Forms.Button();
                label = new System.Windows.Forms.Label();

                button.Location = new System.Drawing.Point(160+posx, 10 + posy);
                button.Name = "button";
                button.Size = new System.Drawing.Size(30, 30);

                button.UseVisualStyleBackColor = true;
                button.BackColor = System.Drawing.Color.Gainsboro;
                button.Text = "";


                label.AutoSize = true;
                label.BackColor = System.Drawing.Color.Transparent;
                label.Cursor = System.Windows.Forms.Cursors.AppStarting;
                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                label.Location = new System.Drawing.Point(10 + posx, 10 + posy);
                label.Name = "label";
                label.RightToLeft = System.Windows.Forms.RightToLeft.No;
                label.Size = new System.Drawing.Size(100, 25);
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                label.Text = "aaaaaaaaaa";
                panel1.Controls.Add(label);
                panel1.Controls.Add(button);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }

    }
}

