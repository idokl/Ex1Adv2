using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    class Model : IModel
    {

        private Dictionary<string, Maze> DictionaryOfMazes { get; set; }
        private Dictionary<SearchableMaze, Solution> DictionaryOfMazesAndSolutions { get; set; }
        public List<MultiPlayer> MultyPlayerList { get; set; }
        public int GetEvaluateNodes { get; }


        public void close(string name)
        {
            throw new NotImplementedException();
        }

        public Maze generate(string name, int rows, int cols)
        {
            DFSMazeGenerator dfsMazeGenerator = new DFSMazeGenerator();
            Maze MyMaze = dfsMazeGenerator.Generate(rows, cols);
            MyMaze.Name = name;
            DictionaryOfMazes[name] = MyMaze;
            return MyMaze;
        }

        public MultiPlayer join(string name)
        {
            foreach (MultiPlayer multiPlayer in MultyPlayerList)
            {
                if (multiPlayer.NameOfGame == name)
                {
                    return multiPlayer;
                }
            }
            throw new NotImplementedException();
        }

        public List<string> list()
        {
            List<string> listOgGames = new List<string>();
            foreach (String name in DictionaryOfMazes.Keys)
            {
                listOgGames.Add(name);
            }
            return listOgGames;
        }

        public MultiPlayer play(Direction direction, TcpClient client)
        {
            foreach (MultiPlayer multiPlayer in MultyPlayerList)
            {
                if (multiPlayer.StartGameClient == client || multiPlayer.JoinGameClient == client)
                {
                    multiPlayer.CurrentDirection = direction;
                    return multiPlayer;
                }
            }
            throw new NotImplementedException();
        }

        public Solution solve(string name, int algorithm)
        {
            Maze maze = this.DictionaryOfMazes[name];
            SearchableMaze searchableMaze = new SearchableMaze(maze);
            if (DictionaryOfMazesAndSolutions.ContainsKey(searchableMaze))
                return DictionaryOfMazesAndSolutions[searchableMaze];
            Solution solution;
            int evaluateNodes;
            if (algorithm == 1)
            {
                BestFirstSearch<PointState> BFS = new BestFirstSearch<PointState>();
                solution = BFS.search(searchableMaze);
                evaluateNodes = BFS.getNumberOfNodesEvaluated();
            }
            else
            {
                DepthFirstSearch<PointState> DFS = new DepthFirstSearch<PointState>();
                solution = DFS.search(searchableMaze);
                evaluateNodes = DFS.getNumberOfNodesEvaluated();
            }
            DictionaryOfMazesAndSolutions.Add(searchableMaze, solution);
            return solution;
        }

        public Maze start(string name, int rows, int cols)
        {
            if (DictionaryOfMazes.ContainsKey(name))
            {
                return DictionaryOfMazes[name];
            }
            else
            {
               return this.generate(name, rows, cols);
            }
        }
    }
}
