using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Adapter;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1.Model
{
    internal class Model : IModel
    {
        public Model()
        {
            DictionaryOfMazes = new Dictionary<string, Maze>();
            DictionaryOfMazesAndSolutions = new Dictionary<SearchableMaze, Solution>();
            DictionaryOfMultyPlayerDS = new Dictionary<string, MultiPlayerDS>();
        }

        private Dictionary<string, Maze> DictionaryOfMazes { get; }
        private Dictionary<SearchableMaze, Solution> DictionaryOfMazesAndSolutions { get; }
        public Dictionary<string, MultiPlayerDS> DictionaryOfMultyPlayerDS { get; set; }
        public int EvaluateNodes { get; set; }


        public Maze generate(string name, int rows, int cols)
        {
            var dfsMazeGenerator = new DFSMazeGenerator();
            var MyMaze = dfsMazeGenerator.Generate(rows, cols);
            MyMaze.Name = name;
            DictionaryOfMazes[name] = MyMaze;
            return MyMaze;
        }

        public MultiPlayerDS GetMultiplayerDataStructure(string name)
        {
            if (DictionaryOfMultyPlayerDS.ContainsKey(name)) return DictionaryOfMultyPlayerDS[name];
            throw new NotImplementedException();
        }

        public List<string> list()
        {
            var listOgGames = new List<string>();
            foreach (var mp in DictionaryOfMultyPlayerDS.Values)
                if (mp.AvailableToJoin)
                    listOgGames.Add(mp.NameOfGame);
            return listOgGames;
        }

        public Solution solve(string name, int algorithmNumber)
        {
            var maze = DictionaryOfMazes[name];
            var searchableMaze = new SearchableMaze(maze);
            if (DictionaryOfMazesAndSolutions.ContainsKey(searchableMaze))
                return DictionaryOfMazesAndSolutions[searchableMaze];
            Solution solution;

             /*
            if (algorithm == 1)
            {
                var BFS = new BestFirstSearch<PointState>();
                solution = BFS.search(searchableMaze);
                EvaluateNodes = BFS.getNumberOfNodesEvaluated();
            }
            else
            {
                var DFS = new DepthFirstSearch<PointState>();
                solution = DFS.search(searchableMaze);
                EvaluateNodes = DFS.getNumberOfNodesEvaluated();
            }
            */

            ISearcher searchAlgorithm = null;
            switch (algorithmNumber)
            {
                case 0:
                    searchAlgorithm = new BestFirstSearch<PointState>();
                    break;
                case 1:
                    searchAlgorithm = new DepthFirstSearch<PointState>();
                    break;
            }

            solution = searchAlgorithm.search(searchableMaze);
            EvaluateNodes = searchAlgorithm.getNumberOfNodesEvaluated();
            DictionaryOfMazesAndSolutions.Add(searchableMaze, solution);
            return solution;
        }

        public MultiPlayerDS start(string name, int rows, int cols, TcpClient host)
        {
            if (DictionaryOfMazes.ContainsKey(name))
            {
                var multiPlayerDs = new MultiPlayerDS(host, name, DictionaryOfMazes[name]);
                DictionaryOfMultyPlayerDS.Add(name, multiPlayerDs);
                return multiPlayerDs;
            }
            else
            {
                var multiPlayerDs = new MultiPlayerDS(host, name, generate(name, rows, cols));
                DictionaryOfMultyPlayerDS.Add(name, multiPlayerDs);
                return multiPlayerDs;
            }
        }
    }
}