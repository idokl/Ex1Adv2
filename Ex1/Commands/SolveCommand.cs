using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;

namespace Ex1
{
    class SolveCommand : ICommand
    {
        private IModel model;
        private string Name { get; set; }
        private StringBuilder solutionStringBuilder { get; set; }
        public SolveCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            this.Name = args[0];
            int algorithm = int.Parse(args[1]);
            Solution solution = model.solve(this.Name, algorithm);
            this.solutionStringBuilder = new StringBuilder("");
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
                        this.solutionStringBuilder.Append("0");
                    else
                        this.solutionStringBuilder.Append("1");
                }
                else
                {
                    if (nextPoint.CurrentPosition.Row < point.CurrentPosition.Row)
                        this.solutionStringBuilder.Append("2");
                    else
                        this.solutionStringBuilder.Append("3");
                }
                state = nextState;
                point = nextPoint;
            }
            PacketStream solvePacketStream = new PacketStream
            {
                StringStream = solutionStringBuilder.ToString()
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(solvePacketStream);
        }

        public string ToJSON()
        {
            JObject startJObject = new JObject
            {
                ["Name"] = this.Name,
                ["Solution"] = this.solutionStringBuilder.ToString(),
                ["NodesEvaluated"] = this.model.GetEvaluateNodes
            };

            return startJObject.ToString();
        }
    }
}
