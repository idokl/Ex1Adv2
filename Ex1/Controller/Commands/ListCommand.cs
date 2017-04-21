using System.Net.Sockets;
using System.Text;
using Ex1.Model;

namespace Ex1.Controller.Commands
{
    internal class ListCommand : ICommand
    {
        private readonly IModel model;

        public ListCommand(IModel model)
        {
            this.model = model;
        }

        public PacketStream Execute(string[] args, TcpClient client)
        {
            var listOfGames = model.list();
            var listOfGameStringBuilder = new StringBuilder(" ");
            foreach (var name in listOfGames)
            {
                var nameFormat = string.Format("{0},", name);
                listOfGameStringBuilder.AppendLine(nameFormat);
            }
            listOfGameStringBuilder.Length -= 1;

            var ListPacketStream = new PacketStream
            {
                StringStream = listOfGameStringBuilder.ToString()
            };
            return ListPacketStream;
        }
    }
}