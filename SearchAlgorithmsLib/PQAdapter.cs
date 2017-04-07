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

        public PQAdapter()
        {
            openList = new PriorityQueue<State>(new StateComparer());
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
