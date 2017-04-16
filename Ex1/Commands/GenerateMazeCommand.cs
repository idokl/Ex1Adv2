using MazeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ex1.Commands
{
    class GenerateMazeCommand : ICommand
    {
        private IModel model;

        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }

        public PacketStream Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.generate(name, rows, cols);
            PacketStream generatePacketStream = new PacketStream
            {
                StringStream = maze.ToJSON()
            };
            return generatePacketStream;
           
        }
    }
}
