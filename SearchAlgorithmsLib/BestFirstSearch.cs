using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class BestFirstSearch<T> : Searcher
    {
        /*
        public int getNumberOfNodesEvaluated()
        {
            throw new NotImplementedException();
        }
        */

        public override Solution search(ISearchable searchable)
        {
            IComparer<State> comperar = new StateComparer();
            //PriorityQueue<State> openList = new PriorityQueue<State>();
            HashSet<State> closeStates = new HashSet<State>();
            State initialState = searchable.getInitialState();
            openList.push(initialState);
            while(OpenListSize > 0)
            {
                State bestOpenState = popOpenList();
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
                    if ((!closeStates.Contains(s)) && (!openList.DoesContain(s)))
                    {
                        s.SetCameFrom(bestOpenState);
                        openList.push(s);
                    }
                    else
                    {
                        openList.AddElementOrTryToDecreaseItsKey(s);
                    }
                }
            }
            Console.WriteLine("This Searchable doesn't have a solution and we didn't" +
                "implement solutions for insolvable problems, so we throws an exception");
            throw new NotImplementedException();
        }
    }
}
