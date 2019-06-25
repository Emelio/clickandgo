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
        private readonly IVehicle _vehicleRepository;

        public AdminController(IConfiguration config, IUsers userRepository, IDriver driverRepository, IVehicle vehicleRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _driverRepository = driverRepository;
            _vehicleRepository = vehicleRepository;
        }

        [Route("api/admin/getAllOwners")]
        [HttpGet]
        public async Task<IActionResult> GetAllOwner()
        {

            List<Users> users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [Route("api/admin/getAllDrivers/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers(string id)
        {
            List<Driver> drivers = await  _driverRepository.GetDriverList(id);
            return Ok(drivers);
        }

        [Route("api/admin/getSingleDriver/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSingleDriver(string id)
        {
            List<Driver> driver = await _driverRepository.GetDriver(id);
            return Ok(driver);
        }

        [Route("api/admin/getSingleOwner/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSingleOwner(string id)
        {
            Users user = await _userRepository.CheckUserById(id);
            return Ok(user);
        }

        [Route("api/admin/getSingleVehicle/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSingleVehicle(string id)
        {
            Vehicle vehicle = await _vehicleRepository.GetVehicleSingle(id);
            return Ok(vehicle);
        }
    }
}