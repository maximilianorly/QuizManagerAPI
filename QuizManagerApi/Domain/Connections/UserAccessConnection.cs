using QuizManagerApi.Domain.Models.User;
using QuizManagerApi.Domain.Models.UserHasAccess;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizManagerApi.Domain.Connections
{
    public class UserAccessConnection
    {
        public string ConnectionString { get; set; }


        public UserAccessConnection(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        private MySqlConnection GetConnection()
        {
            //return new MySqlConnection(ConnectionString);
            return new MySqlConnection("Server=localhost; port=3306; Database=QuizManager; Uid=root; Pwd=password");
        }


        public UserHasAccess GetUserAccessByUserId(int UserId)
        {

            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM UserHasAccess WHERE Users_Users_Id = {UserId}", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new UserHasAccess()
                                {
                                    Id = Convert.ToInt32(reader["UserHasAccess_Id"]),
                                    AccessLevelId = Convert.ToInt32(reader["AccessLevels_AccessLevels_Id"]),
                                    UserId = Convert.ToInt32(reader["Users_Users_Id"])
                                };
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public UserHasAccess MapNewUserToAccessLevel(int NewUserId, int AccessLevel)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO UserHasAccess (AccessLevels_AccessLevels_Id, Users_Users_Id) " +
                        $"VALUES (@AccessLevels_AccessLevels_Id, @Users_Users_Id)", conn);

                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@AccessLevels_AccessLevels_Id", $"{AccessLevel}");
                        cmd.Parameters.AddWithValue("@Users_Users_Id", $"{NewUserId}");
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return GetUserAccessByUserId(NewUserId);
        }
    }
}
