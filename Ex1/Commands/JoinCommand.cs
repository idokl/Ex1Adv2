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
            model.join(name);
            PacketStream joinPacketStream = new PacketStream(true, "");
            return Newtonsoft.Json.JsonConvert.SerializeObject(joinPacketStream);
        }
    }
}
