using System;
using System.Net.Sockets;
using Ex1.Model;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Ex1.Controller.Commands
{
    class PlayCommand : ICommand
    {
        private IModel model;
        private MultiPlayerDS multiPlayerDS;
        private string Name { get; set; }
        private Direction Direction { get; set; }
        public PlayCommand(MultiPlayerDS multiPlayerDS, IModel model)
        {
            this.multiPlayerDS = multiPlayerDS;
            this.model = model;
        }
        public PacketStream Execute(string[] args, TcpClient client)
        {

            //this.Direction = (Direction)Enum.Parse(typeof(Direction),args[0]);
            //MultiPlayerDS mpPlay = this.model.play(this.Direction, client);
            //mpPlay.CurrentDirection = Direction;

            /* test! : */ multiPlayerDS.CurrentDirection = Direction.Down;
            //... here we can to call to model.play(direction, multiplayeDS, client)

            PacketStream playPacketStream = new PacketStream
            {
                MultiPlayer = true, StringStream = this.ToJSON()
            };

            return playPacketStream;
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
