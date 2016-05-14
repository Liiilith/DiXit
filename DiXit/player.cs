using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace DiXit
{

   public  enum playerType
    {
        challanger, guesser, unsign
    };


    [SerializableAttribute]
    public class Player
    {

        public playerType type = playerType.unsign; //zaczynamy od typu nieokreslonego

        public string iPadd;
        public string playerID;
        public string PlayerID
        {
            get
            {
                return playerID;
            }
        }
        public System.Drawing.Color rabbitColor;
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

        public List<Player> votedOnMe;
        public int result;// ilość punktów w rundzie
        public int Result
        {
            get
            {
                return result;
            }
        }
        public int myVotes;
        public int[] cards;
      

        public Player(string ip, string login)
        {
            iPadd = ip;
            playerID = login;
           
            cards = new int[3];
            cards[0] = -1;
            cards[1] = -1;
            cards[2] = -1;
            votedOnMe = new List<Player>();
        }
        //ustaw color
        public void selectColor(System.Drawing.Color rc)
        {
        
            rabbitColor = rc;
        }

        // update gracza po msg:&id_gracza, &karta, &ch_flag/g_flag

        public void updatePlayer(int ownCard, playerType type)
        {
            this.type = type;
            this.cards[0] = ownCard;
        }


        public void updateVote(int firstCard)                      // update wyboru gracza w przypadku głosowania na jedną kartę
        {
            if (type == playerType.guesser)//coby nie oszukiwac
            {
                this.cards[1] = firstCard;
            }
        }


        public void updateVote(int firstCard, int secondCard)      // update wyboru gracza w przypadku głosowania na dwie karty

        {
            if (type == playerType.guesser)//coby nie oszukiwac
            {
                this.cards[1] = firstCard;
                this.cards[2] = secondCard;
            }
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

        public void updateVotingList(Player pl)         // dorzucamy glosujacego
        {
            // sprawdzmy czy już go nie ma na liscie

            if (!votedOnMe.Contains(pl))
            {
                votedOnMe.Add(pl);
                if (myVotes < 3) myVotes++;
            }

         }

        public void resetRound()   // czyscimy na koniec rundy
        {
            votedOnMe.Clear();
            myVotes = 0;
            result = 0;
            type = playerType.unsign;
            cards[0] = -1;
            cards[1] = -1;
            cards[2] = -1;

        }

        public bool selfVote()// czy gracz głosował na siebie, może sie przyda
        {
            if (cards[0] == cards[1] || cards[0] == cards[1]) return true;
            return false;
        }
        public int getMyCard()//zwraca kartę rzucną przez gracza
        {
            return cards[0];
        }
        public void  guessed(int p)
        {
            result = p;
        }

        public void setFinalScore() //koncowy winik
        {
            result += myVotes;
        }

        public int checkVote(int win) //spradz za ile gracz zgadl
        {
            int res = 0;
            if (type == playerType.guesser)
            {
                result = 0;
                if (cards[1] == win) result = 2;//gracz zgadł
                if (cards[2] == -1) result++;  // i głosował 1 grzybkiem
            }
            return res;
        }

        public bool checkVoteElse(int win) //spradz za ile gracz zgadl
        {
            if (type == playerType.guesser)
            {
                
                if (cards[1] == win || cards[2] == win) return true; //gracz głosował na daną kartę
            }
            return false;
        }
    }
}
