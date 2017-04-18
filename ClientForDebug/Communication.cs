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
            //bool multiplayer = false;
            //while (!multiplayer)
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


                    if (result == "pass to multiplayer mode")
                    {
                        //this.ReadAndWrite(reader, writer);
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
                            Console.Write("You are in multiplayer mode. Please enter a command: ");
                            String command2;
                            command2 = Console.ReadLine();
                            try
                            {
                                writer.Write(command2);
                            }
                            catch (SocketException)
                            {
                                break;
                            }
                            /*
                            if (command2 == "terminate")
                                break;
                            string result2 = reader.ReadString();
                            Console.WriteLine("debug massage: Result = {0}", result2);
                            */
                        }
                        stop = true;
                    }


                    Console.WriteLine("debug massage: Result = {0}", result);

                }
                client.Close();
            }
           
        }

/*
        private void ReadAndWrite(BinaryReader reader, BinaryWriter writer)
        {
            bool stop = false;
            Task readUpdates = new Task(() =>
            {
                while (!stop)
                {

                    string update = reader.ReadString();
                    Console.WriteLine("Got update: {0}", update);
                    stop = false;
                }
            });
            readUpdates.Start();
            while (!stop)
            {
                Console.Write("You are in multiplayer mode. Please enter a command: ");
                String command2;
                command2 = Console.ReadLine();
                writer.Write(command2);
                if (command2 == "terminate")
                    break;
                string result2 = reader.ReadString();
                Console.WriteLine("debug massage: Result = {0}", result2);
            }
        }
*/


    }
}
