using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clickandgo.Data;
using clickandgo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace clickandgo.Controllers
{

    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsers _userRepository;
        private readonly IDriver _driverRepository;

        public AdminController(IConfiguration config, IUsers userRepository, IDriver driverRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _driverRepository = driverRepository;
        }

        [Route("api/admin/getAllOwners")]
        [HttpGet]
        public async Task<IActionResult> GetAllOwner()
        {

            List<Users> users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [Route("api/admin/getSingleDriver/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSingleDriver(string id)
        {
            List<Driver> driver = await _driverRepository.GetDriver(id);
            return Ok(driver);
        }
    }
}