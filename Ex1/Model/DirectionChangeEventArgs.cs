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

        public override string ToString()
        {
            switch (direction)
            {
                case Direction.Up:
                    {
                        return "Up";
                    }
                case Direction.Down:
                    {
                        return "Down";

                    }
                case Direction.Left:
                    {
                        return "Left";

                    }
                case Direction.Right:
                    {
                        return "Right";

                    }
                default:
                    return "";
            }
           
        }
    }
}
