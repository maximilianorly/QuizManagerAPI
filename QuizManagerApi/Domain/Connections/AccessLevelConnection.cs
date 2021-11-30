using QuizManagerApi.Domain.Models.AccessLevel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizManagerApi.Domain.Connections
{
    public class AccessLevelConnection
    {
        private readonly MySqlConnection _conn;

        public AccessLevelConnection(MySqlConnection conn)
        {
            _conn = conn;
        }

        public List<AccessLevel> GetAllAccessLevels()
        {
            List<AccessLevel> list = new List<AccessLevel>();

            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM AccessLevels", _conn);

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
