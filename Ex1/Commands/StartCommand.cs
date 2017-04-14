using System.Net.Sockets;
using Newtonsoft.Json;

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
        public string Execute(string[] args, TcpClient client)
        {
            this.Name = args[0];
            var rows = int.Parse(args[1]);
            var cols = int.Parse(args[2]);
            var maze = this.model.start(this.Name, rows, cols);
            var mpStart = new MultiPlayer
            {
                StartGameClient = client,
                JoinGameClient = null,
                MazeInit = maze,
                NameOfGame = this.Name,
                IsAvilble = true
            };
            this.model.MultyPlayerList.Add(mpStart);
            var startPacketStream = new PacketStream
            {
                MultiPlayer = true,
                StringStream = ""
            };
            return JsonConvert.SerializeObject(startPacketStream);
        }
    }
}
