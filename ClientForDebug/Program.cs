using System;
using System.Threading;

namespace ClientForDebug
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("debug massage: I am the Client.");
            Console.WriteLine("Waiting 0.5 second to the server");
            Thread.Sleep(500);
            var communication = new Communication();
            communication.Communicate();

            Thread.Sleep(1000);
        }
    }
}