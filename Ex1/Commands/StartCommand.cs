using System.Net.Sockets;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Ex1.Commands
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
            var maze = this.model.start(this.Name, rows, cols);
            var mpStart = new MultiPlayerDS
            {
                StartGameClient = client,
                JoinGameClient = null,
                MazeInit = maze,
                NameOfGame = this.Name,
                IsAvilble = true
            };
            this.model.DictionaryOfMultyPlayerDS.Add(this.Name,mpStart);
            var startPacketStream = new PacketStream
            {
                MultiPlayer = true,
                MultiPlayerDs = mpStart,
                StringStream = ""
            };
            /*
            Task keepListenningToClientCommands = new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                    while (mpStart.IsAvilble)
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
            });
            keepListenningToClientCommands.Start();
            */

            return startPacketStream;
        }
    }
}
