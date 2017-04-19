using System.Net.Sockets;
using Ex1.Model;

namespace Ex1.Controller.Commands
{
    class StartCommand : ICommand
    {
        private IModel model;
        private string Name { get; set; }
        public StartCommand(IModel model)
        {
            this.model = model;
        }
        public PacketStream Execute(string[] args, TcpClient client)
        {
            this.Name = args[0];
            var rows = int.Parse(args[1]);
            var cols = int.Parse(args[2]);
           // var maze =
            var mpStart = this.model.start(this.Name, rows, cols, client);
           // new MultiPlayerDS(client, this.Name, maze);

            //this.model.DictionaryOfMultyPlayerDS.Add(this.Name,mpStart);
            var startPacketStream = new PacketStream
            {
                MultiPlayer = true,
                MultiPlayerDs = mpStart,
                StringStream = ""
            };

            MultiPlayerGameController mpgStart = new MultiPlayerGameController(this.model, mpStart, true);
            mpgStart.Initialize();
            mpgStart.ManageCommunication();
            /*
            //Task keepListenningToClientCommands = new Task(() =>
            //{
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            //BinaryWriter writer = new BinaryWriter(stream);
            {
                mpStart.Start(stream);
                //writer.Write("mp");

                while (!mpStart.Closed)
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
                            mpStart.IsAvilble = false;
                            break;
                        }
                        //PacketStream packet = command.Execute(mpCommandArgs, client);

                        //string result = packet.StringStream;
                    }
                }
            }
            //}
            //);
            //keepListenningToClientCommands.Start();
            */

            return startPacketStream;
        }
    }
}
