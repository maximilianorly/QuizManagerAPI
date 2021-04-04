using QuizManagerApi.Domain.Models.QuizQuestion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizManagerApi.Domain.Connections
{
    public class QuestionConnection
    {
        public string ConnectionString { get; set; }


        public QuestionConnection(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("Server=localhost; port=3306; Database=QuizManager; Uid=root; Pwd=password");
        }

        public List<QuizQuestion> GetAllQuizQuestions()
        {
            List<QuizQuestion> list = new List<QuizQuestion>();

            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Questions", conn);

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
                                Modified = reader["Questions_Modified"].ToString()
                            };

                            list.Add(_quizQuestion);
                        }
                    }
                }
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
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Questions WHERE Questions_Id = '{Id}'", conn);

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
                                    Modified = reader["Questions_Modified"].ToString()
                                };
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
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
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    // Must find a way to allow quote and apostraphes in questions without SQL error
                    // To get this method to succeed, the front end application will search string and apply a \ before posting.
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Questions WHERE Questions_Question = \"{Question.Question}\"", conn);

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
                                    Modified = reader["Questions_Modified"].ToString()
                                };
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
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
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    // For MVP new question IsActive will be default posted as true, after MVP development will accommodate for Admin to select active questions from a list of all questions in DB
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO Questions (Questions_Question, Questions_IsActive) " +
                        $"VALUES (@Questions_Question, @Questions_IsActive)", conn);

                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@Questions_Question", $"{Question.Question}");
                        cmd.Parameters.AddWithValue("@Questions_IsActive", $"{Convert.ToInt32(Question.IsActive)}");
                        cmd.ExecuteNonQuery();
                    }
                }
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

                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Questions WHERE Questions_Id = '{QuestionId}'", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        hasRows = reader.HasRows;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return hasRows;
        }
    }
}
