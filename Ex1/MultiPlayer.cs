using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Ex1
{
    class MultiPlayer
    {
        public TcpClient StartGameClient { get; set; }
        public TcpClient JoinGameClient { get; set; }
        public string NameOfGame { get; set; }
        public Maze MazeInit { get; set; }
        public Direction CurrentDirection { get; set; }
        public bool IsAvilble { get; set; }
    }
}
