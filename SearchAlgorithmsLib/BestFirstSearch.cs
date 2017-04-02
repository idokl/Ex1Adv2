using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class BestFirstSearch<T> : ISearcher
    {
        public int getNumberOfNodesEvaluated()
        {
            throw new NotImplementedException();
        }

        public Solution search(ISearchable searchable)
        {
            IComparer<State> comperar = searchable.GetComaparer();
            PriorityQueue<State> openStates = new PriorityQueue<State>();
            HashSet<State> closeStates = new HashSet<State>();
            State initialState = searchable.getInitialState();
            openStates.push(initialState);
            while(openStates.Count > 0)
            {
                State bestOpenState = openStates.pop();
                closeStates.Add(bestOpenState);
                if (bestOpenState.Equals(searchable.getGoalState()))
                {
                    LinkedList<State> bestPath = new LinkedList<State>();
                    while(!(bestOpenState.Equals(initialState)))
                    {
                        bestPath.AddFirst(bestOpenState);
                        bestOpenState = bestOpenState.GetCameFrom();
                    }
                    bestPath.AddFirst(initialState);
                    return new Solution(bestPath);
                }
                List<State> successors = searchable.getAllPossibleStates(bestOpenState);
                foreach(State s in successors)
                {
                    if ((!closeStates.Contains(s)) && (!openStates.DoesContain(s)))
                    {
                        s.SetCameFrom(bestOpenState);
                        openStates.push(s);
                    }
                    else
                    {
                        openStates.AddElementOrTryToDecreaseItsKey(s);
                    }
                }
            }
            Console.WriteLine("This Searchable doesn't have a solution and we didn't" +
                "implement solutions for insolvable problems, so we throws an exception");
            throw new NotImplementedException();
        }
    }
}
