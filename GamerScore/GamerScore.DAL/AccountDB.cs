using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.DAL
{
    public class AccountDB
    {
        private readonly string connectionString;
        public AccountDB(string _connectionString) 
        {
            this.connectionString = _connectionString;
        }

        public void CreateUser(string _username, string _email, string _password)
        {
            BasicDB basicDB = new(connectionString);
            string query = $"INSERT INTO user (username, email, role, password) VALUES ('{_username}', '{_email}', 'user', '${_password}')";
            basicDB.ExecuteNonQuery(query);
        }
    }
}
