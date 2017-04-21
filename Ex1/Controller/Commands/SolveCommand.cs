using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Adapter;
using Ex1.Model;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;

namespace Ex1.Controller.Commands
{
    internal class SolveCommand : ICommand
    {
        private readonly IModel model;

        public SolveCommand(IModel model)
        {
            this.model = model;
        }

        private string Name { get; set; }
        private StringBuilder solutionStringBuilder { get; set; }

        public PacketStream Execute(string[] args, TcpClient client)
        {
            Name = args[0];
            var algorithmNumber = int.Parse(args[1]);

            var solution = model.solve(Name, algorithmNumber);
            solutionStringBuilder = new StringBuilder("");
            var state = solution.Path.First;
            var point = state.Value as PointState;
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
            var solvePacketStream = new PacketStream
            {
                StringStream = ToJSON()
            };
            return solvePacketStream;
        }

        private string ToJSON()
        {
            var startJObject = new JObject
            {
                ["Name"] = Name,
                ["Solution"] = solutionStringBuilder.ToString(),
                ["NodesEvaluated"] = model.EvaluateNodes
            };

            return startJObject.ToString();
        }
    }
}