﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution
    {
        public LinkedList<State> Path {  get; set; }

        public Solution(LinkedList<State> path)
        {
            this.Path = path;
        }

        public Solution()
        {

        }
    }
}
