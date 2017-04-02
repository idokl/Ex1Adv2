using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class StringState : BaseState
    {
        private string state; // the state represented by a string

        public StringState(string state) // CTOR
        {
            this.state = state;
        }

        public override bool Equals(State s) // we overload Object's Equals method
        {
            return state.Equals((s as StringState).state);
        }
        // ...
    }
}
