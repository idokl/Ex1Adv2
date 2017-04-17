using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Ex1
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
                bool stop = false;
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                    while (true)
                    {
                        {
                            string commandLine = reader.ReadString();
                            Console.WriteLine("debug massage: Got command: {0}", commandLine);
                            Controller.ExecuteCommand(commandLine, client);
                        }
                    }
            });
            t.Start();

        }
    }
}

