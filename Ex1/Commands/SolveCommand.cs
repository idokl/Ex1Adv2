using System;
using System.Collections.Generic;
using System.IO;
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
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            Solution solution = model.solve(name, algorithm);
            StringBuilder solutionStringBuilder = new StringBuilder("");
            LinkedListNode<State> state = solution.Path.First;
            PointState point = state.Value as PointState;
            LinkedListNode<State> nextState;
            PointState nextPoint;
            for (nextState = state.Next; nextState != null; nextState = state.Next)
            {
                nextPoint = nextState.Value as PointState;
                if (nextPoint.CurrentPosition.Row == point.CurrentPosition.Row)
                {
                    if (nextPoint.CurrentPosition.Col < point.CurrentPosition.Col)
                        solutionStringBuilder.Append("0");
                    else
                        solutionStringBuilder.Append("1");
                }
                else
                {
                    if (nextPoint.CurrentPosition.Row < point.CurrentPosition.Row)
                        solutionStringBuilder.Append("2");
                    else
                        solutionStringBuilder.Append("3");
                }
                state = nextState;
                point = nextPoint;
            }
            PacketStream listPacketStream = new PacketStream(false, solutionStringBuilder.ToString());
            return Newtonsoft.Json.JsonConvert.SerializeObject(listPacketStream);
        }
    }
}
