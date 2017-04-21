using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// implementation of IComparer of States. compare states according to their cost.
/// </summary>
namespace SearchAlgorithmsLib
{
    class StateComparer : IComparer<State>
    {
        public int Compare(State x, State y)
        {
            if (x.Cost < y.Cost)
                return -1;
            if (x.Cost > y.Cost)
                return 1; 
            return 0;
        }
    }
}
