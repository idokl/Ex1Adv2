
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Ex1.Controller;
using Ex1.Model;
using Ex1.View;

namespace Ex1
{
    class Program
    {
        //The main:
        static void Main(string[] args)
        {
            Console.WriteLine("debug massage: I am the Server.");
            IController controller = new Controller.Controller();
            IModel model = new Model.Model(/*controller*/); 
            IView view = new View.View(8000, controller);
            controller.SetModel(model);
            view.Start(/*controller*/);

            //terminating:
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("press any key to terminate this server...");
            Console.ReadLine();
            view.Stop();

            //CompareSolvers.Run();
        }
    }


}
