using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using clickandgo.Data;
using clickandgo.dto;
using clickandgo.Helper;
using clickandgo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace clickandgo.Controllers
{
    [Authorize]
    public class OwnerController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsers _userRepository;
        private readonly IVehicle _vehicleRepository;
        private readonly IDriver _driverRepository;
        private readonly TokenHelper _tokenHelper = new TokenHelper();

        public OwnerController(IUsers userRepository, IConfiguration config, IVehicle vehicleRepository, IDriver driverRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
        }

        [Route("api/owner/addCar")]
        [HttpPost]
        public async Task<IActionResult> createCar([FromBody]Vehicle vehicle)
        {
            var token = Request.Headers["Authorization"];
            string id = _tokenHelper.getUserFromToken(token);

            vehicle.OwnerId = id;

            await _vehicleRepository.AddVehicle(vehicle);

            Users user = await _userRepository.CheckUserById(id);

            if (user != null)
            {
                if (user.Stage == "first")
                {
                    await _userRepository.UpdateStage("second", id);
                    return Ok(new { status = "update Stage" });
                }
                if (user.Stage == "second") {
                    await _userRepository.UpdateStage("third", id);
                    return Ok(new { status = "update Stage" });
                }
            }

           
            return Ok(new { status = "updated"}); 
        }

        [Route("api/owner/updateCar")]
        [HttpPost]
        public async Task<IActionResult> updateCar([FromBody]VehicleDto vehicle)
        {

            Vehicle car = await _vehicleRepository.GetVehicleSingle(vehicle.vehicleId);

            if(vehicle.VehicleType != null){ car.VehicleType = vehicle.VehicleType; }
            if (vehicle.Make != null){ car.Make = vehicle.Make; }
            if (vehicle.Year != null){ car.Year = vehicle.Year; }
            if (vehicle.Model != null){ car.Model = vehicle.Model; }
            if (vehicle.Color != null){ car.Color = vehicle.Color; }
            if (vehicle.BodyType != null){ car.BodyType = vehicle.BodyType; }
            if (vehicle.EngineNumber != null){ car.EngineNumber = vehicle.EngineNumber; }
            if (vehicle.ChassisNumber != null){ car.ChassisNumber = vehicle.ChassisNumber; }
            if (vehicle.RegIssue != null){ car.RegIssue = vehicle.RegIssue; }
            if (vehicle.FitIssue != null){ car.FitIssue = vehicle.FitIssue; }
            if (vehicle.FitExpiry != null){ car.FitExpiry = vehicle.FitExpiry; }
            if (vehicle.InsuranceName != null){ car.InsuranceName = vehicle.InsuranceName; }
            if (vehicle.PolicyNumber != null){ car.PolicyNumber = vehicle.PolicyNumber; }
            if (vehicle.PolicyIssueDate != null){ car.PolicyIssueDate = vehicle.PolicyIssueDate; }
            if (vehicle.PolicyExpiryDate != null){ car.PolicyExpiryDate = vehicle.PolicyExpiryDate; }

            await _vehicleRepository.UpdateVehicle(car, vehicle.vehicleId);

            return Ok(new { status = "Updated!"});
        }

        
        [Route("api/owner/updateDriver")]
        [HttpPost]
        public async Task<IActionResult> UpdateDriverData([FromBody]UpdateDriver driverDto)
        {

            if(await _driverRepository.UpdateDriver(driverDto))
                return Ok();
            return BadRequest();
        }

        [Route("api/owner/checkStage")]
        [HttpGet]
        public async Task<IActionResult> checkStage()
        {
            var token = Request.Headers["Authorization"];
            string id = _tokenHelper.getUserFromToken(token);

            Users user = await _userRepository.CheckUserById(id); 
            return Ok(user);
        }

        [Route("api/owner/getunits")]
        [HttpGet]
        public async Task<IActionResult> GetUnits()
        { 

        var token = Request.Headers["Authorization"];
        string id = _tokenHelper.getUserFromToken(token);

        List<Vehicle> v = await _vehicleRepository.GetVehicleDataAsync(id);

            return Ok(v);
        }

        [Route("api/owner/removeVehicle/{id}")]
        [HttpGet]
        public async Task<IActionResult> RemoveVehicle(string id)
        {
            var result = await _vehicleRepository.RemoveVehicle(id);
            return Ok(result);
        }

        
        [Route("api/owner/addDriver")]
        [HttpPost]
        public async Task<IActionResult> AddDriver([FromBody] AddDriverDto addDriver)
        {
            var token = Request.Headers["Authorization"];
            string id = _tokenHelper.getUserFromToken(token);

            Driver driver = new Driver();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddDriverDto, Driver>();
            });
            IMapper mapper = new Mapper(config);
            driver = mapper.Map<AddDriverDto, Driver>(addDriver);

            driver.PrimaryId = id;

            await _driverRepository.CreateDriver(driver);

            Users user = await _userRepository.CheckUserById(id);

            user.Stage = "final";

            await _userRepository.UpdateUserMainAsync(user.Email, user);

            return Ok(new { status = "created" });
        }

        [Route("api/owner/getDriverList")]
        [HttpGet]
        public async Task<IActionResult> GetDriverList()
        {
            var token = Request.Headers["Authorization"];
            string id = _tokenHelper.getUserFromToken(token);

            List<Driver> drivers = await _driverRepository.GetDriverList(id);

            return Ok(drivers);
        }

        [Route("api/owner/removeDriver/{id}")]
        [HttpGet]
        public async Task<IActionResult> RemoveDriver(string id)
        {
            var result = await _driverRepository.RemoveDriver(id); 
            return Ok(result);
        }


        [Route("api/owner/assignDriver/{driverId}/{carId}")]
        [HttpGet]
        public async Task<IActionResult> AssignDriver(string driverId, string carId)
        {
            var result = await _driverRepository.AssignCar(carId, driverId);
            return Ok(result);
        } 
    }
}