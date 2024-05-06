using Gamerscore.Core;
using Gamerscore.Core.Enums;
using Gamerscore.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.DAL
{
    public class GenreDB
    {
        private readonly string connectionString;
        public GenreDB(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public bool CreateGenre(string _name, string _imageUrl)//ToDo: turn this into a more general function/edit ExecuteNonQuery?
        {
            string query = $"INSERT INTO genre (name, imageUrl) VALUES ('@name', '@imageUrl');";

            using (MySqlConnection connection = new(connectionString))
            {
                try
                {
                    using MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    command.Parameters.AddWithValue("@name", _name);
                    command.Parameters.AddWithValue("@imageUrl", _imageUrl);

                    command.ExecuteNonQuery();

                    return true;
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
                return false;
            }
        }
    }
}
