using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public class DepthFirstSearch<T> : Searcher
    {
        public override Solution search(ISearchable searchable)
        {
            var s = new Stack<State>();
            var initialState = searchable.getInitialState();
            var path = new LinkedList<State>();

            var discovered = new HashSet<State>();
            var solution = new Solution();
            solution.Path = path;
            s.Push(initialState);
            var sSize = s.Count;
            while (sSize > 0)
            {
                var v = s.Pop();
                evaluatedNodes++;
                if (v.Equals(searchable.getGoalState()))
                {
                    while (!v.Equals(initialState))
                    {
                        solution.Path.AddFirst(v);
                        v = v.CameFrom;
                    }
                    return solution;
                }

                if (!discovered.Contains(v))
                {
                    discovered.Add(v);
                    var successors = searchable.getAllPossibleStates(v);
                    foreach (var w in successors)
                    {
                        w.CameFrom = v;
                        s.Push(w);
                    }
                }
                sSize = s.Count;
            }
            Console.WriteLine("This Searchable doesn't have a solution and we didn't" +
                              "implement solutions for insolvable problems, so we throws an exception");
            throw new NotImplementedException();
        }
    }
}