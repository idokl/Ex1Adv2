using System.Net.Sockets;
using Ex1.Model;

namespace Ex1.Controller
{
    interface ICommand
    {
        PacketStream Execute(string[] args, TcpClient client = null);
    }
}
