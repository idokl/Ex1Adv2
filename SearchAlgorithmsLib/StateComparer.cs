using System.Collections.Generic;

/// <summary>
/// implementation of IComparer of States. compare states according to their cost.
/// </summary>
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