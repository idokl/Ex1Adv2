using System;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Ex1.Commands
{
    class JoinCommand : ICommand
    {
        private IModel model;
        public JoinCommand(IModel model)
        {
            this.model = model;
        }
        public PacketStream Execute(string[] args, TcpClient client)
        {
            PacketStream joinPacketStream = new PacketStream
            {
                MultiPlayer = true,
                StringStream = ""
            };

            string name = args[0];
            MultiPlayerDS mpJoin;
            try
            {
                mpJoin = this.model.join(name);
                mpJoin.JoinGameClient = client;
                mpJoin.IsAvailable = false;

                MultiPlayerGame mpgJoin = new MultiPlayerGame(mpJoin, false);
                mpgJoin.Initialize();
                mpgJoin.ManageCommunication();

                /*
                //
                //Task keepListenningToClientCommands = new Task(() =>
                //{
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);
                {
                    mpJoin.Start(stream);
                    //writer.Write("mp");

                    while (!mpJoin.Closed)
                    {
                        {
                            string commandLine = reader.ReadString();
                            //Console.WriteLine("debug massage: Got command: {0}", commandLine);
                            string[] arr = commandLine.Split(' ');
                            string commandKey = arr[0];
                            // if (!commands.ContainsKey(commandKey))
                            //     throw NotImplementedException;
                            string[] mpCommandArgs = arr.Skip(1).ToArray();
                            if (commandKey == "play")
                            {

                            }
                            else if (commandKey == "close")
                            {
                                mpJoin.IsAvilble = false;
                                break;
                            }
                            //PacketStream packet = command.Execute(mpCommandArgs, client);

                            //string result = packet.StringStream;
                        }
                    }
                    //});
                    //keepListenningToClientCommands.Start();
                    //
                }
                */
            }
            catch
            {
                Console.WriteLine("the name of game to join isn't exist");
            }
           
            return joinPacketStream;
        }
    }
}
