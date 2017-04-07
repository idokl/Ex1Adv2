using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class TestOfCommand : ICommand
    {
        private IModel model;
        public TestOfCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            Console.WriteLine("I am the Server.");
            Console.WriteLine("The client executed the command TestOfCommand.");
            Console.WriteLine("It sended " + args.Length + " arguments");
            Console.WriteLine("The arguments are:");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
            }
            return "Happy and Kosher Pesach";
        }
    }
}
