﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Adapter;
using Ex1.Controller;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1.Model
{
    class Model : IModel
    {

        private Dictionary<string, Maze> DictionaryOfMazes { get; set; }
        private Dictionary<SearchableMaze, Solution> DictionaryOfMazesAndSolutions { get; set; }
        public Dictionary<string, MultiPlayerDS> DictionaryOfMultyPlayerDS { get; set; }
        public int EvaluateNodes { get; set; }
        //private IController controller;

        
        public Model(/*IController controller*/)
        {
            //this.controller = controller;
            DictionaryOfMazes = new Dictionary<string, Maze>();
            DictionaryOfMazesAndSolutions = new Dictionary<SearchableMaze, Solution>();
            DictionaryOfMultyPlayerDS = new Dictionary<string,MultiPlayerDS>();
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

        public MultiPlayerDS join(string name)
        {
            if (DictionaryOfMultyPlayerDS.ContainsKey(name)) { 
               return DictionaryOfMultyPlayerDS[name];
                
            }
            throw new NotImplementedException();
        }

        public List<string> list()
        {
            List<string> listOgGames = new List<string>();
            foreach (MultiPlayerDS mp in DictionaryOfMultyPlayerDS.Values)
            {
                if (mp.IsAvailable)
                {
                    listOgGames.Add(mp.NameOfGame);
                }
            }
            return listOgGames;
        }

        //public MultiPlayerDS play(Direction direction, TcpClient client)
        public void play(Direction direction, MultiPlayerDS multiPlayerDS, TcpClient client)
        {
            /*
            foreach (MultiPlayerDS multiPlayer in DictionaryOfMultyPlayerDS.Values)
            {
                if (multiPlayer.StartGameClient == client || multiPlayer.JoinGameClient == client)
                {
                    multiPlayer.CurrentDirection = direction;
                    return multiPlayer;
                }
            }
            */
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

        public MultiPlayerDS start(string name, int rows, int cols, TcpClient host)
        {
            if (DictionaryOfMazes.ContainsKey(name))
            {
                MultiPlayerDS multiPlayerDs = new MultiPlayerDS(host, name, DictionaryOfMazes[name]);
                DictionaryOfMultyPlayerDS.Add(name,multiPlayerDs);
                return multiPlayerDs;
            }
            else
            {
                MultiPlayerDS multiPlayerDs = new MultiPlayerDS(host, name, this.generate(name, rows, cols));
                DictionaryOfMultyPlayerDS.Add(name, multiPlayerDs);
                return multiPlayerDs;
            }
        }
    }
}
