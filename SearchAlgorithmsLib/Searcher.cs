using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher : ISearcher
    {
        public int evaluatedNodes { get; set; } = 0;

        public Searcher()
        {
           evaluatedNodes = 0;
        }
       
        // ISearcher’s methods:
        public virtual int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public abstract Solution search(ISearchable searchable);
    }

}