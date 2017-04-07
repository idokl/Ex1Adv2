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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("debug massage: I am the Client.");
            Console.WriteLine("Waiting 0.5 second to the server");
            System.Threading.Thread.Sleep(500);
            Communication communication = new Communication();
            communication.Communicate();

            System.Threading.Thread.Sleep(2000);
        }
    }
}
