using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ex1
{
    class CloseCommand : ICommand
    {
        private IModel model;
        public CloseCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            model.close(name);
            string closeJson = Newtonsoft.Json.JsonConvert.SerializeObject("");
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
                writer.Write(closeJson);
            return "1";
        }
    }
}
