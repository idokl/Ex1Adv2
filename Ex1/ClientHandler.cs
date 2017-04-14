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
                        //char[] separator = { ' ' };
                        //string[] words = commandLine.Split(separator);

                        string commandResult = Controller.ExecuteCommand(commandLine, client);
                       PacketStream packet = Newtonsoft.Json.JsonConvert.DeserializeObject< PacketStream>(commandResult);

                     String result = packet.StringStream;
                        if (packet.MultiPlayer)
                        {
                            writer.Write(commandResult);
                        }
                        else
                        {
                            writer.Write(result);
                        }
                        
                    }
                }
            client.Close();
        });
            t.Start();

        }
    }
}

