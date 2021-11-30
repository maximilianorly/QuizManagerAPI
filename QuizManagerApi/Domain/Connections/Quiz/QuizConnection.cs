using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using QuizManagerApi.Domain.Models;

namespace QuizManagerApi.Domain.Connections
{
    public class QuizConnection
    {

        private readonly MySqlConnection _conn;

        public QuizConnection(MySqlConnection conn)
        {
            _conn = conn;
        }

        public List<Quiz> GetAllQuizzes()
        {
            List<Quiz> list = new List<Quiz>();

            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Quizzes", _conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Quiz _quiz = new Quiz
                        {
                            Id = Convert.ToInt32(reader["Quizzes_Id"]),
                            IsActive = Convert.ToBoolean(reader["Quizzes_IsActive"]),
                            Name = reader["Quizzes_QuizName"].ToString()
                        };

                        list.Add(_quiz);
                    }
                }

                _conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return list;
        }

        public List<Quiz> GetAllActiveQuizzes()
        {
            List<Quiz> list = new List<Quiz>();

            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Quizzes WHERE Quizzes_IsActive = {Convert.ToInt32(true)}", _conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Quiz _quiz = new Quiz
                        {
                            Id = Convert.ToInt32(reader["Quizzes_Id"]),
                            IsActive = Convert.ToBoolean(reader["Quizzes_IsActive"]),
                            Name = reader["Quizzes_QuizName"].ToString()
                        };

                        list.Add(_quiz);
                    }
                }

                _conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return list;
        }

        public Quiz GetQuizById(int QuizId)
        {
            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Quizzes WHERE Quizzes_Id = {QuizId}", _conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return new Quiz()
                            {
                                Id = Convert.ToInt32(reader["Quizzes_Id"]),
                                IsActive = Convert.ToBoolean(reader["Quizzes_IsActive"]),
                                Name = reader["Quizzes_QuizName"].ToString()
                            };
                        }
                    }
                    else
                    {
                        return null;
                    }
                }

                _conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return null;
        }


        public Quiz CreateNewQuiz(Quiz NewQuiz)
        {
            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO Quizzes (Quizzes_IsActive, Quizzes_QuizName)" + $"VALUES (@IsActive, @QuizName); SELECT LAST_INSERT_ID()", _conn);

                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@IsActive", $"{Convert.ToInt32(NewQuiz.IsActive)}");
                    cmd.Parameters.AddWithValue("@QuizName", $"{NewQuiz.Name}");
                    NewQuiz.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }

                _conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            //TODO: change to get quiz by id
            return NewQuiz;
        }

        public Quiz UpdateQuizSetIsActive(int QuizId, bool IsActive)
        {
            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"UPDATE Quizzes SET Quizzes_IsActive = @IsActive WHERE Quizzes_Id = @QuizId", _conn);

                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@IsActive", $"{Convert.ToInt32(IsActive)}");
                    cmd.Parameters.AddWithValue("@QuizId", $"{QuizId}");
                    cmd.ExecuteNonQuery();
                }

                _conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return GetQuizById(QuizId);
        }
    }
}
