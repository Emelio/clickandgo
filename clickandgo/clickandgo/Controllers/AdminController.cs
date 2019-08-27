using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clickandgo.Data;
using clickandgo.dto;
using clickandgo.Models;
using Microsoft.AspNetCore.Authorization;
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

        //[AllowAnonymous]
        [Route("api/admin/setApprovedStatus/{id}")]
        [HttpPost]
        public async Task<IActionResult> ChangeApprovalStatus([FromBody]ApprovalStatusDto status, string id)
        {
             Users user = await _userRepository.CheckUserById(id);
             if(user !=null){
                 if(await _userRepository.UpdateApprovalStatus(id,status.Status)){
                     return Ok();
                 }else{
                     return BadRequest("failed to update status");
                 }
             }
             return BadRequest("user not found");
        }
        [Route("api/admin/getSingleDriver/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSingleDriver(string id)
        {
            Driver driver = await _driverRepository.GetDriver(id);
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

        [Route("api/admin/getAllVehicles/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles(string id)
        {
            List<Vehicle> cars = await _vehicleRepository.GetVehicleDataAsync(id);
            return Ok(cars);
        }

        [Route("api/admin/removeOwner/{id}")]
        [HttpGet]
        public async Task<IActionResult> RemoveOwner(string id)
        {
            var user = await  _userRepository.CheckUserById(id);

            // get all drivers and delete them 

            if (user != null)
            {
                List<Vehicle> cars = await _vehicleRepository.GetVehicleDataAsync(user._id.ToString());
                List<Driver> drivers = await _driverRepository.GetDriverList(user._id.ToString());


                foreach (var item in cars)
                {
                    await _vehicleRepository.RemoveVehicle(item._id.ToString());
                }

                foreach (var item in drivers)
                {
                    await _driverRepository.RemoveOwnerDrivers(item._id.ToString()); 
                }

                if (await _userRepository.DeleteUser(id))
                {
                    return Ok(new { Updated = "updated"}) ;
                }
                else
                {
                    return BadRequest("Failed to delete Owner");
                }
            }

            // get all cars and remove them 



            return BadRequest("Failed");
        }
    }
}