using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ClientForDebug
{
    class Communication
    {
        public void Communicate()
        {
            String command = "";
            bool commandIsReadyToBeSent = false;
            while (true)
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
                    if (!commandIsReadyToBeSent)
                    {
                        Console.Write("Please enter a command: ");
                        //command = "debug arg0 arg1 arg2";
                        //command = "generate mazeName 10 10";
                        command = Console.ReadLine();
                        if (command == "terminate")
                            break;
                    }
                    writer.Write(command);
                    commandIsReadyToBeSent = false;
                    
                    // Get result from server
                    string result = reader.ReadString();
                    Console.WriteLine("debug massage: Result = {0}", result);

                    if (result == "pass to multiplayer mode")
                    {
                        bool stop = false;
                        Task readUpdates = new Task(() =>
                        {
                            while (!stop)
                            {
                                string update = reader.ReadString();
                                Console.WriteLine("Got update: {0}", update);
                                if (update == "hello client, we noticed that your multiplayer game is closed now")
                                    stop = true;
                            }
                        });
                        readUpdates.Start();

                        while (!stop)
                        {
                            if (!commandIsReadyToBeSent)
                            {
                                Console.Write("You are in multiplayer mode. Please enter a command: ");
                                command = Console.ReadLine();
                                commandIsReadyToBeSent = true;
                            }
                            if (!stop)
                            {
                                if (commandIsReadyToBeSent)
                                {
                                    writer.Write(command);
                                    commandIsReadyToBeSent = false;
                                }
                            }
                        }
                        stop = true;
                    }

                }
                client.Close();
            }
           
        }
    }
}
