using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    class Model : IModel
    {

        public Dictionary<string, Maze> dictionaryOfMazes = new Dictionary<string, Maze>();
        public Dictionary<SearchableMaze, Solution> dictionaryOfMazesAndSolutions = new Dictionary<SearchableMaze, Solution>();

        public void close(string name)
        {
            throw new NotImplementedException();
        }

        public Maze generate(string name, int rows, int cols)
        {
            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze MyMaze = dfsMazeGenerator.Generate(rows, cols);
            MyMaze.Name = name;
            return MyMaze;
        }

        public void join(string name)
        {
            throw new NotImplementedException();
        }

        public List<string> list()
        {
            throw new NotImplementedException();
        }

        public void play(move move)
        {
            throw new NotImplementedException();
        }

        public void solve(string name, int algorithm)
        {
            Maze maze = this.dictionaryOfMazes[name];
            SearchableMaze searchableMaze = new SearchableMaze(maze);
            Solution solution;
            if (algorithm == 1)
            {
                BestFirstSearch<PointState> BFS = new BestFirstSearch<PointState>();
                solution = BFS.search(searchableMaze);
            }
            else
            {
                DepthFirstSearch<PointState> DFS = new DepthFirstSearch<PointState>();
                solution = DFS.search(searchableMaze);
            }
            dictionaryOfMazesAndSolutions.Add(searchableMaze, solution);
        }

        public void start(string name, int rows, int cols)
        {
            throw new NotImplementedException();
        }
    }
}
