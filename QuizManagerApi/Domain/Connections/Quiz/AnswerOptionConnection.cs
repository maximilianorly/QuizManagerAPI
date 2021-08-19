using QuizManagerApi.Domain.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizManagerApi.Domain.Connections
{
    public class AnswerOptionConnection
    {
        //public string ConnectionString { get; set; }


        //public AnswerOptionConnection(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}


        //private MySqlConnection GetConnection()
        //{
        //    return new MySqlConnection("Server=localhost; port=3306; Database=QuizManager; Uid=root; Pwd=password");
        //}

        private readonly MySqlConnection _conn;

        public AnswerOptionConnection(MySqlConnection conn)
        {
            _conn = conn;
        }

        public List<AnswerOption> GetAllAnswerOptions()
        {
            List<AnswerOption> list = new List<AnswerOption>();

            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Question_AnswerOptions", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AnswerOption _answerOption = new AnswerOption
                            {
                                Id = Convert.ToInt32(reader["AnswerOption_Id"]),
                                QuestionId = Convert.ToInt32(reader["Questions_Questions_Id"]),
                                IsCorrectOption = reader.GetBoolean("AnswerOption_IsCorrectOption"),
                                Option = reader["AnswerOption_Option"].ToString(),
                                Created = reader["AnswerOptions_Created"].ToString(),
                                Modified = reader["AnswerOptions_Modified"].ToString()
                            };

                            list.Add(_answerOption);
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

        public List<AnswerOption> GetAllAnswerOptionsForQuestion(int QuestionId)
        {
            List<AnswerOption> list = new List<AnswerOption>();

            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Question_AnswerOptions WHERE Questions_Questions_Id = {QuestionId}", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                AnswerOption _answerOption = new AnswerOption
                                {
                                    Id = Convert.ToInt32(reader["AnswerOption_Id"]),
                                    QuestionId = Convert.ToInt32(reader["Questions_Questions_Id"]),
                                    IsCorrectOption = reader.GetBoolean("AnswerOption_IsCorrectOption"),
                                    Option = reader["AnswerOption_Option"].ToString(),
                                    Created = reader["AnswerOptions_Created"].ToString(),
                                    Modified = reader["AnswerOptions_Modified"].ToString()
                                };

                                list.Add(_answerOption);
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
            return list;
        }

        public AnswerOption GetAnswerOptionDataByAnswerOption(AnswerOption NewAnswerOption)
        {
            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                // Must find a way to allow quote and apostraphes in questions without SQL error
                // To get this method to succeed, the front end application will search string and apply a \ before posting.
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Question_AnswerOptions WHERE AnswerOption_Option = \"{NewAnswerOption.Option}\"", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new AnswerOption()
                                {
                                    Id = Convert.ToInt32(reader["AnswerOption_Id"]),
                                    QuestionId = Convert.ToInt32(reader["Questions_Questions_Id"]),
                                    IsCorrectOption = reader.GetBoolean("AnswerOption_IsCorrectOption"),
                                    Option = reader["AnswerOption_Option"].ToString(),
                                    Created = reader["AnswerOptions_Created"].ToString(),
                                    Modified = reader["AnswerOptions_Modified"].ToString()
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

        public AnswerOption CreateNewAnswerOption(AnswerOption NewAnswerOption)
        {
            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                // For MVP new question IsActive will be default posted as true, after MVP development will accommodate for Admin to select active questions from a list of all questions in DB
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO Question_AnswerOptions (Questions_Questions_Id, AnswerOption_IsCorrectOption, AnswerOption_Option) " +
                        $"VALUES (@Questions_Questions_Id, @AnswerOption_IsCorrectOption, @AnswerOption_Option)", _conn);

                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@Questions_Questions_Id", $"{NewAnswerOption.QuestionId}");
                        cmd.Parameters.AddWithValue("@AnswerOption_IsCorrectOption", $"{Convert.ToInt32(NewAnswerOption.IsCorrectOption)}");
                        cmd.Parameters.AddWithValue("@AnswerOption_Option", $"{NewAnswerOption.Option}");
                        cmd.ExecuteNonQuery();
                    }
                    _conn.Close();
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return GetAnswerOptionDataByAnswerOption(NewAnswerOption);
        }

        public bool isNewAnswerOptionCreationSuccessful(int AnswerOptionId)
        {
            bool hasRows = false;

            try
            {

                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Question_AnswerOptions WHERE AnswerOption_Id = {AnswerOptionId}", _conn);

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
