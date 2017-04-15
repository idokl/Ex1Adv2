using System;
using System.Net.Sockets;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Ex1.Commands
{
    class PlayCommand : ICommand
    {
        private IModel model;
        private string Name { get; set; }
        private Direction Direction { get; set; }
        public PlayCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            this.Direction = (Direction)Enum.Parse(typeof(Direction),args[0]);
            MultiPlayerDS mpPlay = this.model.play(this.Direction, client);
            mpPlay.CurrentDirection = Direction;
           PacketStream playPacketStream = new PacketStream
            {
                MultiPlayer = true, StringStream = this.ToJSON()
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(playPacketStream);
        }
        private string ToJSON()
        {
            JObject playJObject = new JObject
            {
                ["Name"] = this.Name,
                ["Direction"] = this.Direction.ToString(),
            };

            return playJObject.ToString();
        }
    }
}
