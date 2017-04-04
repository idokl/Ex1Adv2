using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class StateComparer : IComparer<State>
    {
        //private static StringComparer MyComparer = StringComparer.Ordinal;
        
        public int Compare(State x, State y)
        {
            if (x.Cost< y.Cost)
                return -1;
            else
                return 1;
        }
    }
}
