using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class StateComparer : Comparer<BaseState>
    {
        private static StringComparer MyComparer = StringComparer.Ordinal;
        
        public override int Compare(BaseState x, BaseState y)
        {
            return (MyComparer.Compare(x, y));
        }
    }
}
