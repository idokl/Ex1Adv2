using System.IO;
using System.Net.Sockets;

namespace Ex1.Model
{
    class SinglePlayerGame
    {
        private TcpClient client;
        private string stream;

        public SinglePlayerGame(TcpClient client, string stream)
        {
            this.client = client;
            this.stream = stream;
        }

        public void SendMassage()
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
                writer.Write(this.stream);
            client.Close();
        }
    }
}
