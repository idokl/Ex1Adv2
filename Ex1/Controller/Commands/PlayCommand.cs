using System;
using System.Net.Sockets;
using Ex1.Model;
using MazeLib;

namespace Ex1.Controller.Commands
{
    internal class PlayCommand : ICommand
    {
        private IModel model;
        private readonly MultiPlayerDS multiPlayerDS;

        public PlayCommand(MultiPlayerDS multiPlayerDS, IModel model)
        {
            this.multiPlayerDS = multiPlayerDS;
            this.model = model;
            Name = this.multiPlayerDS.NameOfGame;
        }

        private string Name { get; }
        private Direction Direction { get; set; }

        public PacketStream Execute(string[] args, TcpClient client)
        {
            
            Direction = this.DirectionParser(args[0]);
            if (multiPlayerDS.HostClient == client)
                multiPlayerDS.HostCurrentDirection = Direction;
            else
                multiPlayerDS.GuestCurrentDirection = Direction;

            return new PacketStream();
        }

        public Direction DirectionParser(string direction)
        {
            switch (direction)
            {
                case "up":
                    return Direction.Up;
                case "down":
                    return Direction.Down;
                case "left":
                    return Direction.Left;
                case "right":
                    return Direction.Right;
            }
            return Direction;
        }
    }
}