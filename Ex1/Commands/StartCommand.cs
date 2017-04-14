using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Ex1
{
    class StartCommand : ICommand
    {
        private IModel model;
        public string Name { get; set; }
        public StartCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            this.Name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = this.model.start(this.Name, rows, cols);
            MultiPlayer mpStart = new MultiPlayer
            {
                StartGameClient = client,
                JoinGameClient = null,
                MazeInit = maze,
                NameOfGame = this.Name
              //  IsAvilble = true
            };
            this.model.MultyPlayerList.Add(mpStart);
            PacketStream startPacketStream = new PacketStream
            {
                MultiPlayer = true,
                StringStream = maze.ToJSON()
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(startPacketStream);
        }
    }
}
