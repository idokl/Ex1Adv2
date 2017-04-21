using System.IO;
using System.Net.Sockets;

namespace Ex1.Model
{
    internal class SinglePlayerGame
    {
        private readonly TcpClient client;
        private readonly string stream;

        public SinglePlayerGame(TcpClient client, string stream)
        {
            this.client = client;
            this.stream = stream;
        }

        public void SendMassage()
        {
            using (var stream = client.GetStream())
            using (var reader = new BinaryReader(stream))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(this.stream);
            }
            client.Close();
        }
    }
}