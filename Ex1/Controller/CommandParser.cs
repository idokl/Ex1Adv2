using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.Controller
{
    class CommandParser
    {
        public string CommandKey {get; }
        public string[] Args { get; }

        public CommandParser(string commandLine)
        {
            string[] arr = commandLine.Split(' ');
            CommandKey = arr[0];
            Args = arr.Skip(1).ToArray();
        }
    }
}
