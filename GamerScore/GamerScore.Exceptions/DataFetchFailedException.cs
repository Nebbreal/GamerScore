using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.Exceptions
{
    public class DataFetchFailedException : Exception
    {
        public DataFetchFailedException() { }
        public DataFetchFailedException(string message) : base(message) { }
        public DataFetchFailedException(string message,  Exception inner) : base(message, inner) { }
    }
}
