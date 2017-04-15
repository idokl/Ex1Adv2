using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
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

        public void Play()
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
                writer.Write(this.stream);
            client.Close();
        }
    }
}
