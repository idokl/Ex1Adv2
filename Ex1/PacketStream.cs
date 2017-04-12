using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class PacketStream
    {
        public bool MultiPlayer { get; } = false;
        public string StringStream { get; }

        public PacketStream(bool mp, string s)
        {
            this.MultiPlayer = mp;
            this.StringStream = s;
        } 
    }
}
