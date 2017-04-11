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
            string playJson = Newtonsoft.Json.JsonConvert.SerializeObject(directionBack);
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
                writer.Write(playJson);
            return "1";
        }
    }
}
