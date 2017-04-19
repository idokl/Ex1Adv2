using System.Net.Sockets;
using Ex1.Model;

namespace Ex1.Controller
{
    interface IController
    {
       bool ExecuteCommand(string commandLine, TcpClient client);
        void SetModel(IModel model);
    }
}
