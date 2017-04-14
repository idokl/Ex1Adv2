using System;

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

            System.Threading.Thread.Sleep(1000);
        }
    }
}
