using clickandgo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.Data
{
    public interface IVehicle
    {
        Task<bool> AddVehicle(Vehicle vehicle);

        Task<List<Vehicle>> GetVehicleDataAsync(string id);

        Task<Vehicle> GetVehicleSingle(string id);

        Task<dynamic> RemoveVehicle(string id);

        Task<bool> UpdateVehicle(Vehicle vehicle, string id);
        Task<bool> RemoveOwnerVehicles(string ownerId);
    }
}
