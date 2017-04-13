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
    class PlayCommand : ICommand
    {
        private IModel model;
        private string Name { get; set; }
        private Direction direction { get; set; }
        public PlayCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            this.direction = (Direction)Enum.Parse(typeof(Direction),args[0]);
            MultiPlayer mpPlay = this.model.play(this.direction, client);

           PacketStream playPacketStream = new PacketStream
            {
                MultiPlayer = true, StringStream = this.ToJSON()
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(playPacketStream);
        }
        public string ToJSON()
        {
            JObject playJObject = new JObject
            {
                ["Name"] = this.Name,
                ["Direction"] = this.direction.ToString(),
            };

            return playJObject.ToString();
        }
    }
}
