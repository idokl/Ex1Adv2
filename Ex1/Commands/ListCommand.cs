using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class ListCommand : ICommand
    {
        private IModel model;
        public ListCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            List<String> listOfGames =  model.list();
            StringBuilder listOfGameStringBuilder = new StringBuilder(" ");
            foreach (String name in listOfGames)
            {
                String nameFormat = String.Format("{0},", name);
                listOfGameStringBuilder.AppendLine(nameFormat);
            }
            listOfGameStringBuilder.Length -= 1;
            string listJson = Newtonsoft.Json.JsonConvert.SerializeObject(listOfGameStringBuilder);
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
                writer.Write(listJson);
            return "-1";
        }
    }
}
