using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models.AccessLevel;
using MySql.Data.MySqlClient;
using QuizManagerApi.Domain.Enums;

namespace QuizManagerApi.Domain.Services
{
    public class AccessLevelService
    {
        private readonly AccessLevelConnection _accessLevelConnection;
        private UserService _userService;

        public AccessLevelService(MySqlConnection conn)
        {
            _accessLevelConnection = new AccessLevelConnection(conn);
            _userService = new UserService(conn);
        }

        public IEnumerable<AccessLevel> GetAllAccessLevels()
        {

            var _accessLevels = _accessLevelConnection.GetAllAccessLevels();

            return _accessLevels;
        }

        public bool IsAccessRestricted(int UserId)
        {
            bool _isRestricted = true;
            int _userAccessLevel = _userService.GetUserAccessByUserId(UserId).AccessLevelId;
            int _restrictedAccessLevel = (int)UserAccessEnum.Restricted;

            if (_userAccessLevel != _restrictedAccessLevel)
            {
                _isRestricted = false;
            }

            return _isRestricted;
        }
    }
}
