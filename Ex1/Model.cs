using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class Model : IModel
    {
        public void close(string name)
        {
            throw new NotImplementedException();
        }

        public void generate(string name, int rows, int cols)
        {
            throw new NotImplementedException();
        }

        public void join(string name)
        {
            throw new NotImplementedException();
        }

        public List<string> list()
        {
            throw new NotImplementedException();
        }

        public void play(move move)
        {
            throw new NotImplementedException();
        }

        public void solve(string name, int algorithm)
        {
            throw new NotImplementedException();
        }

        public void start(string name, int rows, int cols)
        {
            throw new NotImplementedException();
        }
    }
}
