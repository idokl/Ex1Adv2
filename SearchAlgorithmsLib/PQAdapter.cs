using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public State popOpenList(){
            evaluatedNodes++;
            return openList.pop();
        }

        // a property of openList
        public int OpenListSize{ // it is a read-only property :)
            get { return openList.Count; }
        }

      
    }
}
