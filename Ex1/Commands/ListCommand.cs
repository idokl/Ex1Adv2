﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Ex1.Commands
{
    class ListCommand : ICommand
    {
        private IModel model;
        public ListCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            List<String> listOfGames =  model.list();
            StringBuilder listOfGameStringBuilder = new StringBuilder(" ");
            foreach (String name in listOfGames)
            {
                String nameFormat = String.Format("{0},", name);
                listOfGameStringBuilder.AppendLine(nameFormat);
            }
            listOfGameStringBuilder.Length -= 1;
           
            PacketStream ListPacketStream = new PacketStream
            {
                StringStream = listOfGameStringBuilder.ToString()
            };
            return JsonConvert.SerializeObject(ListPacketStream);
        }
    }
}
