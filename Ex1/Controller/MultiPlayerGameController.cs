using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using CommunicationSettings;
using Ex1.Controller;
using Ex1.Controller.Commands;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Ex1.Model
{
    internal class MultiPlayerGameController : IController
    {
        private readonly Dictionary<string, ICommand> commands;

        private readonly bool IamHostClient;
        private IModel model;
        private readonly MultiPlayerDS multiPlayerDs;
        //private PacketStream packet;
        private BinaryReader Reader;
        private NetworkStream stream;
        private BinaryWriter Writer;

        public MultiPlayerGameController(MultiPlayerDS multiPlayerDs, bool amITheHostClient)
        {
            this.multiPlayerDs = multiPlayerDs;
            IamHostClient = amITheHostClient;

            //dictionary with the possible commands of multiplayer mode:
            commands = new Dictionary<string, ICommand>();
            commands.Add("play", new PlayCommand(multiPlayerDs, model));
            commands.Add("close", new CloseCommand(multiPlayerDs, model));
        }

        public void ExecuteCommand(string commandLine, TcpClient client)
        {
            //stream = client.GetStream();
            //Writer = new BinaryWriter(stream);
            var parser = new CommandParser(commandLine);
            if (!commands.ContainsKey(parser.CommandKey))
                Writer.Write("The command " + parser.CommandKey + " isn't known in a multiplayer game.");
            else
            {
                var command = commands[parser.CommandKey];
                command.Execute(parser.Args, client);
            }
        }

        public void SetModel(IModel model)
        {
            this.model = model;
        }

        public void Initialize()
        {
            if (IamHostClient)
                WaitToOpponent();
            //initialize Writer and reader and subscribe to events of changes in the Multiplayer-Data-Structure:
            multiPlayerDs.SomebodyClosedTheGameEvent += DSbecameClosed;
            if (IamHostClient)
            {
                stream = multiPlayerDs.HostClient.GetStream();
                multiPlayerDs.GuestPlayedEvent += OpponentPlayed;
            }
            else
            {
                stream = multiPlayerDs.GuestClient.GetStream();
                multiPlayerDs.HostPlayActionOccurd += OpponentPlayed;
            }
            Reader = new BinaryReader(stream);
            Writer = new BinaryWriter(stream);
            Writer.Write(Messages.PassToMultiplayerMassage);
        }

        private void WaitToOpponent()
        {
            while (multiPlayerDs.GuestClient == null)
                Thread.Sleep(100);
        }

        public void ManageCommunication()
        {
            Writer.Write(multiPlayerDs.MazeInit.ToJSON());

            //subscribe to events of changes in the Multiplayer-Data-Structure:
            //multiPlayerDs.SomebodyClosedTheGame += DSbecameClosed;
            //multiPlayerDs.HostPlayActionOccurd += HostPlay;
            //multiPlayerDs.GuestPlayActionOccurd += GuestPlay;

            var client = IamHostClient ? multiPlayerDs.HostClient : multiPlayerDs.GuestClient;
            //handle communication with our client (recieve commands and execute them):
            while (!multiPlayerDs.Closed)
                try
                {
                    var commandFromTheClient = Reader.ReadString();
                    Console.WriteLine("debug massage: commandFromTheClient = {0}", commandFromTheClient);
                    ExecuteCommand(commandFromTheClient, client);
                }
                catch (Exception)
                {
                    Reader.Dispose();
                }
        }

        // This will be called whenever the list changes.
        private void DSbecameClosed(object sender, EventArgs e)
        {
            if (multiPlayerDs.Closed)
            {
                Console.WriteLine(
                    "debug massage: This function is called when the MultiPlayedDS.Closed is changed to be true.");
                Console.WriteLine("debug massage: We will pass massage about it to our client.");
                Writer.Write(Messages.PassToSingleplayerMassage);
            }
        }

        private void OpponentPlayed(/*TcpClient guestClient,*/ Direction direction)
        {
            var s = PlayToJSON(direction);
            //stream = guestClient.GetStream();
            //Writer = new BinaryWriter(stream);
            Writer.Write(s);
        }

        /*
        private void GuestPlay(//TcpClient hostClient,
                                Direction direction)
        {
            var s = PlayToJSON(direction);
            //stream = hostClient.GetStream();
            //Writer = new BinaryWriter(stream);
            Writer.Write(s);
        }
        */

        private string PlayToJSON(Direction direction)
        {
            var playJObject = new JObject
            {
                ["Name"] = multiPlayerDs.NameOfGame,
                ["Direction"] = direction.ToString()
            };

            return playJObject.ToString();
        }
    }
}