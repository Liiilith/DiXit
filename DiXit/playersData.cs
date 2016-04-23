using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiXit
{
    class playersData               // klasa przechowująca dane o graczach - listę obiektów graczy, updatowana przez serwer i rozsyłana do wszystkich
    {

        List<Player> playersList = new List<Player>();

        public void AddToPlayerList(Player pl)                                 // tutaj sobie dodajemy do listy graczy 

        {
            Player checkPlayer = getPlayerByIp(pl.getIpAddress());                // sprawdzmy czy już go nie ma na liscie

            if (checkPlayer.getIpAddress() == "unknown")
            {
                playersList.Add(pl);
            }
        }
              
        public void RemoveFromPlayerList (Player pl)                            // a tu odejmujemy z listy graczy, na podstawie IP 
   
        {
            for (int i =0; i<=playersList.Count;i++)
            {

                if (pl.getIpAddress() == playersList[i].getIpAddress())

                {
                    playersList.RemoveAt(i);
                }
            }
        }

        public Player getPlayerByIp (string ip)                                      // zwróci nam playera z podanym IP gracza

        {
            Player foundPlayer;
            foundPlayer = new Player("unknown", "unknown");

            for (int i=0; i <= playersList.Count;i++)
            {
                if (playersList[i].getIpAddress() == ip)
                {
                    foundPlayer = playersList[i];
                    break;
                }
            }
            return foundPlayer;
        }

        public Player getPlayerByLogin(string login)                                    // zwróci nam gracza z listy po loginie

        {
            Player foundPlayer;
            foundPlayer = new Player("unknown", "unknown");

            for (int i = 0; i <= playersList.Count; i++)
            {
                if (playersList[i].getIpAddress() == login)
                {
                    foundPlayer = playersList[i];
                    break;
                }
            }
            return foundPlayer;
        }

    }
}
