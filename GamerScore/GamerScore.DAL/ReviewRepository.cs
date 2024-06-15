using Gamerscore.Core;
using Gamerscore.Core.Interfaces.Repositories;
using GamerScore.DTO;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;

namespace GamerScore.DAL
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string connectionString;
        public ReviewRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public bool CreateReview(Review _review)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = string.Empty;
                    if (String.IsNullOrWhiteSpace(_review.UserContext))
                    {
                        query = "INSERT INTO review (user_id, game_id, starRating) VALUES (@userId, @gameId, @starRating);";

                    }
                    else
                    {
                        query = "INSERT INTO review (user_id, game_id, userContext, starRating) VALUES (@userId, @gameId, @userContext, @starRating);";
                    }
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@userId", _review.UserId);
                    command.Parameters.AddWithValue("@gameId", _review.GameId);
                    if (!String.IsNullOrWhiteSpace(_review.UserContext))
                    {
                        command.Parameters.AddWithValue("@userContext", _review.UserContext);
                    }
                    command.Parameters.AddWithValue("@starRating", _review.StarRating);

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
