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
   
            Stack<State> s = new Stack<State>();
            State initialState = searchable.getInitialState();
            LinkedList<State> path = new LinkedList<State>();
            
            HashSet<State> discovered = new HashSet<State>();
            Solution solution = new Solution();
            solution.Path = path;
            s.Push(initialState);
            int sSize = s.Count;
            while (sSize > 0)
            {
                State v = s.Pop();
                base.evaluatedNodes++;
                if (v.Equals(searchable.getGoalState()))
                {
                    while (!(v.Equals(initialState)))
                    {
                        solution.Path.AddFirst(v);
                        v = v.CameFrom;
                    }
                    return solution;
                }
                 
                if (!discovered.Contains(v))
                {
                    discovered.Add(v);
                    List<State> successors = searchable.getAllPossibleStates(v);
                    foreach (State w in successors)
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
