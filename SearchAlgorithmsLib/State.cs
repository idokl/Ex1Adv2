using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface State
    {
        bool Equals(State s); // we overload Object's Equals method

        double Cost { get; }

        void SetCost(double cost);

        State GetCameFrom();

        void SetCameFrom(State cameFrom);
    }
}
