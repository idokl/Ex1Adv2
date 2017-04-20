using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    public class DirectionChangeEventArgs : EventArgs
    {
        public Direction direction;

        public DirectionChangeEventArgs(Direction direction)
        {
            this.direction = direction;
        }

    }
}
