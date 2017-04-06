using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientForDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting 0.5 second to the server");
            System.Threading.Thread.Sleep(500);
            Communication communication = new Communication();
            communication.Communicate();
        }
    }
}
