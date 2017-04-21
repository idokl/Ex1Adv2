using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
