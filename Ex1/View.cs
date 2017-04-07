using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class View : IView
    {
        
        private int port; 
        private TcpListener listener;
        private IClientHandler ch;

        public View(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }
        public void Start()
        {
            //definition of communication channels:
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();

            Task acceptingClients = new Task(() =>
            {
                //accept clients:
                while (true)
                {
                    int counterOfClients = 0;
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("debug massage: Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                    counterOfClients++;
                    Console.WriteLine("debug massage: counterOfClients: " + counterOfClients);
                }
                Console.WriteLine("debug massage: Server stopped");
            });
            acceptingClients.Start();
        }

        public void Stop()
        {
            listener.Stop();
        }
    }
}
    
