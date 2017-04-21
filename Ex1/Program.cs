using System;
using Ex1.Controller;
using Ex1.Model;
using Ex1.View;

namespace Ex1
{
    internal class Program
    {
        //The main:
        private static void Main(string[] args)
        {
            Console.WriteLine("debug massage: I am the Server.");
            IController controller = new Controller.Controller();
            IModel model = new Model.Model();
            IView view = new View.View(8888, controller);
            controller.SetModel(model);
            view.Start();

            ////terminating:
            //System.Threading.Thread.Sleep(500);
            //Console.WriteLine("press any key to terminate this server...");
            //Console.ReadLine();
            view.Stop();

            //CompareSolvers.Run();
        }
    }
}