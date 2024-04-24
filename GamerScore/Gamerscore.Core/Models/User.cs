using Gamerscore.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerscore.Core.Models
{
    public class User
    {
        public int? Id { get; private set; }
        public string? Username { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public string Email { get; private set; } = string.Empty;

        public User(int? _accountId, UserRole _role) 
        {
            Id = _accountId;
            Role = _role;
        }
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
