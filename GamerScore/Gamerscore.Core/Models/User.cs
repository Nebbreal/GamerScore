using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerscore.Core.Models
{
    public class User
    {
        public string? Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public User(string _username, string _email)
        {
            Username = _username;
            Email = _email;
        }
        public User(string _email)
        {
            Email = _email;
        }
    }
}
