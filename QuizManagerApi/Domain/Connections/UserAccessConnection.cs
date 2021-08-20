using QuizManagerApi.Domain.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizManagerApi.Domain.Connections
{
    public class UserAccessConnection
    {
        private readonly MySqlConnection _conn;

        public UserAccessConnection(MySqlConnection conn)
        {
            _conn = conn;
        }


        public UserHasAccess GetUserAccessByUserId(int UserId)
        {

            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM UserHasAccess WHERE Users_Users_Id = {UserId}", _conn);

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
                    _conn.Close();
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
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO UserHasAccess (AccessLevels_AccessLevels_Id, Users_Users_Id) " +
                        $"VALUES (@AccessLevels_AccessLevels_Id, @Users_Users_Id)", _conn);

                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@AccessLevels_AccessLevels_Id", $"{AccessLevel}");
                        cmd.Parameters.AddWithValue("@Users_Users_Id", $"{NewUserId}");
                        cmd.ExecuteNonQuery();
                    }
                    _conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return GetUserAccessByUserId(NewUserId);
        }
    }
}
