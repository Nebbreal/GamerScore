using Gamerscore.Core;
using Gamerscore.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.DAL
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string connectionString;
        public ReviewRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public bool CreateReview(int userId, int gameId, string userContext, int starRating)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO review (user_id, game_id, userContext, starRating) VALUES (@userId, @gameId, @userContext, @starRating);";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@gameId", gameId);
                    command.Parameters.AddWithValue("@userContext", userContext);
                    command.Parameters.AddWithValue("@starRating", starRating);

                    command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageLogger.Log($"Exception trying to execute CreateGame, Exception: " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
