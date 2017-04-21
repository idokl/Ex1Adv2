using System.Collections.Generic;
using System.Net.Sockets;
using Ex1.Controller.Commands;
using Ex1.Model;

namespace Ex1.Controller
{
    internal class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;

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

        public void ExecuteCommand(string commandLine, TcpClient client)
        {
            var parser = new CommandParser(commandLine);
            SinglePlayerGame sp;
            if (!commands.ContainsKey(parser.CommandKey))
            {
                sp = new SinglePlayerGame(client, "The command " + parser.CommandKey + " isn't known");
                sp.SendMassage();
                // return false;
            }
            else
            {
                var command = commands[parser.CommandKey];
                var packet = command.Execute(parser.Args, client);

                var result = packet.StringStream;
                if (!packet.MultiPlayer)
                {
                    sp = new SinglePlayerGame(client, result);
                    sp.SendMassage();
                }
                //  return true;
            }
        }
    }
}