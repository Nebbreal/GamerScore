using Gamerscore.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.DAL
{
    public class AccountDB : IAccountDB
    {
        private readonly string connectionString;
        public AccountDB(string _connectionString) 
        {
            this.connectionString = _connectionString;
        }

        public bool CreateUser(string _username, string _email, string _password)
        {
            BasicDB basicDB = new(connectionString);
            string query = $"INSERT INTO user (username, email, role, password) VALUES ('{_username}', '{_email}', 'user', '{_password}')";

            if (basicDB.ExecuteNonQuery(query))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetPasswordHash(string _email)
        { 
            string query = $"SELECT password FROM user WHERE email = '{_email}';";
            string passwordHash = "Password not found";
            MySqlConnection connection = new(connectionString);
            try
            {
                connection.Open();
                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    passwordHash = reader["password"].ToString() ?? "Password not found";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return passwordHash;
        }
    }
}
