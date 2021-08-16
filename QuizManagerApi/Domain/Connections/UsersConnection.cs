using QuizManagerApi.Domain.Models.User;
using QuizManagerApi.Domain.Models.UserHasAccess;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizManagerApi.Domain.Connections
{
    public class UsersConnection
    {
        private readonly MySqlConnection _conn;

        public UsersConnection(MySqlConnection conn)
        {
            _conn = conn;
        }


        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();

            try
            {
                _conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users", _conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        User _user = new User
                        {
                            Id = Convert.ToInt32(reader["Users_Id"]),
                            FirstName = reader["Users_FirstName"].ToString(),
                            LastName = reader["Users_LastName"].ToString(),
                            UserName = reader["Users_Username"].ToString(),
                            Password = reader["Users_Password"].ToString()
                        };

                        list.Add(_user);
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


        public User GetUserById(int id)
        {

            try
            {
                    _conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE Users_Id = {id}", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new User()
                                {
                                    Id = Convert.ToInt32(reader["Users_Id"]),
                                    FirstName = reader["Users_FirstName"].ToString(),
                                    LastName = reader["Users_LastName"].ToString(),
                                    UserName = reader["Users_Username"].ToString(),
                                    Password = reader["Users_Password"].ToString()
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

        public User GetUserByUsername(string Username)
        {

            try
            {
                    _conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE Users_Username = '{Username}'", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new User()
                                {
                                    Id = Convert.ToInt32(reader["Users_Id"]),
                                    FirstName = reader["Users_FirstName"].ToString(),
                                    LastName = reader["Users_LastName"].ToString(),
                                    UserName = reader["Users_Username"].ToString(),
                                    Password = reader["Users_Password"].ToString()
                                };
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public bool IsExistingUser(string Username)
        {
            bool hasRows = false;

            try
            {
                    _conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE Users_Username = '{Username}'", _conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        hasRows = reader.HasRows;
                    }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return hasRows;
        }

        public User CreateUser(User NewUser)
        {
            try
            {
                _conn.Open();
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO Users (Users_FirstName, Users_LastName, Users_Username, Users_Password) " +
                    $"VALUES (@Users_FirstName, @Users_LastName, @Users_Username, @Users_Password)", _conn);

                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@Users_FirstName", $"{NewUser.FirstName}");
                    cmd.Parameters.AddWithValue("@Users_LastName", $"{NewUser.LastName}");
                    cmd.Parameters.AddWithValue("@Users_Username", $"{NewUser.UserName}");
                    cmd.Parameters.AddWithValue("@Users_Password", $"{NewUser.Password}");
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return GetUserByUsername(NewUser.UserName);
        }
    }
}
