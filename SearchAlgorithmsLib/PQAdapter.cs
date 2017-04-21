using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public abstract class PQAdapter : Searcher
    {
        public PriorityQueue<State> openList;

        //public PQAdapter(): this(new StateComparer()) {  }

        public PQAdapter(IComparer<State> optionalSpecialComparer)
        {
            openList = new PriorityQueue<State>(optionalSpecialComparer);
        }

        // a property of openList
        public int OpenListSize => openList.Count;

        public State popOpenList()
        {
            evaluatedNodes++;
            return openList.pop();
        }
    }
}