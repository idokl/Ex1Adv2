﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        public Controller()

        {
            model = new Model();
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveCommand(model));
            commands.Add("start", new StartCommand(model));
            commands.Add("list", new ListCommand(model));
            commands.Add("join", new JoinCommand(model));
            commands.Add("play", new PlayCommand(model));
            commands.Add("close", new CloseCommand(model));

            commands.Add("debug", new TestOfCommand(model));
        }
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
           }
    }
}
