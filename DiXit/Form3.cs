
﻿using System;
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
    public partial class Form3 : Form
    {
        // deklaracja zmiennych dla Ekranu 3

        public Form3()
        {
            InitializeComponent();
            buttonsLook();         // nazwy buttonów i inne

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void buttonsLook()
        {
            button1.Text = "Start";
            button2.Text = "CHALLANGE";
            button3.Text = "GUESS";
        }


      

    }




}
