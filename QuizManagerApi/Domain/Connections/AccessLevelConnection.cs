using QuizManagerApi.Domain.Models.AccessLevel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizManagerApi.Domain.Connections
{
    public class AccessLevelConnection
    {
        public string ConnectionString { get; set; }


        public AccessLevelConnection(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("Server=localhost; port=3306; Database=QuizManager; Uid=root; Pwd=password");
        }

        public List<AccessLevel> GetAllAccessLevels()
        {
            List<AccessLevel> list = new List<AccessLevel>();

            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM AccessLevels", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            AccessLevel _accessLevel = new AccessLevel
                            {
                                AccessLevelId = Convert.ToInt32(reader["AccessLevels_Id"]),
                                Description = reader["AccessLevels_Description"].ToString()
                            };

                            list.Add(_accessLevel);
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
    }
}
