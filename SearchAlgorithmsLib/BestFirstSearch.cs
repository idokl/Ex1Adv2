using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public class BestFirstSearch<T> : PQAdapter
    {
        public BestFirstSearch() : base(new StateComparer())
        {
        }

        public override Solution search(ISearchable searchable)
        {
            IComparer<State> comperar = new StateComparer();
            //PriorityQueue<State> openList = new PriorityQueue<State>();
            var closeStates = new HashSet<State>();
            var initialState = searchable.getInitialState();
            openList.push(initialState);
            while (OpenListSize > 0)
            {
                var bestOpenState = popOpenList();
                closeStates.Add(bestOpenState);
                if (bestOpenState.Equals(searchable.getGoalState()))
                {
                    var bestPath = new LinkedList<State>();
                    while (!bestOpenState.Equals(initialState))
                    {
                        bestPath.AddFirst(bestOpenState);
                        bestOpenState = bestOpenState.CameFrom;
                    }
                    bestPath.AddFirst(initialState);
                    var solution = new Solution();
                    solution.Path = bestPath;
                    return solution;
                }
                var successors = searchable.getAllPossibleStates(bestOpenState);
                foreach (var s in successors)
                    if (!closeStates.Contains(s))
                        if (!openList.DoesContain(s))
                        {
                            s.CameFrom = bestOpenState;
                            openList.push(s);
                        }
                        else
                        {
                            s.CameFrom = bestOpenState;
                            openList.TryToDecreaseTheKeyOfTheElement(s);
                        }
            }
            Console.WriteLine("This Searchable doesn't have a solution and we didn't" +
                              "implement solutions for insolvable problems, so we throw an exception");
            throw new NotImplementedException();
        }
    }
}