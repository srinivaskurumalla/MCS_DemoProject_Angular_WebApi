using MCS_DemoProject_Angular_WebApi.Interfaces;
using MCS_DemoProject_Angular_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace MCS_DemoProject_Angular_WebApi.Repositories
{
    public class UserRepo : IUsers<User>
    {

        private readonly MCSContext _dbContext;

        public UserRepo(MCSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(User obj)
        {
            if(obj == null)
            {
                return false;
            }
            else
            {
                var userExists = _dbContext.Users.Any(u => u.Email== obj.Email);
                if(!userExists)
                {
                    EncryptPassword(obj.Password, out byte[] passwordSalt,out byte[] passwordHash);
                    obj.PasswordHash= passwordHash;
                    obj.PasswordSalt= passwordSalt;
                    _dbContext.Users.Add(obj);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
               
                
            }
        }

        public async Task<User?> Delete(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if(user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            return null;

        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            var user= await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if(user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        public async Task<User?> Update(int id, User obj)
        {
           var user  = await GetById(id);
            EncryptPassword(obj.Password, out byte[] passwordSalt, out byte[] passwordHash);
            if (user!=null)
            {
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                //user.Email = obj.Email;
                user.Password = obj.Password;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            return null;
        }

        [NonAction]
        public void EncryptPassword(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }


}
