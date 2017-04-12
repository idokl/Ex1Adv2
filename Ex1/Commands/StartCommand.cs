using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Ex1
{
    class StartCommand : ICommand
    {
        private IModel model;
        public StartCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.start(name, rows, cols);
           
            model.ClientListForMultiplayerGames.Add(client, null); 
            PacketStream startPacketStream = new PacketStream
            {
                MultiPlayer = true,
                StringStream = maze.ToString()
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(startPacketStream);
        }
    }
}
