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

                    //using (MySqlDataReader reader = cmd.ExecuteReader())
                    //{
                    //    while (reader.Read())
                    //    {
                    //    }
                    //}
                }

                _conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            //change to get quiz by id
            return NewQuiz;
        }
    }
}
