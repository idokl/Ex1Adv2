using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution
    {
        private LinkedList<State> Path { get; }

        public Solution(LinkedList<State> path)
        {
            this.Path = path;
        }
    }
}
