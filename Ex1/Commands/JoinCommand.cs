using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class JoinCommand : ICommand
    {
        private IModel model;
        public JoinCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            model.join(name);
            return "join game";
        }
    }
}
