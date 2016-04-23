using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace DiXit
{

    enum playerType
    {
        challanger, guesser, unsign
    };



    class Player
    {


        private playerType type = playerType.unsign; //zaczynamy od typu nieokreslonego

        private string iPadd;
        private string playerID;
        public string PlayerID
        {
            get
            {
                return playerID;
            }
        }
        private System.Drawing.Color rabbitColor;
        public System.Drawing.Color Color
        {
            get
            {
                return rabbitColor;
            }
            set
            {
                rabbitColor = value;
            }
        }


        private int result;// ilość punktów w rundzie
        private int[] cards;
        /*
        cards[0]- własna karta
        cards[1] cards[2] głosy gracza
>>>>>>> 8356a40847c86025f3e28e343f9a81cc888f550a



        */

        public Player(string ip, string login)
        {
            iPadd = ip;
            playerID = login;
            cards = new int[3];
            cards[0] = -1;
            cards[1] = -1;
            cards[2] = -1;
        }


        // update gracza po msg:&id_gracza, &karta, &ch_flag/g_flag

        public void updatePlayer(int ownCard, playerType type)
        {
            this.type = type;
            this.cards[0] = ownCard;
        }


        public void updateVote(int firstCard)                      // update wyboru gracza w przypadku głosowania na jedną kartę

        {
            this.cards[1] = firstCard;
        }


        public void updateVote(int firstCard, int secondCard)      // update wyboru gracza w przypadku głosowania na dwie karty

        {
            this.cards[1] = firstCard;
            this.cards[2] = secondCard;
        }

        public string getIpAddress()                               // getter do ip 

        {

            return iPadd;

        }

        public void setTyp(playerType a)      // procedura ustawiajaca typ gracza 

        {
            type = a;
        }


        public playerType getType()           // zwraca typ gracza
        {
            return type;
        }
    }
}
