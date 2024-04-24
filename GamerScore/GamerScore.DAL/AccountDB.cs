using Gamerscore.Core;
using Gamerscore.Core.Enums;
using Gamerscore.Core.Interfaces;
using Gamerscore.Core.Models;
using Microsoft.AspNetCore.Identity;
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
                MessageLogger.Log("Connection opened");

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    passwordHash = reader["password"].ToString() ?? "Password not found";
                }
            }
            catch (Exception e)
            {
                MessageLogger.Log($"Exception caught trying to execute query: {query} Exception:" + e.ToString());
            }
            finally
            {
                connection.Close();
                MessageLogger.Log("Connection closed");
            }
            return passwordHash;
        }

        //Uses the email which it uses to search for the user's accountId and UserRole
        public User GetAccountInfo(string _email)
        {
            string query = $"SELECT id, role FROM user WHERE email = @email;";

            using (MySqlConnection connection = new(connectionString))
            {
                try
                {
                    using MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    command.Parameters.AddWithValue("@email", _email);

                    //using MySqlCommand command = connection.CreateCommand();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int accountId = int.Parse(reader["id"].ToString());
                        UserRole role = (UserRole)Enum.Parse(typeof(UserRole), reader["role"].ToString() ?? "None");
                        User user = new(accountId, role);
                        return user;
                    }
                }
                catch (Exception e)
                {
                    MessageLogger.Log($"Exception caught trying to execute query: {query} Exception:" + e.ToString());
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
                return new User(null, UserRole.None);
            }
        }
    }
}
