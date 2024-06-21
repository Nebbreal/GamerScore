using Gamerscore.Core;
using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.DTO;
using GamerScore.Domain;
using GamerScore.Exceptions;
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

        public Review GetReviewByGameAndUserIdOrDefault(int _gameId, int _userId)
        {
            Review review = new Review();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT review.id, review.user_id, u.username, review.game_id, review.userContext, review.starRating, review.createdAt FROM review INNER JOIN user u ON review.user_id = u.id WHERE review.game_id = @gameId AND review.user_id = @userId;";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@gameId", _gameId);
                    command.Parameters.AddWithValue("@userId", _userId);
                    
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            review.Id = reader.GetInt32("Id");
                            review.UserId = reader.GetInt32("user_id");
                            review.Username = reader.GetString("username");
                            review.GameId = reader.GetInt32("game_id");
                            review.UserContext = reader.GetString("userContext");
                            review.StarRating = reader.GetFloat("starRating");
                            review.createdAt = reader.GetDateTime("createdAt");
                        }
                        else
                        {
                            throw new DataFetchFailedException();
                        }

                        return review;
                    }

                    return review;
                }
                catch (Exception ex)
                {
                    MessageLogger.Log($"Exception trying to execute CreateGame, Exception: " + ex.Message);

                    return new Review();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Review> GetAllReviewsByGameIdOrDefault(int _gameId)
        {
            List<Review> reviews = new List<Review>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT review.id, review.user_id, u.username, review.game_id, review.userContext, review.starRating, review.createdAt FROM review INNER JOIN user u ON review.user_id = u.id WHERE review.game_id = @gameId";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@gameId", _gameId);

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            Review review = new Review();

                            review.Id = reader.GetInt32("Id");
                            review.UserId = reader.GetInt32("user_id");
                            review.Username = reader.GetString("username");
                            review.GameId = reader.GetInt32("game_id");
                            review.UserContext = reader.GetString("userContext");
                            review.StarRating = reader.GetFloat("starRating");
                            review.createdAt = reader.GetDateTime("createdAt");

                            reviews.Add(review);
                        }
                        else
                        {
                            throw new DataFetchFailedException();
                        }

                    }

                    return reviews;
                }
                catch (Exception ex)
                {
                    MessageLogger.Log($"Exception trying to execute CreateGame, Exception: " + ex.Message);

                    return reviews;
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public bool DeleteReviewByGameIdAndUserId(int _gameId, int _userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM review WHERE game_id = @gameId AND user_id = @userId;";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@gameId", _gameId);
                    command.Parameters.AddWithValue("@userId", _userId);

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
