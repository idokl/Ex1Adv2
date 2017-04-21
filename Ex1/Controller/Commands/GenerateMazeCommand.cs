using System.Net.Sockets;
using Ex1.Model;

namespace Ex1.Controller.Commands
{
    internal class GenerateMazeCommand : ICommand
    {
        private readonly IModel model;

        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }

        public PacketStream Execute(string[] args, TcpClient client)
        {
            var name = args[0];
            var rows = int.Parse(args[1]);
            var cols = int.Parse(args[2]);
            var maze = model.generate(name, rows, cols);
            var generatePacketStream = new PacketStream
            {
                StringStream = maze.ToJSON()
            };
            return generatePacketStream;
        }
    }
}