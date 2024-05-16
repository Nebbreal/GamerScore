using Gamerscore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerscore.Core.Interfaces
{
    public interface IAccountRepository
    {
        public bool CreateUser(string _username, string _email, string _password);
        public string GetPasswordHash(string _email);
        public User GetAccountInfo(string _email);
    }
}
