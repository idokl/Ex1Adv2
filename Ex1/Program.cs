using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            Maze MyMaze = new Maze(3, 4);
            string s = MyMaze.ToString();
            Console.WriteLine(s);
            SearchableMaze SM = new SearchableMaze();
            s = SM.MyMaze.ToString();
            Console.WriteLine(s);
            BestFirstSearch<PointState> BFS = new BestFirstSearch<PointState>();
            Solution solution = BFS.search(SM);
            LinkedList<State> path = solution.Path;
            Console.WriteLine("The solution path is:");
            foreach (State state in path)
            {
                Console.WriteLine(state.ToString());
            }
            Console.WriteLine();


            SearchAlgorithmsLib.PriorityQueue<int> PQ = new PriorityQueue<int>();
            PQ.push(2);
            PQ.push(4);
            PQ.push(3);
            PQ.push(5);
            PQ.push(1);
            PQ.push(7);
            PQ.push(6);
            PQ.push(0);
            PQ.push(9);
            PQ.push(8);
            Console.WriteLine("does contain? " + PQ.DoesContain(5));
            //PQ.DecreaseKey(3, -1);
            //PQ.AddElementOrTryToDecreaseItsKey(3);
            int a1 = PQ.pop();
            int a2 = PQ.pop();
            int a3 = PQ.pop();
            int a4 = PQ.pop();
            int a5 = PQ.pop();
            int a6 = PQ.pop();
            int a7 = PQ.pop();
            int a8 = PQ.pop();
            int a9 = PQ.pop();
            int a10 = PQ.pop();
            Console.WriteLine($"{a1} {a2} {a3} {a4} {a5} {a6} {a7} {a8} {a9} {a10}");

            State a, b, goal;
            a = new StringState("A");
            b = new StringState("B");
            goal = new StringState("B");
            Console.WriteLine(b.Equals(goal));
            Console.ReadLine();
        }
    }
}
