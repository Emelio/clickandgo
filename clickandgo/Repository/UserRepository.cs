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

        public async Task<dynamic> SetVerificationCode(string code, string email)
        {
            var filter = Builders<Users>.Filter.Eq(x => x.Email, email);
            var update = Builders<Users>.Update.Set(x => x.VerificationCode, code).Set(x => x.Verified, "false");
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

        public async Task<List<Users>> GetAllUsers()
        {
            List<Users> user = await _context.Users.Find(x => x.Type == "owner").ToListAsync();
            return user; 
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
    }
}
