using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class PQAdapter : Searcher
    {
        public Searcher searcher;

        public PriorityQueue<State> openList;

        public PQAdapter()
        {
            openList = new PriorityQueue<State>(new StateComparer());
        }

        public State popOpenList(){
            evaluatedNodes++;
            return openList.pop();
        }

        public override Solution search(ISearchable searchable)
        {
            throw new NotImplementedException();
        }

        // a property of openList
        public int OpenListSize{ // it is a read-only property :)
            get { return openList.Count; }
        }

      
    }
}
