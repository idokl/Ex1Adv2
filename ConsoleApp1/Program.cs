using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericState<int> a = GenericState<int>.StatePool.GetState(3);
            a.Cost = 8;
            GenericState<int> b = GenericState<int>.StatePool.GetState(4);
            b.Cost = 9;
            GenericState<int> c = GenericState<int>.StatePool.GetState(5);
            Console.WriteLine("cost of a: " + GenericState<int>.StatePool.GetState(3).Cost);
            Console.WriteLine("cost of b: " + GenericState<int>.StatePool.GetState(4).Cost);
            Console.WriteLine("cost of c: " + GenericState<int>.StatePool.GetState(5).Cost);
        }
    }
}
