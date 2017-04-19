using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Ex1.Controller.Commands;
using Ex1.Model;
using Ex1.View;

namespace Ex1.Controller
{
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;

        public Controller() { }

        public void SetModel(IModel model)
        {
            this.model = model;
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveCommand(model));
            commands.Add("start", new StartCommand(model));
            commands.Add("list", new ListCommand(model));
            commands.Add("join", new JoinCommand(model));
        }

        public bool ExecuteCommand(string commandLine, TcpClient client)
        {
            CommandParser parser = new CommandParser(commandLine);
            if (!commands.ContainsKey(parser.CommandKey))
            {
                SinglePlayerGame sp = new SinglePlayerGame(client, "The command " + parser.CommandKey + " isn't known");
                sp.SendMassage();
                return false;
            }
            else
            {
                ICommand command = commands[parser.CommandKey];
                PacketStream packet = command.Execute(parser.Args, client);

                string result = packet.StringStream;
                if (!packet.MultiPlayer)
                {
                    SinglePlayerGame sp = new SinglePlayerGame(client, result);
                    sp.SendMassage();
                }
                return true;
            }
        }
    }
}
