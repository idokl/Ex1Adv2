using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher : ISearcher
    {
        protected PriorityQueue<State> openList;
        public int evaluatedNodes { get; private set; }

        public Searcher()
        {
            openList = new PriorityQueue<State>(new StateComparer());
            evaluatedNodes = 0;
        }
        protected State popOpenList()

        {
            evaluatedNodes++;
            return openList.pop();
        }
        // a property of openList
        public int OpenListSize
        { // it is a read-only property :)
            get { return openList.Count; }
        }

        // ISearcher’s methods:
        public virtual int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public abstract Solution search(ISearchable searchable);
    }

}