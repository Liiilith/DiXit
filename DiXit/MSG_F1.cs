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


        public void check_MSG(PlayerL plL)
        {


            switch (plL.type)
            {

                case msgType.startGame:
                    _shouldStop = true;
                    button5.Invoke(new Action(delegate ()
                    {
                        activegame = true;
                        button5.Show();
                        button5.PerformClick();
                        button5.Hide();
                    }));

                    /*
                    this.Invoke(new Action(delegate ()
                    {
                        this.Hide();
                    }));*/
                    break;

                case msgType.colorUpd:
                    if (server)
                    {
                        if (plData.checkColor(plL.Lista[0].Color))
                        {
                            MSGAddPlayers(plL);//do testów
                                               //  MSGUpdPlayers(plL)// nie działa do konca
                            MSGUpdColorss(plL);
                            UPD_plList(plData.getList());
                            SendColorRes(msgType.okColor);
                        }
                        else
                        {
                            SendColorRes(msgType.wrongColor);
                        }
                    }
                    else // klient powinien tylko zupdatowac dostanych graczy
                    {
                        MSGAddPlayers(plL);
                        //   MSGUpdPlayers(plL);
                        MSGUpdColorss(plL);
                        UPD_plList(plData.getList());
                    }

                    break;

                case msgType.wrongColor:
                    MSGAddPlayers(plL);
                    updButtonColorWRONG();
                    MSGUpdColorss(plL);
                    //  MSGUpdPlayers(plL);
                    UPD_plList(plData.getList());
                    // LockButton1(true);
                    break;

                case msgType.okColor:
                    MSGAddPlayers(plL);
                    //     MSGUpdPlayers(plL);
                    MSGUpdColorss(plL);
                    Player p = plData.getPlayerByIp(pl.iPadd);
                    //  Player p = plData.getPlayerByLogin(pl.playerID);
                    pl.rabbitColor = kolor;
                    plData.Change_Color(pl, kolor);
                    //  updOwnColor();


                    UPD_plList(plData.getList());
                    //  LockButton1(true);
                    updButtonColor();

                    break;

                case msgType.addPlayer:

                    //dodaj gracza
                    activegame = true;
                    MSGAddPlayers(plL);
                    // label3.Text = ppp.lista[0].playerID;
                    UPD_plList(plData.getList());
                    PlayerL sss = new PlayerL();
                    sss.type = msgType.gameOn;

                    sss.lista = plData.getList();
                    Message m = response(sss);
                    ss.sendMSG(m);

                    break;

                case msgType.gameOn://serwer odpowiedzial

                    activegame = true;
                    MSGAddPlayers(plL);
                    //  MSGUpdPlayers(plL);
                    //  MSGUpdColorss(plL);//troche za duzo
                    UPD_plList(plData.getList());

                    break;


                case msgType.goOn://serwer odpowiedzial


                    if (!server)
                    {
                        Message msg4 = preparesMSG(msgType.goOn);
                        cc.sendOnlyClient(msg4.Data);
                    }
                    else
                    {
                        start_the_game();
                        button5.Invoke(new Action(delegate ()
                        {
                            activegame = true;
                            button5.Show();
                            button5.PerformClick();
                            button5.Hide();
                        }));
                    }

                    break;

                default:
                    Console.WriteLine("Default case");
                    break;
            }


        }







    }
}
