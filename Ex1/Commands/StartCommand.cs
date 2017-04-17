﻿using System.Net.Sockets;
using Newtonsoft.Json;

namespace Ex1.Commands
{
    class StartCommand : ICommand
    {
        private IModel model;
        private string Name { get; set; }
        public StartCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            this.Name = args[0];
            var rows = int.Parse(args[1]);
            var cols = int.Parse(args[2]);
            var mpStart = new MultiPlayerDS
            {
                StartGameClient = client,
                JoinGameClient = null,
                NameOfGame = this.Name,
                IsAvilble = true
            };
            var maze = this.model.start(this.Name, rows, cols, mpStart);
            
            this.model.DictionaryOfMultyPlayerDS.Add(this.Name,mpStart);
            var startPacketStream = new PacketStream
            {
                MultiPlayer = true,
                MultiPlayerDs = mpStart,
                StringStream = ""
            };
            return JsonConvert.SerializeObject(startPacketStream);
        }
    }
}
