﻿using clickandgo.Data;
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
    public class UserRepository : IUsers
    {
        private readonly DataContext _context;

        public UserRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<Users> CheckUser(string email)
        {
            Users user = await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<Users> CheckUserById(string id)
        {
            Users user = await _context.Users.Find(x => x._id == ObjectId.Parse(id)).FirstOrDefaultAsync();
            return user; 
        }

        public async Task<Users> CheckUserByUsername(string userD)
        {
            Users user = await _context.Users.Find(x => x.Stage == userD).FirstOrDefaultAsync();
            return user;
        }

        public async Task<Users> CheckUserByCode(string code)
        {
            Users user = await _context.Users.Find(x => x.VerificationCode == code).FirstOrDefaultAsync();
            return user;
        }

        public async Task<dynamic> SetVerificationCode(string code, string email)
        {
            var filter = Builders<Users>.Filter.Eq(x => x.Email, email);
            var update = Builders<Users>.Update.Set(x => x.VerificationCode, code).Set(x => x.Verified, "false").Set(x => x.Stage, "first");
            var result = await _context.Users.UpdateOneAsync(filter, update);
            return result;
        }

        public async Task<dynamic> SetVerificationCodeReset(string code, string email)
        {
            var filter = Builders<Users>.Filter.Eq(x => x.Email, email);
            var update = Builders<Users>.Update.Set(x => x.VerificationCode, code);
            var result = await _context.Users.UpdateOneAsync(filter, update);
            return result;
        }


        public async Task<dynamic> UpdateVerificationCode(string code, string email)
        {
            var filter = Builders<Users>.Filter.Eq(x => x.Email, email);
            var update = Builders<Users>.Update.Set(x => x.Verified, "true");
            var result = await _context.Users.UpdateOneAsync(filter, update);
            return result;
        }

        public async Task<bool> CreateUser(Users users, string password, string type)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            users.PasswordHash = passwordHash;
            users.PasswordSalt = passwordSalt;

            users.Type = type;//"owner";

            await _context.Users.InsertOneAsync(users);

            return true; 
        }

        public async Task<bool> UpdatePassword(string email,string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var filter = Builders<Users>.Filter.Eq(x => x.Email, email);
            var update = Builders<Users>.Update.Set(x => x.PasswordSalt, passwordSalt).Set(x=> x.PasswordHash,passwordHash);
            var result = await _context.Users.UpdateOneAsync(filter, update);

            return result.IsAcknowledged;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            List<Users> user = await _context.Users.Find(x => x.Type == "owner").ToListAsync();
            return user; 
        }

        public async Task<bool> UpdateApprovalStatus(string id, string status)
        {
        
            var filter = Builders<Users>.Filter.Eq(x => x._id, ObjectId.Parse(id));
            var update = Builders<Users>.Update.Set(x => x.ApprovalStatus,status);
            var result = await _context.Users.UpdateOneAsync(filter, update);

            return result.IsAcknowledged;
        }
        public async Task<Users> LoginUser(string email, string password)
        {
            Users user = await _context.Users.Find(x => x.Email == email).FirstOrDefaultAsync();

            if (user != null)
            {
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }
                else
                {

                    return user;
                }
            }
            else
            {
                user = await _context.Users.Find(x => x.Stage == email).FirstOrDefaultAsync();

                if (user != null)
                {
                    if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    {
                        return null;
                    }
                    else
                    {

                        return user;
                    }
                }
            }

            return null;
        }

        public async Task<bool> UpdateStage(string stage, string id)
        {
            var filter = Builders<Users>.Filter.Eq(x => x._id, ObjectId.Parse(id));
            var update = Builders<Users>.Update.Set(x => x.Stage, stage);
            var result = _context.Users.UpdateOneAsync(filter, update).Result;
            return true;
        }

        public Task<Users> UpdateUserAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUserMainAsync(string type, Users data)
        {
            var filter = Builders<Users>.Filter.Eq(s => s.Email, data.Email);
            var result = await _context.Users.ReplaceOneAsync(filter, data); 

            return true;
        }

        public async Task<bool> DeleteUser(string Id)
        {
            var filter = Builders<Users>.Filter.Eq(s => s._id, ObjectId.Parse(Id));
            var result =await _context.Users.DeleteOneAsync(filter);

            return result.IsAcknowledged;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {

                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            };

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }

                return true;
            };
        }

        public async Task<List<Users>> GetAdmins()
        {
            List<Users> user = await _context.Users.Find(x => x.Type == "admin").ToListAsync();
            return user; 
        }


    }
}
