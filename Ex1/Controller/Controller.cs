using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Ex1.Controller.Commands;
using Ex1.Model;

namespace Ex1.Controller
{
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        public Controller(IModel mazeModel)

        {
            model = mazeModel;
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveCommand(model));
            commands.Add("start", new StartCommand(model));
            commands.Add("list", new ListCommand(model));
            commands.Add("join", new JoinCommand(model));
            commands.Add("play", new PlayCommand(model));
            commands.Add("close", new CloseCommand(model));
        }
        public bool ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
           /* if (!commands.ContainsKey(commandKey))
                throw NotImplementedException;*/
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];

            //string commandResult = command.Execute(args, client);
            //PacketStream packet = Newtonsoft.Json.JsonConvert.DeserializeObject<PacketStream>(commandResult);
            PacketStream packet = command.Execute(args, client);

            string result = packet.StringStream;
            //MultiPlayerGame mp;
            SinglePlayerGame sp;
            if (packet.MultiPlayer)
            {

                if (command is PlayCommand)
                {

                }


                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);
                {
                    commandLine = reader.ReadString();
                    Console.WriteLine("debug massage: Got command: {0}", commandLine);
                    this.ExecuteCommand(commandLine, client);
                }


                    //mp = new MultiPlayerGame(packet.MultiPlayerDs);
                    //mp.Play();
                    return false;
            }
            else
            {
                sp = new SinglePlayerGame(client, result);
                sp.Play();
                return true;
            }
        }
    }
}
