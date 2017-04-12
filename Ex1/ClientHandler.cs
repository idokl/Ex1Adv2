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
                while (true)
                {
                    {
                        string commandLine = reader.ReadString();
                        Console.WriteLine("debug massage: Got command: {0}", commandLine);
                        if (commandLine == "terminate")
                            break;
                        char[] separator = { ' ' };
                        string[] words = commandLine.Split(separator);
                        
                       PacketStream packet = Newtonsoft.Json.JsonConvert.DeserializeObject< PacketStream>(Controller.ExecuteCommand(commandLine, client));

                     String result = packet.StringStream;
                        if (packet.MultiPlayer == true)
                        {
                            
                        }
                        writer.Write(result);
                    }
                }
            client.Close();
        });
            t.Start();

        }
    }
}

