using System.Linq;

namespace Ex1.Controller
{
    internal class CommandParser
    {
        public CommandParser(string commandLine)
        {
            var arr = commandLine.Split(' ');
            CommandKey = arr[0];
            Args = arr.Skip(1).ToArray();
        }

        public string CommandKey { get; }
        public string[] Args { get; }
    }
}