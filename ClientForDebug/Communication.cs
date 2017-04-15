﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ClientForDebug
{
    class Communication
    {
        public void Communicate()
        {
            bool multiplayer = false;
            while (!multiplayer)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
                TcpClient client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("debug massage: You are connected");
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
                        break;

                    // Get result from server
                    string result = reader.ReadString();

                    Console.WriteLine("debug massage: Result = {0}", result);

                }
                client.Close();
            }
           
        }
    }
}