using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    internal class StateComparer : IComparer<State>
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