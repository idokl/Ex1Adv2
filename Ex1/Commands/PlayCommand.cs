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
    class PlayCommand : ICommand
    {
        private IModel model;
        public PlayCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {

            Direction direction = (Direction)Enum.Parse(typeof(Direction),args[0]);
            Direction directionBack = model.play(direction);
            PacketStream playPacketStream = new PacketStream(true, directionBack.ToString());
            return Newtonsoft.Json.JsonConvert.SerializeObject(playPacketStream);
        }
    }
}
