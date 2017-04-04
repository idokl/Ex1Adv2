using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class PointState : BaseState
    {
        public MazeLib.Position CurrentPosition { get; }

        public PointState(MazeLib.Position currentPosition)
        {
            this.CurrentPosition = currentPosition;
        }

        public override bool Equals(State s)
        {
            return ((this.CurrentPosition.Row == (s as PointState).CurrentPosition.Row) 
                && (this.CurrentPosition.Col == (s as PointState).CurrentPosition.Col));
        }

        public override string ToString()
        {
            return String.Format("{0},{1}", CurrentPosition.Row,CurrentPosition.Col);
        }

        //public override bool Equals(object s) => Equals(s as State);
        public override int GetHashCode() => CurrentPosition.GetHashCode();
    }
}
