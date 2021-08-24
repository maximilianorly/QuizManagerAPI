using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using QuizManagerApi.Domain.Models.AccessLevel;
using QuizManagerApi.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class AccessLevelsController : Controller
    {

        public AccessLevelService _accessLevelService;

        public AccessLevelsController(MySqlConnection conn)
        {
            _accessLevelService = new AccessLevelService(conn);
        }
        // GET: api/AccessLevels
        [HttpGet]
        public IEnumerable<AccessLevel> Get()
        {

            var _accessLevels = _accessLevelService.GetAllAccessLevels();

            return _accessLevels;
        }
    }
}
