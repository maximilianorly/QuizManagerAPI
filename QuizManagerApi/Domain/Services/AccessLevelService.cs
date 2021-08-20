using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models.AccessLevel;
using MySql.Data.MySqlClient;

namespace QuizManagerApi.Domain.Services
{
    public class AccessLevelService
    {
        private readonly AccessLevelConnection _accessLevelConnection;

        public AccessLevelService(MySqlConnection conn)
        {
            _accessLevelConnection = new AccessLevelConnection(conn);
        }

        public IEnumerable<AccessLevel> GetAllAccessLevels()
        {

            var _accessLevels = _accessLevelConnection.GetAllAccessLevels();

            return _accessLevels;
        }
    }
}
