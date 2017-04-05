using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    class CompareSolvers
    {
       
        public static void Run() { 

            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze MyMaze = dfsMazeGenerator.Generate(20, 20);

            string s = MyMaze.ToString();

            Console.WriteLine(s);
            SearchableMaze SM = new SearchableMaze(MyMaze);

            Console.WriteLine("Start:" + new PointState(MyMaze.InitialPos).ToString());
            Console.WriteLine("Goal:" + new PointState(MyMaze.GoalPos).ToString());


            DepthFirstSearch<PointState> DFS = new DepthFirstSearch<PointState>();
            Solution solutionDFS = DFS.search(SM);

            // LinkedList<State> path = solution.Path;
            Console.WriteLine("The solution path by DFS is:");
            foreach (State state in solutionDFS.Path)
            {
                Console.WriteLine(state.ToString());
            }
            Console.WriteLine();



            BestFirstSearch<PointState> BFS = new BestFirstSearch<PointState>();
            Solution solution = BFS.search(SM);

            // LinkedList<State> path = solution.Path;
            Console.WriteLine("The solution path by BFS is:");
            foreach (State state in solution.Path)
            {
                Console.WriteLine(state.ToString());
            }
            Console.WriteLine();


        }
    }
}
