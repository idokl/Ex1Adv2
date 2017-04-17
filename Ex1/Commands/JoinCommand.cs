using System;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.IO;

namespace Ex1.Commands
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
            MultiPlayerDS mpJoin;
            try
            {
                mpJoin = this.model.join(name);
                mpJoin.JoinGameClient = client;
                mpJoin.IsAvilble = false;
                NetworkStream stream = mpJoin.StartGameClient.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(mpJoin.MazeInit.ToJSON());
                writer.Flush();
                stream = client.GetStream();
                writer = new StreamWriter(stream);
                writer.WriteLine(mpJoin.MazeInit.ToJSON());
                writer.Flush();
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
            return JsonConvert.SerializeObject(joinPacketStream);
        }
    }
}
