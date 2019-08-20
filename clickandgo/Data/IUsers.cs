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

        Task<dynamic> UpdateVerificationCode(string code, string email);

        Task<dynamic> SetVerificationCode(string code, string email);

        Task<dynamic> SetVerificationCodeReset(string code, string email);

        Task<Users> CheckUserByCode(string code);

        Task<Users> CheckUserByUsername(string userD);

        Task<Users> UpdateUserAddress(Address address);

        Task<bool> UpdateStage(string stage, string id);

        Task<bool> UpdateUserMainAsync(string type, Users data);

        Task<Users> CheckUser(string email);

        Task<bool> UpdatePassword(string email, string password);

        Task<bool> DeleteUser(string Id);

        Task<bool> UpdateApprovalStatus(string id, string status);

    }
}
