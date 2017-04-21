namespace SearchAlgorithmsLib
{
    public interface State
    {
        double Cost { get; set; }

        State CameFrom { get; set; }

        bool Equals(State s); // we overload Object's Equals method
    }
}