﻿using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISearchable
    {
        State getInitialState();
        State getGoalState();

        List<State> getAllPossibleStates(State s);
    }
}