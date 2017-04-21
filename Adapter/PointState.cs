using MazeLib;
using SearchAlgorithmsLib;

namespace Adapter
{
    public class PointState : State
    {
        public PointState(Position currentPosition)
        {
            CurrentPosition = currentPosition;
        }

        public PointState(Position currentPosition, double cost)
        {
            CurrentPosition = currentPosition;
            Cost = cost;
        }

        public Position CurrentPosition { get; }

        public State CameFrom { get; set; }

        public bool Equals(State s)
        {
            var pointState = s as PointState;
            return pointState != null && CurrentPosition.Row == pointState.CurrentPosition.Row &&
                   CurrentPosition.Col == pointState.CurrentPosition.Col;
        }

        public double Cost { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", CurrentPosition.Row, CurrentPosition.Col);
        }


        public override bool Equals(object s)
        {
            return Equals(s as State);
        }

        public override int GetHashCode()
        {
            return CurrentPosition.GetHashCode();
        }
    }
}