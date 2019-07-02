using clickandgo.Data;
using clickandgo.dto;
using clickandgo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.Repository
{
    public class DriverRepository : IDriver
    {
        private readonly DataContext _context;

        public DriverRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<dynamic> AssignCar(string carId, string driverId)
        {
            
            var update = Builders<Driver>.Update.Set(x => x.VehicleID, carId);
            var result = await _context.Driver.UpdateOneAsync(x => x._id == ObjectId.Parse(driverId), update);
            return result;
        }

        public async Task<bool> UpdateDriver(UpdateDriver driver)
        {
            var filter = Builders<Driver>.Filter.Eq("_id", ObjectId.Parse(driver.driverID));
            var update = Builders<Driver>.Update.Set("Address", driver.Address).Set("Class", driver.Class)
                                                .Set("Collectorate", driver.Collectorate)
                                                .Set("DateExpired", driver.DateExpired)
                                                .Set("DateIssued", driver.DateIssued)
                                                .Set("DOB", driver.DOB)
                                                .Set("Gender", driver.Gender)
                                                .Set("LicenseToDrive", driver.LicenseToDrive)
                                                .Set("FirstName", driver.FirstName)
                                                .Set("LastName", driver.LastName)
                                                .Set("MiddleName", driver.MiddleName)
                                                .Set("PoliceRecordNumber", driver.PoliceRecordNumber)
                                                //.Set("PPV", driver.PPV)
                                                .Set("TimePoliceRecordIssue", driver.TimePoliceRecordIssue)
                                                .Set("Trn", driver.Trn);
            var result= await _context.Driver.UpdateOneAsync(filter, update);
            return result.IsAcknowledged;
        }

        public async Task<bool> CreateDriver(Driver driver)
        {
            await _context.Driver.InsertOneAsync(driver);
            return true;
        }

        public async Task<List<Driver>> GetDriverList(string id)
        {
            List<Driver> drivers = await _context.Driver.Find(x => x.PrimaryId == id).ToListAsync();
            return drivers;
        }

        public async Task<Driver> GetDriver(string id)
        {
            Driver driver = await _context.Driver.Find(x => x._id == ObjectId.Parse(id)).FirstOrDefaultAsync();
            return driver; 
        }

        public async Task<dynamic> RemoveDriver(string id)
        {
            var result = await _context.Driver.DeleteOneAsync(x => x._id == ObjectId.Parse(id));
            return result;
        }
    }
}
