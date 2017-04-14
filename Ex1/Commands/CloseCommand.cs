using System.Net.Sockets;
using Newtonsoft.Json;

namespace Ex1.Commands
{
    class CloseCommand : ICommand
    {
        private IModel model;
        public CloseCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            model.close(name);
            PacketStream closePacketStream = new PacketStream
            {
                MultiPlayer = true,
                StringStream = ""
            };
            return JsonConvert.SerializeObject(closePacketStream);
        }
    }
}
