using QuizManagerApi.Domain.Models.User;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizManagerApi.Domain.Connections
{
    public class UsersConnection
    {
        public string ConnectionString { get; set; }


        public UsersConnection(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        private MySqlConnection GetConnection()
        {
            //return new MySqlConnection(ConnectionString);
            return new MySqlConnection("Server=localhost; port=3306; Database=QuizManager; Uid=root; Pwd=password");
        }


        public List<User> GetAllUsers()
        //public string GetAllUsers()
        {
            List<User> list = new List<User>();

            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users", conn);
                    //conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            User user = new User
                            {
                                Id = Convert.ToInt32(reader["Users_Id"]),
                                FirstName = reader["Users_FirstName"].ToString(),
                                LastName = reader["Users_LastName"].ToString(),
                                UserName = reader["Users_Username"].ToString(),
                                Password = reader["Users_Password"].ToString()
                            };

                            list.Add(user);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return list;
            //return "Hello there";
        }


        public User GetUserById(int id)
        {

            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE Users_Id = {id}", conn);

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
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE Users_Username = '{Username}'", conn);

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
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public List<User> CreateUser(User NewUser)
        {
            try
            {
                //bool exists = ExistingCompany(company.company_name);

                //if (!exists)
                //{
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand($"INSERT INTO Users (Users_FirstName, Users_LastName, Users_Username, Users_Password) " +
                            $"VALUES (@Users_FirstName, @Users_LastName, @Users_Username, @Users_Password)", conn);

                        using (cmd)
                        {
                            cmd.Parameters.AddWithValue("@Users_FirstName", $"{NewUser.FirstName}");
                            cmd.Parameters.AddWithValue("@Users_LastName", $"{NewUser.LastName}");
                            cmd.Parameters.AddWithValue("@Users_Username", $"{NewUser.UserName}");
                            cmd.Parameters.AddWithValue("@Users_Password", $"{NewUser.Password}");
                            cmd.ExecuteNonQuery();
                        }

                    }
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return GetAllUsers();
        }
    }
}
