﻿using System.IO;
using System.Net.Sockets;

namespace Ex1
{
    class MultiPlayerGame
    {
        private TcpClient client;
        private string s;
        private MultiPlayerDS multiPlayerDs;
        private TcpClient opponent;

        public MultiPlayerGame(TcpClient client, string s,MultiPlayerDS multiPlayerDs)
        {
            this.client = client;
            this.s = s;
            this.multiPlayerDs = multiPlayerDs;
            this.opponent = this.multiPlayerDs.JoinGameClient;
        }

        public void Play()
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
                writer.Write(this.s);
        }
    }
}
