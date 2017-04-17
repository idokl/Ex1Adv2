using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Ex1.Commands;

namespace Ex1
{
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        public Controller(IModel model)
        {
            this.model = model;
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(this.model));
            commands.Add("solve", new SolveCommand(this.model));
            commands.Add("start", new StartCommand(this.model));
            commands.Add("list", new ListCommand(this.model));
            commands.Add("join", new JoinCommand(this.model));
            commands.Add("play", new PlayCommand(this.model));
            commands.Add("close", new CloseCommand(this.model));
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
