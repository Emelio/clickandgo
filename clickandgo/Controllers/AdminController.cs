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
using AutoMapper;

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

        [Route("api/admin/getAllAdmins")]
        [HttpGet]
        public async Task<IActionResult> GetAllAdmin()
        {
            var adminList = new List<AdminDto>();
            List<Users> users = await _userRepository.GetAdmins();
            foreach (var user in users)
            {
              adminList.Add(new AdminDto(){Id=user._id.ToString(),Name=$"{user.FirstName} {user.LastName}"});
            }
            return Ok(adminList);
        }

        [Route("api/admin/confirmCode/{code}")]
        [HttpPost]
        public IActionResult CheckConfirmationCode(string code){
           var confirmCode= _config.GetSection("AdminCode").Value;

           if(code==confirmCode){
               return Ok(true);
           }

           return BadRequest("Wrong Code");
        }

        [Route("api/admin/registerAdmin")]
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto register){
                
            Users user = await _userRepository.CheckUser(register.Email);

            if (user == null)
            {
                Users userData = new Users();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RegisterDto, Users>();
                });
                IMapper mapper = new Mapper(config);
                Users dest = mapper.Map<RegisterDto, Users>(register);
                
                await _userRepository.CreateUser(dest, register.Password, "admin");
                return Ok(true);

            }
            return BadRequest();
        }
        [Route("api/admin/getAllDrivers/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers(string id)
        {
            List<Driver> drivers = await  _driverRepository.GetDriverList(id);
            return Ok(drivers);
        }

        //[AllowAnonymous]
        [Route("api/admin/setApprovedStatus/{id}/{status}")]
        [HttpPost]
        public async Task<IActionResult> ChangeApprovalStatus(string id, string status)
        {
             Users user = await _userRepository.CheckUserById(id);
             if(user !=null){
                 if(await _userRepository.UpdateApprovalStatus(id,status)){
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

        [Route("api/admin/removeDriver/{id}")]
        [HttpPost]
        public async Task<IActionResult> RemoveOwner(string id)
        {
            var user = await  _userRepository.CheckUserById(id);

            if(user != null)
            {
                if( await _driverRepository.RemoveOwnerDrivers(user._id.ToString()) && await _vehicleRepository.RemoveOwnerVehicles(user._id.ToString()))
                {
                    if( await _userRepository.DeleteUser(id))
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Failed to delete Owner");
                    }

                }
                else
                {
                    return BadRequest("Failed to delete Driver");
                }
            }
            return BadRequest("Failed");
        }

        [Route("api/admin/removeAdmin/{id}")]
        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            var user = await  _userRepository.CheckUserById(id);

            if(user != null)
            {
                
                    if( await _userRepository.DeleteUser(id))
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Failed to delete Admin");
                    }
            }
            return BadRequest("Failed");
        }
    }
}