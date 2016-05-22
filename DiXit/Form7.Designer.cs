namespace DiXit
{
    partial class Form7
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button = new System.Windows.Forms.Button[16];

            this.SuspendLayout();

            for (int i = 0; i < 16; i++)
            {
                int posy = (i / 4)*40;
                int posx = (i % 4)*40;
                System.Windows.Forms.Button  p = new System.Windows.Forms.Button();
                p.Location = new System.Drawing.Point(10 + posx, 10 + posy);
                p.Size = new System.Drawing.Size(30, 30);
                p.UseVisualStyleBackColor = true;
                p.Text = "";
                p.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                p.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                p.Size = new System.Drawing.Size(30, 30);
                p.TabIndex = i + 1;
                p.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                p.Name = "button" + i.ToString();

                button[i] = p;
                button[i].Click += new System.EventHandler(this.button_Click);
                this.Controls.Add(button[i]);
            }

            this.button[0].BackColor = System.Drawing.Color.Red;
            this.button[1].BackColor = System.Drawing.Color.DimGray;
            this.button[2].BackColor = System.Drawing.Color.Blue;
            this.button[3].BackColor = System.Drawing.Color.DarkOrange;
            this.button[4].BackColor = System.Drawing.Color.Lime;
            this.button[5].BackColor = System.Drawing.Color.Black;
            this.button[6].BackColor = System.Drawing.Color.Yellow;
            this.button[7].BackColor = System.Drawing.Color.Purple;
            this.button[8].BackColor = System.Drawing.Color.Coral;
            this.button[9].BackColor = System.Drawing.Color.White;
            this.button[10].BackColor = System.Drawing.Color.Maroon;
            this.button[11].BackColor = System.Drawing.Color.Coral;
            this.button[12].BackColor = System.Drawing.Color.Cyan;
            this.button[13].BackColor = System.Drawing.Color.Green;
            this.button[14].BackColor = System.Drawing.Color.DeepPink;
            this.button[15].BackColor = System.Drawing.Color.SpringGreen;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(170, 170);
            this.Name = "Form7";
            this.Text = "Colors";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button[] button;

    }
}