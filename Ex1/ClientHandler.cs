using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                string commandLine = reader.ReadString();
                Console.WriteLine("debug massage: Got command: {0}", commandLine);
                char[] separator = { ' ' };
                string[] words = commandLine.Split(separator);

                string result = Controller.ExecuteCommand(commandLine, client);
                writer.Write(result);
            }
            client.Close();
        });
            t.Start();

        }
    }
}

