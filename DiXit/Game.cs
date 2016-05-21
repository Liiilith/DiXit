using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace DiXit
{
    public class Game
    {
        playersData playersList;
        protected string gameIP;
        protected string ownIP;
        public Game(Player pl)
        {
            playersList = new playersData();
            playersList.AddToPlayerList(pl);
            gameIP = pl.getIpAddress();
        }

        public string GameIP
        {
            get
            {
                return gameIP;
            }
        }
        //string ownIP; // nie wiem czy bedzie potrzebne
        public playersData PlayerList
        {
            get
            {
                return playersList;
            }
        }

        public void addPlayer(Player pl)
        {
            playersList.AddToPlayerList(pl);
        }
    }
}
