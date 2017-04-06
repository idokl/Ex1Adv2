using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientForDebug
{
    class Communication
    {    
        public void Communicate()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000); 
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.Write("Please enter a number: ");
                String command = Console.ReadLine();
                //int num = int.Parse(Console.ReadLine());
                writer.Write(command);
                // Get result from server
                int result = reader.ReadInt32();
                Console.WriteLine("Result = {0}", result);
            }
            client.Close();
        }
    }
}

