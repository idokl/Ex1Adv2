using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class PQAdapter
    {
        public Searcher searcher;
        
        protected PriorityQueue<State> openList =  new PriorityQueue<State>(new StateComparer());
        protected State popOpenList(){
            //evaluatedNodes++;
            return openList.pop();
        }
        // a property of openList
        public int OpenListSize{ // it is a read-only property :)
            get { return openList.Count; }
        }

      
    }
}
