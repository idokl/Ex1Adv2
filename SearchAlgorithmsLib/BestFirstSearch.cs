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
        public PQAdapter pqAdapter = new PQAdapter();
       
        public override Solution search(ISearchable searchable)
        {
            IComparer<State> comperar = new StateComparer();
            //PriorityQueue<State> openList = new PriorityQueue<State>();
            HashSet<State> closeStates = new HashSet<State>();
            State initialState = searchable.getInitialState();
            pqAdapter.openList.push(initialState);
            while(pqAdapter.OpenListSize > 0)
            {
                State bestOpenState = pqAdapter.popOpenList();
                closeStates.Add(bestOpenState);
                if (bestOpenState.Equals(searchable.getGoalState()))
                {
                    LinkedList<State> bestPath = new LinkedList<State>();
                    while(!(bestOpenState.Equals(initialState)))
                    {
                        bestPath.AddFirst(bestOpenState);
                        bestOpenState = bestOpenState.CameFrom;
                    }
                    bestPath.AddFirst(initialState);
                    Solution solution = new Solution();
                   solution.Path = bestPath;
                    return solution;
                }
                List<State> successors = searchable.getAllPossibleStates(bestOpenState);
                foreach(State s in successors)
                {
                    if ((!closeStates.Contains(s)) && (!pqAdapter.openList.DoesContain(s)))
                    {
                        s.CameFrom = bestOpenState;
                        s.Cost = s.CameFrom.Cost + 1;
                        pqAdapter.openList.push(s);
                    }
                    else
                    {
                        s.CameFrom = bestOpenState;
                        s.Cost = s.CameFrom.Cost + 1;
                        pqAdapter.openList.AddElementOrTryToDecreaseItsKey(s);
                    }
                }
            }
            Console.WriteLine("This Searchable doesn't have a solution and we didn't" +
                "implement solutions for insolvable problems, so we throws an exception");
            throw new NotImplementedException();
        }
    }
}
