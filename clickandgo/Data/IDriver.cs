﻿using clickandgo.dto;
using clickandgo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.Data
{
    public interface IDriver
    {
        Task<bool> CreateDriver(Driver driver);

        Task<List<Driver>> GetDriverList(string id);

        Task<dynamic> RemoveDriver(string id);

        Task<Driver> GetDriverByTrn(string trn);

        Task<bool> UpdatePassword(string email, string password);

        Task<Driver> LoginDriver(string trn, string password);

 

        Task<bool> updateDriverCode(int code, string trn);

        Task<dynamic> AssignCar(string carId, string driverId);

        Task<Driver> GetDriver(string id);
        Task<bool> UpdateDriver(UpdateDriver driver);

        Task<bool> RemoveOwnerDrivers(string primaryId);
    }
}
