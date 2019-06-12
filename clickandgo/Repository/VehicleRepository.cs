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
    public class VehicleRepository : IVehicle
    {
        private readonly DataContext _context;
        

        public VehicleRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<bool> AddVehicle(Vehicle vehicle)
        {
            await _context.Vehicle.InsertOneAsync(vehicle);
            return true;
        }

        public async Task<List<Vehicle>> GetVehicleDataAsync(string id)
        {
            List<Vehicle> vehicleData = await _context.Vehicle.Find(x => x.OwnerId == id).ToListAsync();
            return vehicleData;
        }

        public async Task<Vehicle> GetVehicleSingle(string id)
        {
            Vehicle vehicle = await _context.Vehicle.Find(x => x._id == ObjectId.Parse(id)).FirstOrDefaultAsync();
            return vehicle;
        }

        public async Task<dynamic> RemoveVehicle(string id)
        {
            var result = await _context.Vehicle.DeleteOneAsync(x => x._id == ObjectId.Parse(id));
            return result;
        }

        public async Task<bool> UpdateVehicle(Vehicle vehicle, string id)
        {
            var filter = Builders<Vehicle>.Filter.Eq(x => x._id, ObjectId.Parse(id));
            var result = await _context.Vehicle.ReplaceOneAsync(filter, vehicle);

            return true;
        }
    }
}
