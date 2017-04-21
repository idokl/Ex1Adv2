using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public interface ISearchable
    {
        State getInitialState();
        State getGoalState();

        List<State> getAllPossibleStates(State s);
        //Comparer<State> GetComaparer();
    }
}