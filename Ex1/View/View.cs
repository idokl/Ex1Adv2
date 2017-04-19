using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ex1.Controller;
using System.Configuration;

namespace Ex1.View
{
    class View : IView
    {

        private int port;
        private TcpListener listener;
        private IController MazeController;


        public View(int port, IController controller)
        {
            this.port = port;
            this.MazeController = controller;
        }

        //public void Start(IController controller)
        public void Start()
        {
            IClientHandler ch = new ClientHandler(MazeController);
            //definition of communication channels:
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ip"]), port);
            listener = new TcpListener(ep);
            listener.Start();

            Task acceptingClients = new Task(() =>
            {
                int counterOfClients = 0;
                //accept clients:
                while (true)
                {
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

