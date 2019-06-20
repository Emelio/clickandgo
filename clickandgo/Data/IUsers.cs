using clickandgo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clickandgo.Data
{
    public interface IUsers
    {
        Task<bool> CreateUser(Users users, string password, string type);

        Task<Users> CheckUserById(string id);

        Task<Users> LoginUser(string email, string password);

        Task<List<Users>> GetAllUsers();


        Task<Users> UpdateUserAddress(Address address);

        Task<bool> UpdateStage(string stage, string id);

        Task<bool> UpdateUserMainAsync(string type, Users data);

        Task<Users> CheckUser(string email);
    }
}
