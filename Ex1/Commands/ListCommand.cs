using System;
using System.Collections.Generic;
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
        public string Execute(string[] args, TcpClient client = null)
        {
            List<String> listOfGames =  model.list();
            StringBuilder listOfGameStringBuilder = new StringBuilder(" ");
            foreach (String name in listOfGames)
            {
                String nameFormat = String.Format("{0},", name);
                listOfGameStringBuilder.AppendLine(nameFormat);
            }
            listOfGameStringBuilder.Length -= 1;
            return  listOfGameStringBuilder.ToString();
        }
    }
}
