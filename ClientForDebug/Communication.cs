using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CommunicationSettings;

namespace ClientForDebug
{
    internal class Communication
    {
        public void Communicate()
        {
            var command = "";
            var commandIsReadyToBeSent = false;
            while (true)
            {
                var ep = new IPEndPoint(
                    IPAddress.Parse(ConfigurationManager.AppSettings["ip"]), Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
                var client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("debug massage: You are connected");
                using (var stream = client.GetStream())
                using (var reader = new BinaryReader(stream))
                using (var writer = new BinaryWriter(stream))
                {
                    // Send data to server
                    if (!commandIsReadyToBeSent)
                    {
                        Console.Write("Please enter a command: ");
                        //command = "debug arg0 arg1 arg2";
                        //command = "generate mazeName 10 10";
                        command = Console.ReadLine();
                        commandIsReadyToBeSent = true;
                        if (command == "terminate")
                            break;
                    }
                    if (commandIsReadyToBeSent)
                    {
                        writer.Write(command);
                        commandIsReadyToBeSent = false;
                    }

                    // Get result from server
                    var result = reader.ReadString();
                    Console.WriteLine("debug massage: Result = {0}", result);

                    if (result == Messages.PassToMultiplayerMassage)
                    {
                        var stop = false;
                        var readUpdates = new Task(() =>
                        {
                            while (!stop)
                            {
                                var update = reader.ReadString();
                                Console.WriteLine("Got update: {0}", update);
                                if (update == Messages.PassToSingleplayerMassage)
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
                                if (commandIsReadyToBeSent)
                                {
                                    writer.Write(command);
                                    commandIsReadyToBeSent = false;
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