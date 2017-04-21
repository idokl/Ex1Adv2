using System.Net.Sockets;
using Ex1.Model;
using System;

namespace Ex1.Controller.Commands
{
    class CloseCommand : ICommand
    {
        MultiPlayerDS multiPlayerDS;
        private IModel model;
        public CloseCommand(MultiPlayerDS multiPlayerDS, IModel model)
        {
            this.multiPlayerDS = multiPlayerDS;
            this.model = model;
        }
        public PacketStream Execute(string[] args, TcpClient client)
        {
            this.multiPlayerDS.Close();
            Console.WriteLine("debug massage: waiting 0.3 second");
            System.Threading.Thread.Sleep(300);

            PacketStream closePacketStream = new PacketStream
            {
                MultiPlayer = false,
                StringStream = ""
            };
            return closePacketStream;
        }
    }
}
