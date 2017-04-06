using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class PointState : State
    {
        public MazeLib.Position CurrentPosition { get; }

        public State CameFrom { get; set; }

        public PointState(MazeLib.Position currentPosition)
        {
            this.CurrentPosition = currentPosition;
        }

        public bool Equals(State s)
        {
            return ((this.CurrentPosition.Row == (s as PointState).CurrentPosition.Row) 
                && (this.CurrentPosition.Col == (s as PointState).CurrentPosition.Col));
        }

        public override string ToString()
        {
            return String.Format("{0},{1}", CurrentPosition.Row,CurrentPosition.Col);
        }

        public double Cost { get; set; }
     

        public override bool Equals(object s) => Equals(s as State);

        public override int GetHashCode() => CurrentPosition.GetHashCode();
        
    }
}
