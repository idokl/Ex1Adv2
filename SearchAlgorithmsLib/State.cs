using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface State
    {

        /// <summary>
        /// Equalses the specified s.
        /// </summary>
        /// <param name="s"> The state. </param>
        /// <returns></returns>
        bool Equals(State s); // we overload Object's Equals method

        double Cost { get; set; }

        State CameFrom { get; set; }

    }
}
