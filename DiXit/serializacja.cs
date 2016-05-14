using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DiXit
{

    public class Message
    {
        public byte[] Data { get; set; }



    }

    class SRL
    {
        public static Message Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return new Message { Data = memoryStream.ToArray() };
            }
        }

        public static object Deserialize(Message message)
        {
            using (var memoryStream = new MemoryStream(message.Data))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }


        public static Message doM(PlayerL player)
        {
            Message message = SRL.Serialize(player);

            return message;
        }

        public static PlayerL takeM(Message message)
        {

            object obj = SRL.Deserialize(message);
            if (obj is PlayerL)
                return obj as PlayerL;
            else return null;
        }


    }

    [SerializableAttribute]
    public class PlayerL
    {

        public playerType type = playerType.unsign; //zaczynamy od typu nieokreslonego


        public List<Player> lista { get; set; }
        public bool listflag = false;


        public PlayerL()
        {
        }
        public PlayerL(Player i)
        {

            lista = new List<Player>();
            lista.Add(i);
        }

        public void AddToPL(Player pl)                                 // tutaj sobie dodajemy do listy graczy 

        {
                if (lista.IndexOf(pl)==-1)
                {
                    lista.Add(pl);
                }

            
        }

        public String getPlayers()
        {
            String s = "";
            if (lista == null) return "dupa";
            if (lista.Count > 0)
            {
                foreach (Player p in lista)
                {
                    s += p.PlayerID;
                    s += ":\t";
                    s += p.iPadd;
                    s += "\n";
                }
            }
            return s;
        }
    }



}
