using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DiXit
{
    public partial class Form1 : Form
    {



        private void MSGUpdColorss(PlayerL ppppp2)//updatuje playersData o otrzymaną listę graczy
        {

            if (ppppp2 != null)
            {
                if (ppppp2.lista != null)
                {
                    if (ppppp2.lista.Count > 0)
                    {
                        foreach (Player p in ppppp2.lista)
                        {
                            plData.Change_Color(p, p.rabbitColor);
                           // updPlayerColor(p);
                        }

                    }
                }
            }
        }



        public bool updOwnColor()
        {
            //Player p = plData.getPlayerByLogin(pl.playerID);
           

            bool res = false;
            if (plData.checkColor(kolor))
            {
                pl.rabbitColor = kolor;
                //  p.rabbitColor = kolor;
                //  plData.UpdatePlayerID(pl);
                if (plData.Change_Color(pl, kolor))
                    res = true;

            }
            return res;

        }



        public bool updPlayerColor(Player pp)
        {
            //Player p = plData.getPlayerByLogin(pl.playerID);
            Player p = plData.getPlayerByIp(pp.iPadd);

            //  plData.UpdatePlayerID(pl);
            plData.Change_Color(pp, pp.rabbitColor);



            return true;

        }





        public void SENDcolor()
        {
            if (!server)
            {
                PlayerL sss = new PlayerL(pl);
                /*  for (int i = 0; i < 3; i++)
                  {
                      Player p = new Player("127.0.1." + i.ToString(), pl.playerID);
                      p.rabbitColor = pl.rabbitColor;

                      sss.AddToPL(p);

                  }*/
                sss.Lista[0].playerID="test";
                sss.Lista[0].iPadd = "127.0.1.0";// na potrzeby testów
                sss.Lista[0].Color = kolor;
               // LockButton1(false);
                sss.type = msgType.colorUpd;
                Message m = response(sss);
                Message ms = new Message();
                ms.Data = cc.runClient(m.Data);

            }
        }

        public void SendColorRes(msgType cres)//zwrotka na zmianę koloru
        {
            if (server)
            {
                PlayerL sss = new PlayerL();
                sss.Lista = plData.getList();
                sss.type = cres;
                Message m = response(sss);
                Message ms = new Message();
                ss.sendMSG(m);
            }
        }


        public void LockButton1(bool p)
        {
            button1.Invoke(new Action(delegate ()
            {
                button1.Enabled = p;

            }));
        }

        public void updButtonColor()
        {
            button1.Invoke(new Action(delegate ()
            {
                button1.BackColor = kolor;
                button1.Text = "";
            }));
        }
        public void updButtonColorWRONG()
        {
            kolor = System.Drawing.Color.LemonChiffon;


            button1.Invoke(new Action(delegate ()
            {
                button1.BackColor = kolor;
                button1.Text = "X";
            }));
        }



        public void UPD_srv_col()//zaktualizuj i wyswietl nowe kolory i odeslij reszcie
        {
            UPD_plList(plData.getList());
            if (activegame)
            {

                if (server)
                {
                    PlayerL p = new PlayerL();
                    p.lista = plData.getList();
                    p.type = msgType.colorUpd;

                    Message m = response(p);
                    ss.sendMSG(m);
                }

            }
        }

        
    }
}
