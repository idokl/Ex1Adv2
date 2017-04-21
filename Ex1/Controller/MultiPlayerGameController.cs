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

        private bool IamHostClient;
        private MultiPlayerDS multiPlayerDs;
        private NetworkStream stream;
        private BinaryReader Reader;
        private BinaryWriter Writer;
        private PacketStream packet;

        public MultiPlayerGameController(MultiPlayerDS multiPlayerDs, bool amITheStartClient)
        {
            this.multiPlayerDs = multiPlayerDs;
            IamHostClient = amITheStartClient;
            
            //dictionary with the possible commands of multiplayer mode:
            commands = new Dictionary<string, ICommand>();
            commands.Add("play", new PlayCommand(multiPlayerDs, model));
            commands.Add("close", new CloseCommand(multiPlayerDs, model));
        }

        public void Initialize()
        {
            if (IamHostClient)
                this.WaitToOpponent();
            if (IamHostClient)
                stream = multiPlayerDs.HostClient.GetStream();
            else
                stream = multiPlayerDs.GuestClient.GetStream();
            Reader = new BinaryReader(stream);
            Writer = new BinaryWriter(stream);
            Writer.Write(Messages.PassToMultiplayerMassage);
        }

        private void WaitToOpponent()
        {
            while(multiPlayerDs.GuestClient == null)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        public void ManageCommunication()
        {
            //subscribe to events of changes in the Multiplayer-Data-Structure:
            multiPlayerDs.SomebodyClosedTheGame += DSbecameClosed;
            multiPlayerDs.HostPlayActionOccurd += HostPlay;
            multiPlayerDs.GuestPlayActionOccurd += GuestPlay;

            TcpClient client = IamHostClient ? multiPlayerDs.HostClient : multiPlayerDs.GuestClient;
            //handle communication with our client (recieve commands and execute them):
            while (!multiPlayerDs.Closed)
            {
                try
                {
                    string commandFromTheClient = Reader.ReadString();
                    Console.WriteLine("debug massage: commandFromTheClient = {0}", commandFromTheClient);
                    this.ExecuteCommand(commandFromTheClient, client);
                }
                catch (Exception)
                {
                    //Reader.Dispose();
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
                            }
        }

        private void HostPlay(TcpClient guestClient)
        {
            
            stream = guestClient.GetStream();
            Writer = new BinaryWriter(stream);
            Console.WriteLine(packet.StringStream);
            Writer.Write("testFromHost");
        }

        private void GuestPlay(TcpClient hostClient)
        {
            stream = hostClient.GetStream();
            Writer = new BinaryWriter(stream);
            Console.WriteLine(packet.StringStream);
            Writer.Write("testFromGuest");
          
        }

        public void ExecuteCommand(string commandLine, TcpClient client)
        {
            stream = client.GetStream();
            Writer = new BinaryWriter(stream);
            
            CommandParser parser = new CommandParser(commandLine);
            if (!commands.ContainsKey(parser.CommandKey))
            {
                Writer.Write("The command " + parser.CommandKey + " isn't known in a multiplayer game.");
            }
            ICommand command = commands[parser.CommandKey];
            packet = command.Execute(parser.Args, client);
        }

        public void SetModel(IModel model)
        {
            this.model = model;
        }
    }
}