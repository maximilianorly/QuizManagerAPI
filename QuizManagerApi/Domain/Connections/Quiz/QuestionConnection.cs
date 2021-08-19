using QuizManagerApi.Domain.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizManagerApi.Domain.Connections
{
    public class QuestionConnection
    {
        //public string ConnectionString { get; set; }


        //public QuestionConnection(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}


        //private MySqlConnection GetConnection()
        //{
        //    return new MySqlConnection("Server=localhost; port=3306; Database=QuizManager; Uid=root; Pwd=password");
        //}

        private readonly MySqlConnection _conn;

        public QuestionConnection(MySqlConnection conn)
        {
            _conn = conn;
        }

        public List<QuizQuestion> GetAllQuizQuestions()
        {
            List<QuizQuestion> list = new List<QuizQuestion>();

            try
            {
                //using (MySqlConnection conn = GetConnection())
                //{
                //    conn.Open();
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Questions", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuizQuestion _quizQuestion = new QuizQuestion
                            {
                                Id = Convert.ToInt32(reader["Questions_Id"]),
                                Question = reader["Questions_Question"].ToString(),
                                IsActive = reader.GetBoolean("Questions_IsActive"),
                                Created = reader["Questions_Created"].ToString(),
                                Modified = reader["Questions_Modified"].ToString(),
                                QuizId = Convert.ToInt32(reader["Questions_QuizzesId"])
                            };

                            list.Add(_quizQuestion);
                        }
                    }
                    _conn.Close();
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return list;
        }

        public List<QuizQuestion> GetQuizQuestionsByQuizId(int QuizId)
        {
            List<QuizQuestion> list = new List<QuizQuestion>();

            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Questions WHERE Questions_QuizzesId = {QuizId}", _conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        QuizQuestion _quizQuestion = new QuizQuestion
                        {
                            Id = Convert.ToInt32(reader["Questions_Id"]),
                            Question = reader["Questions_Question"].ToString(),
                            IsActive = reader.GetBoolean("Questions_IsActive"),
                            Created = reader["Questions_Created"].ToString(),
                            Modified = reader["Questions_Modified"].ToString(),
                            QuizId = Convert.ToInt32(reader["Questions_QuizzesId"])
                        };

                        list.Add(_quizQuestion);
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

        public List<QuizQuestion> GetAllActiveQuizQuestions()
        {
            List<QuizQuestion> list = new List<QuizQuestion>();

            try
            {
                //using (MySqlConnection conn = GetConnection())
                //{
                //    conn.Open();
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Questions WHERE Questions_IsActive = 1", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuizQuestion _quizQuestion = new QuizQuestion
                            {
                                Id = Convert.ToInt32(reader["Questions_Id"]),
                                Question = reader["Questions_Question"].ToString(),
                                IsActive = reader.GetBoolean("Questions_IsActive"),
                                Created = reader["Questions_Created"].ToString(),
                                Modified = reader["Questions_Modified"].ToString(),
                                QuizId = Convert.ToInt32(reader["Questions_QuizzesId"])
                            };

                            list.Add(_quizQuestion);
                        }
                    }
                    _conn.Close();
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return list;
        }

        public QuizQuestion GetQuizQuestionById(int Id)
        {
            try
            {
                //using (MySqlConnection conn = GetConnection())
                //{
                //    conn.Open();
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Questions WHERE Questions_Id = '{Id}'", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new QuizQuestion()
                                {
                                    Id = Convert.ToInt32(reader["Questions_Id"]),
                                    Question = reader["Questions_Question"].ToString(),
                                    IsActive = reader.GetBoolean("Questions_IsActive"),
                                    Created = reader["Questions_Created"].ToString(),
                                    Modified = reader["Questions_Modified"].ToString(),
                                    QuizId = Convert.ToInt32(reader["Questions_QuizzesId"])
                                };
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    _conn.Close();
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public QuizQuestion GetQuestionDataByQuestion(QuizQuestion Question)
        {
            try
            {
                //using (MySqlConnection conn = GetConnection())
                //{
                //    conn.Open();

                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                // Must find a way to allow quote and apostraphes in questions without SQL error
                // To get this method to succeed, the front end application will search string and apply a \ before posting.
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Questions WHERE Questions_Question = \"{Question.Question}\"", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new QuizQuestion()
                                {
                                    Id = Convert.ToInt32(reader["Questions_Id"]),
                                    Question = reader["Questions_Question"].ToString(),
                                    IsActive = reader.GetBoolean("Questions_IsActive"),
                                    Created = reader["Questions_Created"].ToString(),
                                    Modified = reader["Questions_Modified"].ToString(),
                                    QuizId = Convert.ToInt32(reader["Questions_QuizzesId"])
                                };
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    _conn.Close();
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public QuizQuestion CreateNewQuizQuestion(QuizQuestion Question)
        {
            try
            {
                //using (MySqlConnection conn = GetConnection())
                //{
                //    conn.Open();
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                // For MVP new question IsActive will be default posted as true, after MVP development will accommodate for Admin to select active questions from a list of all questions in DB
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO Questions (Questions_Question, Questions_IsActive, Questions_QuizzesId) " +
                        $"VALUES (@Questions_Question, @Questions_IsActive, @Questions_QuizId)", _conn);

                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@Questions_Question", $"{Question.Question}");
                        cmd.Parameters.AddWithValue("@Questions_IsActive", $"{Convert.ToInt32(Question.IsActive)}");
                    cmd.Parameters.AddWithValue("@Questions_QuizId", $"{Question.QuizId}");
                        cmd.ExecuteNonQuery();
                    }
                    _conn.Close();
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return GetQuestionDataByQuestion(Question);
        }

        public bool isNewQuestionCreationSuccessful(int QuestionId)
        {
            bool hasRows = false;

            try
            {

                //using (MySqlConnection conn = GetConnection())
                //{
                //    conn.Open();
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Questions WHERE Questions_Id = '{QuestionId}'", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        hasRows = reader.HasRows;
                    }
                    _conn.Close();
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return hasRows;
        }
    }
}
