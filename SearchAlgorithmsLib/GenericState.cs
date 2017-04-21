using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public class GenericState<T> : State
    {
        private GenericState(T stateContent)
        {
            StateContent = stateContent;
        }

        public GenericState(T stateContent, double cost)
        {
            StateContent = stateContent;
            Cost = cost;
        }

        public T StateContent { get; }
        public State CameFrom { get; set; }
        public double Cost { get; set; }

        public bool Equals(State s)
        {
            return Equals(s as GenericState<T>);
        }

        public override bool Equals(object s)
        {
            return Equals(s as GenericState<T>);
        }

        public bool Equals(GenericState<T> s)
        {
            return StateContent.Equals(s.StateContent);
        }

        public override int GetHashCode()
        {
            return StateContent.GetHashCode();
        }

        public static class StatePool
        {
            private static readonly Dictionary<T, GenericState<T>> StatesHashTable =
                new Dictionary<T, GenericState<T>>();

            public static GenericState<T> GetState(T stateContent)
            {
                if (StatesHashTable.ContainsKey(stateContent))
                    return StatesHashTable[stateContent];
                var newState = new GenericState<T>(stateContent);
                StatesHashTable.Add(stateContent, newState);
                return newState;
            }
        }
    }
}