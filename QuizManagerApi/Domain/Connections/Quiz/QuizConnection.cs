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
    }
}
