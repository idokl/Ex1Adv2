using System.Collections.Generic;
using System.Net.Sockets;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1.Model
{
    internal interface IModel
    {
        Dictionary<string, MultiPlayerDS> DictionaryOfMultyPlayerDS { get; set; }

        //MultiPlayerDS play(Direction direction, TcpClient client);
        //void play(Direction direction, TcpClient client);
        //void close(string name);
        int EvaluateNodes { get; set; }

        Maze generate(string name, int rows, int cols);
        Solution solve(string name, int algorithmNumber);
        MultiPlayerDS start(string name, int rows, int cols, TcpClient client);
        List<string> list();
        MultiPlayerDS GetMultiplayerDataStructure(string name);
    }
}