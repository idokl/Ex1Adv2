using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class GenericState<T> : State
    {
        public T StateContent { get; }
        public State CameFrom { get; set; }
        public double Cost { get; set; }

        public GenericState(T stateContent)
        {
            StateContent = stateContent;
        }
        public GenericState(T stateContent, double cost)
        {
            StateContent = stateContent;
            Cost = cost;
        }

        public override bool Equals(object s) => Equals(s as GenericState<T>);
        public bool Equals(State s) => Equals(s as GenericState<T>);

        public bool Equals(GenericState<T> s)
        {
            return (this.StateContent.Equals(s.StateContent));
        }

        public override int GetHashCode() => StateContent.GetHashCode();
    }
}        