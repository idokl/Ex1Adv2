using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Ex1.Controller
{
    internal class ClientHandler : IClientHandler
    {
        private readonly IController Controller;

        public ClientHandler(IController controller)
        {
            Controller = controller;
        }

        public void HandleClient(TcpClient client)
        {
            var t = new Task(() =>
            {
                var stream = client.GetStream();
                var reader = new BinaryReader(stream);
                var writer = new BinaryWriter(stream);
                {
                    var commandLine = reader.ReadString();
                    Console.WriteLine("debug massage: Got command: {0}", commandLine);
                    Controller.ExecuteCommand(commandLine, client);
                }
            });
            t.Start();
        }
    }
}