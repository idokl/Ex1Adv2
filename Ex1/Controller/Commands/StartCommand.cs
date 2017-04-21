using System.Net.Sockets;
using Ex1.Model;

namespace Ex1.Controller.Commands
{
    internal class StartCommand : ICommand
    {
        private readonly IModel model;

        public StartCommand(IModel model)
        {
            this.model = model;
        }

        private string Name { get; set; }

        public PacketStream Execute(string[] args, TcpClient client)
        {
            Name = args[0];
            var rows = int.Parse(args[1]);
            var cols = int.Parse(args[2]);

            var mpStart = model.start(Name, rows, cols, client);

            var startPacketStream = new PacketStream
            {
                MultiPlayer = true,
                MultiPlayerDs = mpStart,
                StringStream = ""
            };

            var mpgStart = new MultiPlayerGameController(mpStart, true);
            mpgStart.SetModel(model);
            mpgStart.Initialize();
            mpgStart.ManageCommunication();
            return startPacketStream;
        }
    }
}