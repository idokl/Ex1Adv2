using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class JoinCommand : ICommand
    {
        private IModel model;
        public JoinCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            MultiPlayer mpJoin;
            try
            {
                mpJoin = this.model.join(name);
                mpJoin.JoinGameClient = client;
               // mpJoin.IsAvilble = false;
            }
            catch
            {
                Console.WriteLine("the name of game to join isn't exist");
            }
           
            PacketStream joinPacketStream = new PacketStream
            {
                MultiPlayer = true,
                StringStream = ""
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(joinPacketStream);
        }
    }
}
