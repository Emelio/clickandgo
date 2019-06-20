using clickandgo.Data;
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

        public async Task<List<Driver>> GetDriver(string id)
        {
            List<Driver> driver = await _context.Driver.Find(x => x.PrimaryId == id).ToListAsync();
            return driver; 
        }

        public async Task<dynamic> RemoveDriver(string id)
        {
            var result = await _context.Driver.DeleteOneAsync(x => x._id == ObjectId.Parse(id));
            return result;
        }
    }
}
