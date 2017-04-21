using System;
using System.Net.Sockets;
using System.Threading;
using Ex1.Model;

namespace Ex1.Controller.Commands
{
    internal class CloseCommand : ICommand
    {
        private IModel model;
        private readonly MultiPlayerDS multiPlayerDS;

        public CloseCommand(MultiPlayerDS multiPlayerDS, IModel model)
        {
            this.multiPlayerDS = multiPlayerDS;
            this.model = model;
        }

        public PacketStream Execute(string[] args, TcpClient client)
        {
            multiPlayerDS.Close();
            Console.WriteLine("debug massage: waiting 0.3 second");
            Thread.Sleep(300);

            var closePacketStream = new PacketStream
            {
                MultiPlayer = false,
                StringStream = ""
            };
            return closePacketStream;
        }
    }
}