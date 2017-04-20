using System;
using System.IO;
using System.Net.Sockets;
using MazeLib;
using CommunicationSettings;
using System.Collections.Generic;
using Ex1.Controller;
using Ex1.Controller.Commands;

namespace Ex1.Model
{
    class MultiPlayerGameController: IController
    {
        private IModel model;
        private Dictionary<string, ICommand> commands;

        private bool IamTheStartClient;
        private MultiPlayerDS multiPlayerDs;

        private BinaryReader Reader;
        private BinaryWriter Writer;

        public MultiPlayerGameController(IModel model, MultiPlayerDS multiPlayerDs, bool amITheStartClient)
        {
            this.model = model;
            this.multiPlayerDs = multiPlayerDs;
            IamTheStartClient = amITheStartClient;
            
            //dictionary with the possible commands of multiplayer mode:
            commands = new Dictionary<string, ICommand>();
            commands.Add("play", new PlayCommand(multiPlayerDs, model));
            commands.Add("close", new CloseCommand(multiPlayerDs, model));
        }

        public void Initialize()
        {
            if (IamTheStartClient)
                this.WaitToOpponent();
            NetworkStream stream;
            if (IamTheStartClient)
                stream = multiPlayerDs.StartGameClient.GetStream();
            else
                stream = multiPlayerDs.JoinGameClient.GetStream();
            Reader = new BinaryReader(stream);
            Writer = new BinaryWriter(stream);
            Writer.Write(Messages.PassToMultiplayerMassage);
        }

        private void WaitToOpponent()
        {
            while(multiPlayerDs.JoinGameClient == null)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        public void ManageCommunication()
        {
            //subscribe to events of changes in the Multiplayer-Data-Structure:
            multiPlayerDs.SomebodyClosedTheGame += DSbecameClosed;
            multiPlayerDs.PlayActionOccurd += Play;

            TcpClient client = IamTheStartClient ? multiPlayerDs.StartGameClient : multiPlayerDs.JoinGameClient;
            //handle communication with our client (recieve commands and execute them):
            while (!multiPlayerDs.Closed)
            {
                try
                {
                    string commandFromTheClient = Reader.ReadString();
                    Console.WriteLine("debug massage: commandFromTheClient = {0}", commandFromTheClient);
                    if (multiPlayerDs.Closed)
                    {
                        break;
                    }
                    CommandParser parser = new CommandParser(commandFromTheClient);
                    if (!commands.ContainsKey(parser.CommandKey))
                    {
                        Writer.Write("The command " + parser.CommandKey + " isn't known in a multiplayer game.");
                    }
                    else
                    {
                        ICommand command = commands[parser.CommandKey];
                        PacketStream packet = command.Execute(parser.Args, client);

                        //string result = packet.StringStream;

/*
//This code has to be implemented in the play ICommand!!!!!!!!!!!!!!!!!!! :
                        if (commandFromTheClient == "Play")
                        {
                            multiPlayerDs.CurrentDirection = Direction.Down;

                        }
           
                        // DirectionChangeEventArgs direction = new DirectionChangeEventArgs(Direction.Down);
*/

                    //string answer = "The server recived your command. Your command was: " + commandFromTheClient;
                    //Writer.Write(answer);
                    }
                }
                catch (Exception)
                {
                    Reader.Dispose();
                }
            }

        }

        // This will be called whenever the list changes.
        private void DSbecameClosed(object sender, EventArgs e)
        {
            if (!multiPlayerDs.Closed)
            {
                Console.WriteLine("debug massage: This function is called when the MultiPlayedDS.Closed is changed to be true.");
                Console.WriteLine("debug massage: We will pass massage about it to our client.");
                Writer.Write(Messages.PassToSingleplayerMassage);
                Writer.Dispose();
            }
        }

        private void Play(DirectionChangeEventArgs e)
        {
            Console.WriteLine("This function ");
        }

        public bool ExecuteCommand(string commandLine, TcpClient client)
        {
            throw new NotImplementedException();
        }

        public void SetModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}