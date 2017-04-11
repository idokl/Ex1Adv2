using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientForDebug
{
    class Communication
    {
        private bool multiPlayer = false;
        private bool stop = false;
        public void Communicate()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("debug massage: You are connected");
            while (!stop)
            {
                Thread sendThread = new Thread(() =>
                {
                    using (NetworkStream stream = client.GetStream())
                    using (BinaryReader reader = new BinaryReader(stream))
                    using (BinaryWriter writer = new BinaryWriter(stream))

                    {
                        // Send data to server
                        Console.Write("Please enter a command: ");
                        String command;
                        command = Console.ReadLine();
                        //command = "debug arg0 arg1 arg2";
                        //command = "generate mazeName 10 10";
                        writer.Write(command);
                        if (command == "terminate")
                        {
                            client.Close();
                            stop = true;
                        }

                    }
                });
                Thread reciveThread = new Thread(() =>
                {
                    using (NetworkStream stream = client.GetStream())
                    using (BinaryReader reader = new BinaryReader(stream))
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {

                        // Get result from server
                        string result = reader.ReadString();
                        if (Int32.Parse(result) == 1)
                        {
                            multiPlayer = true;
                        }
                        Console.WriteLine("debug massage: Result = {0}", result);

                    }
                   // client.Close();
                });
                sendThread.Start();
                reciveThread.Start();
            }


        }
    }
}

