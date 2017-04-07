
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;

namespace Ex1
{
    class Program
    {
        //The main:
        static void Main(string[] args)
        {
            IController controller = new Controller();
            IClientHandler clientHandler = new ClientHandler(controller);
            IView view = new View(8000, clientHandler);
            view.Start();


          // CompareSolvers.Run();
        }
    }

  
}
