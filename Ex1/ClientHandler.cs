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
            new Task(() => 
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    Console.WriteLine("Got command: {0}", commandLine);
                    char[] separator = { ' ' };
                    string[] words = commandLine.Split(separator);
                    Console.WriteLine("arg0: {0}", words[0]);
                    Console.WriteLine("arg1: {0}", words[1]);
                    string result = Controller.ExecuteCommand(commandLine, client);
                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }
/*
        public void manageCommunicationWithClients()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpListener listener = new TcpListener(ep);
            //listener.Start();
            Console.WriteLine("Waiting for client connections...");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected");
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                Console.WriteLine("Waiting for a request");
                string request = reader.ReadString();
                Console.WriteLine("Request accepted");

                
                //                Console.WriteLine("Waiting for a number");
                //                int num = reader.ReadInt32();
                //                Console.WriteLine("Number accepted");
                //                num *= 2;
                //                writer.Write(num);
                //
            }
            client.Close();
            listener.Stop();
        }
*/
    }
}

