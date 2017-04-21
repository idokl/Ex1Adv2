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
            this.Name = this.multiPlayerDS.NameOfGame;
        }
        public PacketStream Execute(string[] args, TcpClient client)
        {

            this.Direction = (Direction)Enum.Parse(typeof(Direction),args[0]);
           if(multiPlayerDS.HostClient == client)
            {
                multiPlayerDS.HostCurrentDirection = this.Direction;
            }
            else
            {
                multiPlayerDS.GuestCurrentDirection = this.Direction;
            }

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
