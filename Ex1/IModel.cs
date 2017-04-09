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
    interface IModel
    {
        Maze generate(string name, int rows, int cols);
        Solution solve(string name, int algorithm);
        void start(string name, int rows, int cols);
        List<string> list();
        void join(string name);
        void play(Move move);
        void close(string name);
    }

    public enum Move
    {
        up, down, left, right
    }
}
