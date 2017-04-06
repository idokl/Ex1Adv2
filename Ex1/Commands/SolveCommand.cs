using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace Ex1
{
    class SolveCommand : ICommand
    {
        private IModel model;
        public SolveCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            if (algorithm==0)
            {
                DepthFirstSearch<PointState> BFS = new DepthFirstSearch<PointState>();
                //Solution solutionDFS = BFS.search();
            }
            else
            {
                DepthFirstSearch<PointState> DFS = new DepthFirstSearch<PointState>();
                //Solution solutionDFS = DFS.search();
            }
            return "";
        }
    }
}
