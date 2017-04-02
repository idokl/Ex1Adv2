///////////////////////////////////////////////////////////////////////////
//The class PointState in EX1 Project has to replace this implementation //
///////////////////////////////////////////////////////////////////////////
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class PointState : BaseState
    {
        //we have to change these members (and the constructor) to maintain Position struct instead of coordinates
        private int X;
        private int Y;

        public PointState(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(State s)
        {
            return ((this.X == (s as PointState).X) 
                && (this.Y == (s as PointState).Y));
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
*/