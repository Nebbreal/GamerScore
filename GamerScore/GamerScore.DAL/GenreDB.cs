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
using System.Xml.Linq;

namespace GamerScore.DAL
{
    public class GenreDB : IGenreDB
    {
        private readonly string connectionString;
        public GenreDB(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public List<Genre> GetAllGenres()
        {
            string query = "SELECT id, name, imageUrl FROM genre";
            List<Genre> genres = new List<Genre>();

            using (MySqlConnection connection = new(connectionString))
            {
                try
                {
                    using MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int? genreID = reader["id"] == DBNull.Value ? null : int.Parse(reader["id"].ToString());
                        string? genreName = reader["name"].ToString() ?? null;
                        string? genreImageUrl = reader["imageUrl"].ToString() ?? null;
                        Genre genre = new(genreID, genreName, genreImageUrl);

                        genres.Add(genre);
                    }
                    return genres;
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
                return genres;
            }
        }

        public Genre GetGenreByName(string _name)
        {
            string query = "SELECT id, name, imageUrl FROM genre WHERE name = @name";

            using (MySqlConnection connection = new(connectionString))
            {
                try
                {
                    using MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    command.Parameters.AddWithValue("@name", _name);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int? genreID = reader["id"] == DBNull.Value ? null : int.Parse(reader["id"].ToString());
                        string? genreName = reader["name"].ToString() ?? null;
                        string? genreImageUrl = reader["imageUrl"].ToString() ?? null;
                        Genre genre = new(genreID, genreName, genreImageUrl);

                        return genre;
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
                return new Genre(null, null, null);
            }
        }

        public bool CreateGenre(string _name, string? _imageUrl)//ToDo: turn this into a more general function/edit ExecuteNonQuery?
        {
            //Change the query so that it is possible to leave the image url empty
            string query = "INSERT INTO genre (name, imageUrl) VALUES (@name, @imageUrl);";
            if (_imageUrl == null)
            {
                query = "INSERT INTO genre (name) VALUES (@name);";
            }
            

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
