using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Ex1.Controller
{
    class ClientHandler : IClientHandler
    {
        private IController Controller;

        public ClientHandler(IController controller)
        {
            Controller = controller;
        }

        public void HandleClient(TcpClient client)
        {
            Task t = new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);
                {
                    string commandLine = reader.ReadString();
                    Console.WriteLine("debug massage: Got command: {0}", commandLine);
                    Controller.ExecuteCommand(commandLine, client);
                }
            });
            t.Start();
        }
    }
}

