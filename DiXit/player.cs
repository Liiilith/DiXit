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


        playerType type;

        public void setTyp (playerType a)      // procedura ustawiajaca gracza 

        { type = a; }


        public playerType getType()           // zwraca typ gracza
        {
            return type;
        } 




    }
}
