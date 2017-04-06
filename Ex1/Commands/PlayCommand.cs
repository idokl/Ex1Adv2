﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class PlayCommand : ICommand
    {
        private IModel model;
        public PlayCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client = null)
        {

            move move = (move)Enum.Parse(typeof(move),args[0]);
            model.play(move);
            return "play";
        }
    }
}