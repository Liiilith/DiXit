using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiXit
{
    public class playersData               // klasa przechowująca dane o graczach - listę obiektów graczy, updatowana przez serwer i rozsyłana do wszystkich
    {

        List<Player> playersList = new List<Player>() ;
        
        
        public playersData (List<Player> list)
        {
            playersList = list;
        }

        public List<Player> getPlayersList()

        {
            return playersList;
        }

        public void AddToPlayerList(Player pl)                                 // tutaj sobie dodajemy do listy graczy 

        {
            if (playersList.Count > 0)//inaczej rzuca index out of range 
            {
                Player checkPlayer = getPlayerByIp(pl.getIpAddress());                // sprawdzmy czy już go nie ma na liscie

                if (checkPlayer.getIpAddress() == "unknown")
                {
                    playersList.Add(pl);
                }

            }
            else
            {
                playersList.Add(pl);
            }
        }
              
        public void RemoveFromPlayerList (Player pl)                            // a tu odejmujemy z listy graczy, na podstawie IP 
   
        {
            if (playersList.Count > 0)//inaczej rzuca index out of range 
            {
                for (int i = 0; i <= playersList.Count; i++)
                {

                    if (pl.getIpAddress() == playersList[i].getIpAddress())

                    {
                        playersList.RemoveAt(i);
                    }
                }
            }
        }

        public List<Player> getList()
        {
            List<Player> pp = new List<Player>();
            foreach (Player p in playersList)
            {
               
                    pp.Add(p);
                
            }
            return pp;
        }

        public Player getPlayerByIp (string ip)                                    // zwróci nam playera z podanym IP gracza
       {
            Player foundPlayer;
            foundPlayer = new Player("unknown", "unknown");
            if (playersList.Count > 0)//inaczej rzuca index out of range 
            { 
                for (int i = 0; i < playersList.Count; i++)
                {
                    if (playersList[i].getIpAddress() == ip)
                    {
                        foundPlayer = playersList[i];
                        break;
                    }
                }
               
            }
            return foundPlayer;
        }

        public Player getPlayerByLogin(string login)                                    // zwróci nam gracza z listy po loginie

        {
            Player foundPlayer;
            foundPlayer = new Player("unknown", "unknown");
            if (playersList.Count > 0)//inaczej rzuca index out of range 
            {
                for (int i = 0; i < playersList.Count; i++)
                {
                    if (playersList[i].getIpAddress() == login)
                     {
                        foundPlayer = playersList[i];
                        break;
                    }
                }
            }
            return foundPlayer;
        }

        public int getChallenger()                                    // zwróci nam playera z podanym IP gracza
        {
            int foundPlayer;
            foundPlayer = -1;
            
                for (int i = 0; i < playersList.Count; i++)
                {
                    if (playersList[i].getType() == playerType.challanger)
                    {
                        foundPlayer = i;
                        break;
                    }
                }

          
            return foundPlayer;
        }


        public void udpdateData (Player player)        // single player update 

        {

            RemoveFromPlayerList(player);
            AddToPlayerList(player);


        }

        public bool veryfiPlayersTypes ()                                         // do sprawdzenia czy nie ma dwoch chalengerow

        {
            bool verdict = true;
            int challengerCount=0;
            if (playersList.Count > 0)//inaczej rzuca index out of range 
            {
                for (int i = 0; i < playersList.Count; i++)
                {                  
                   if( playersList[i].getType() == playerType.challanger)
                    {
                        challengerCount++;
                        if (challengerCount > 1) { verdict = false; break; }
                    }
                }
            }
            return verdict;
        }



        public bool checkIfallAlreadyPresent()                                         // do sprawdzenia wszyscy gracze maja okreslony typ

        {
            bool verdict = true;
         
            if (playersList.Count > 0)//inaczej rzuca index out of range 
            {
                for (int i = 0; i < playersList.Count; i++)
                {
                    if (playersList[i].getType() == playerType.unsign)
                    {
                      
                    verdict = false;
                      break; 
                    }
                }
            }
            return verdict;
        }





        public bool checkColor(System.Drawing.Color c)//czy możemy wybrać sobie kolor
        {
            bool result = true;
            if (playersList.Count > 0)//inaczej rzuca index out of range 
            {
                for (int i = 0; i < playersList.Count; i++)
                {
                    if (playersList[i].Color == c) result = false;
                }

            }
            return result;
        }

        public void sumUpMainCard()
        {
            int chPlayer = getChallenger();//szukamy indexu dajacego skojarzenie
            int mainCard = playersList[chPlayer].getMyCard();//i jego karty
            int voted = 0;
            
            for (int i = 0; i < playersList.Count; i++)
            {
                voted += playersList[i].checkVote(mainCard);
            }
            if (voted == 0 || voted == playersList.Count - 1) voted = 0;
            else voted = 3;
            playersList[chPlayer].guessed(voted);

            for (int i = 0; i < playersList.Count; i++)
            {
                for (int j = 0; j < playersList.Count; j++)
                {
                    if (j != i)
                    {
                        playersList[j].checkVoteElse(playersList[i].getMyCard());
                        playersList[i].updateVotingList(playersList[j]);
                    }
                }
                playersList[i].setFinalScore();
            }


        }



        public bool checkOwnCards(Player pl)     // czy ownCardy są różne ?

        {
            int[] t = new int[playersList.Count + 1];
            if (playersList.Count > 0)//inaczej rzuca index out of range 
            {
                for (int i = 0; i < playersList.Count; i++)
                {
                    t[playersList[i].getMyCard()]++;

                }

                for (int i = 1; i <= playersList.Count; i++)
                {
                    if (t[i] != 1) return false;
                    // if (t[i] > 1) return false; //t[i]=0 => t[j]>1
                }

            }
            return true;
        }

        public void clearRound()
        {
            for (int j = 0; j < playersList.Count; j++)
            {
                playersList[j].resetRound();
            }
        }
    }
}
