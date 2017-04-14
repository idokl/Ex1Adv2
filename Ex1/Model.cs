using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Adapter;
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
        public int EvaluateNodes { get; set; }

        public Model()
        {
            DictionaryOfMazes = new Dictionary<string, Maze>();
            DictionaryOfMazesAndSolutions = new Dictionary<SearchableMaze, Solution>();
            MultyPlayerList = new List<MultiPlayer>();
        }


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
            foreach (MultiPlayer mp in MultyPlayerList)
            {
                if (mp.IsAvilble)
                {
                    listOgGames.Add(mp.NameOfGame);
                }
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
            if (algorithm == 1)
            {
                BestFirstSearch<PointState> BFS = new BestFirstSearch<PointState>();
                solution = BFS.search(searchableMaze);
                EvaluateNodes = BFS.getNumberOfNodesEvaluated();
            }
            else
            {
                DepthFirstSearch<PointState> DFS = new DepthFirstSearch<PointState>();
                solution = DFS.search(searchableMaze);
                EvaluateNodes = DFS.getNumberOfNodesEvaluated();
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
