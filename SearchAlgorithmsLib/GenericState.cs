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

        private GenericState(T stateContent)
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

        public static class StatePool
        {
            static private Dictionary<T, GenericState<T>> StatesHashTable = new Dictionary<T, GenericState<T>>();

            static public GenericState<T> GetState(T stateContent)
            {
                if (StatesHashTable.ContainsKey(stateContent))
                {
                    return StatesHashTable[stateContent];
                }
                else
                {
                    GenericState<T> newState = new GenericState<T>(stateContent);
                    StatesHashTable.Add(stateContent, newState);
                    return newState;
                }
            }
        }

    }
}        