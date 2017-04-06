using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;

namespace Ex1
{
    interface IModel
    {
        Maze generate(string name, int rows, int cols);
        void solve(string name, int algorithm);
        void start(string name, int rows, int cols);
        List<string> list();
        void join(string name);
        void play(move move);
        void close(string name);
    }

    public enum move
    {
        up, down, left, right
    }
}
