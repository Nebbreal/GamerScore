using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerscore.Core.Interfaces
{
    public interface IBasicDB
    {
        public bool ConnectionTest();
        public bool ExecuteNonQuery(string _query);

    }
}
