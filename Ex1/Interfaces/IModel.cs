using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    interface IModel
    {
        Dictionary<string, MultiPlayerDS> DictionaryOfMultyPlayerDS { get; set; }
        Maze generate(string name, int rows, int cols);
        Solution solve(string name, int algorithm);
        Maze start(string name, int rows, int cols);
        List<string> list();
        MultiPlayerDS join(string name);
        MultiPlayerDS play(Direction direction, TcpClient client);
        void close(string name);
        int EvaluateNodes { get; set; }
    }
}
