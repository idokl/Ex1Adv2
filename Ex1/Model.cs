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

        private Dictionary<string, Maze> dictionaryOfMazes = new Dictionary<string, Maze>();
        private Dictionary<SearchableMaze, Solution> dictionaryOfMazesAndSolutions = new Dictionary<SearchableMaze, Solution>();


        public void close(string name)
        {
            throw new NotImplementedException();
        }

        public Maze generate(string name, int rows, int cols)
        {
            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze MyMaze = dfsMazeGenerator.Generate(rows, cols);
            MyMaze.Name = name;
            dictionaryOfMazes[name] = MyMaze;
            return MyMaze;
        }

        public void join(string name)
        {
            throw new NotImplementedException();
        }

        public List<string> list()
        {
            List<string> listOgGames = new List<string>();
            foreach (String name in dictionaryOfMazes.Keys)
            {
                listOgGames.Add(name);
            }
            return listOgGames;
        }

        public void play(Direction direction)
        {
            throw new NotImplementedException();
        }

        public Solution solve(string name, int algorithm)
        {
            Maze maze = this.dictionaryOfMazes[name];
            SearchableMaze searchableMaze = new SearchableMaze(maze);
            if (dictionaryOfMazesAndSolutions.ContainsKey(searchableMaze))
                return dictionaryOfMazesAndSolutions[searchableMaze];
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
            return solution;
        }

        public void start(string name, int rows, int cols)
        {
            if (dictionaryOfMazes.ContainsKey(name))
            {

            }
            else
            {
                this.generate(name, rows, cols);
            }
        }
    }
}
