
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Ex1
{
    class Program
    {
        //The main:
        static void Main(string[] args)
        {
            Console.WriteLine("debug massage: I am the Server.");
            IController controller = new Controller();
            IClientHandler clientHandler = new ClientHandler(controller);
            IView view = new View(8000, clientHandler);
            view.Start();

            //terminating:
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("press any key to terminate this server...");
            Console.ReadLine();
            view.Stop();

            // CompareSolvers.Run();
        }
    }


}
