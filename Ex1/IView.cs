using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    interface IView
    {
        void Start();
        //void ExecuteCommand(string commandName);
    }

    public enum move
    {
        up, down,left,right
    }
}
