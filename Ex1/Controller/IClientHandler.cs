using System.Net.Sockets;

namespace Ex1.Controller
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
