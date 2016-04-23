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


        private playerType type;
        private string iPadd;
        private string playerID;
        private System.Drawing.Color rabbitColor;
        private int result;// ilość punktów w rundzie
        private int[] cards;
        /*
        cards[0]- własna karta
        cards[1] cards[2] głosy gracza



        */

        public Player(string ip,string login)
        {
            iPadd = ip;
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
      
        public void setTyp (playerType a)      // procedura ustawiajaca gracza 

        { type = a; }


        public playerType getType()           // zwraca typ gracza
        {
            return type;
        } 




    }
}
