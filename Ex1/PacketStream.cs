using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class PacketStream
    {
        public bool MultiPlayer { get; set; } = false;
        public MultiPlayerDS MultiPlayerDs { get; set; }
        public string StringStream { get; set; }
    }
}
