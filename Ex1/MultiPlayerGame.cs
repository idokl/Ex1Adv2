﻿using System;
using System.IO;
using System.Net.Sockets;

namespace Ex1
{
    class MultiPlayerGame
    {
        private bool IamTheStartClient;
        private MultiPlayerDS multiPlayerDs;
        private BinaryReader Reader;
        private BinaryWriter Writer;

        //public MultiPlayerGame(TcpClient client, string s,MultiPlayerDS multiPlayerDs)
        public MultiPlayerGame(MultiPlayerDS multiPlayerDs, bool amITheStartClient)
        {
            this.multiPlayerDs = multiPlayerDs;
            IamTheStartClient = amITheStartClient;
        }

        public void Initialize()
        {
            if (IamTheStartClient)
                this.WaitToOpponent();
            NetworkStream stream;
            if (IamTheStartClient)
                stream = multiPlayerDs.StartGameClient.GetStream();
            else
                stream = multiPlayerDs.JoinGameClient.GetStream();
            Reader = new BinaryReader(stream);
            Writer = new BinaryWriter(stream);
            Writer.Write("pass to multiplayer mode");
        }

        private void WaitToOpponent()
        {
            while(multiPlayerDs.JoinGameClient == null)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        public void ManageCommunication()
        {
            multiPlayerDs.SomebodyClosedTheGame += new ChangedEventHandler(DSbecameClosed);

            while (!multiPlayerDs.Closed)
            {
                string commandFromTheClient = Reader.ReadString();
                if(multiPlayerDs.Closed)
                {
                    break;
                }

                Console.WriteLine("debug massage: commandFromTheClient = {0}", commandFromTheClient);
                if (commandFromTheClient == "close")
                {
                    multiPlayerDs.Close();
                    Console.WriteLine("debug massage: waiting 0.3 second");
                    System.Threading.Thread.Sleep(300);
                }

                //string answer = "The server recived your command. Your command was: " + commandFromTheClient;
                //Writer.Write(answer);
            }

        }

        // This will be called whenever the list changes.
        private void DSbecameClosed(object sender, EventArgs e)
        {
            if (!multiPlayerDs.Closed)
            {
                Console.WriteLine("This function is called when the MultiPlayedDS.Closed is changed to be true.");
                Console.WriteLine("We will pass massage about it ro our client.");
                Writer.Write("hello client, we noticed that your multiplayer game is closed now");
            }
        }

    }
}





/*
List.Changed += new ChangedEventHandler(ListChanged);
        }

        // This will be called whenever the list changes.
        private void ListChanged(object sender, EventArgs e)
{
    Console.WriteLine("This is called when the event fires.");
}
*/