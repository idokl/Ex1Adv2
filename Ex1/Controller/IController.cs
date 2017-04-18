using System.Net.Sockets;

namespace Ex1.Controller
{
    interface IController
    {
       bool ExecuteCommand(string commandLine, TcpClient client);
    }
}
