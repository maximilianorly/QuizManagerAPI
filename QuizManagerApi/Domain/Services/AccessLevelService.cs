using System;
using Microsoft.AspNetCore.Http;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models.AccessLevel;

namespace QuizManagerApi.Domain.Services
{
    public class AccessLevelService
    {
        public HttpContext HttpContext { get; }

        public AccessLevelService(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        public IEnumerable<AccessLevel> GetAllAccessLevels()
        {
            AccessLevelConnection _accessLevelConnection = HttpContext.RequestServices.GetService(typeof(QuizManagerApi.Domain.Connections.AccessLevelConnection)) as AccessLevelConnection;

            var _accessLevels = _accessLevelConnection.GetAllAccessLevels();

            return _accessLevels;
        }
    }
}
