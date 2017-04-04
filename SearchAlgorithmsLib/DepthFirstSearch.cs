using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class DepthFirstSearch<T> : Searcher
    {
        public override Solution search(ISearchable searchable)
        {
            IComparer<State> comperar = new StateComparer();

            Stack<State> stackOfStates = new Stack<State>();
            State initialState = searchable.getInitialState();
            LinkedList<State> path = new LinkedList<State>();
            HashSet<State> visited = new HashSet<State>();
            Solution solution = new Solution();
            stackOfStates.Push(initialState);
            while (stackOfStates.Count > 0)
            {
                State currentState = stackOfStates.Pop();

                if (currentState.Equals(searchable.getGoalState()))
                {
                    while (!(currentState.Equals(initialState)))
                    {

                        solution.Path.AddFirst(currentState);
                        currentState = currentState.GetCameFrom();
                    }

                    return solution;
                }

                if (!visited.Contains(currentState))
                {
                    visited.Add(currentState);
                }
                List<State> successors = searchable.getAllPossibleStates(currentState);

                foreach (State s in successors)
                {
                    s.SetCameFrom(currentState);
                    stackOfStates.Push(s);
                }

            }
            Console.WriteLine("This Searchable doesn't have a solution and we didn't" +
                "implement solutions for insolvable problems, so we throws an exception");
            throw new NotImplementedException();

        }
    }
}
