using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ex1.Controller;

namespace Ex1.View
{
    internal class View : IView
    {
        private TcpListener listener;
        private readonly IController MazeController;

        private readonly int port;


        public View(int port, IController controller)
        {
            this.port = port;
            MazeController = controller;
        }

        //public void Start(IController controller)
        public void Start()
        {
            IClientHandler ch = new ClientHandler(MazeController);
            //definition of communication channels:
            var ep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ip"]), port);
            listener = new TcpListener(ep);
            listener.Start();

            var acceptingClients = new Task(() =>
            {
                var counterOfClients = 0;
                //accept clients:
                while (true)
                {
                    try
                    {
                        var client = listener.AcceptTcpClient();
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