using MCS_DemoProject_Angular_WebApi.Interfaces;
using MCS_DemoProject_Angular_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MCS_DemoProject_Angular_WebApi.Repositories
{
    public class claimsRepo : IUsers<ClaimsMaster>
    {
        private readonly MCSContext _dbContext;

        public claimsRepo(MCSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Create(ClaimsMaster obj)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsMaster?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClaimsMaster>> GetAll()
        {
            return await _dbContext.ClaimsMasters.ToListAsync();
        }

        public Task<ClaimsMaster?> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsMaster?> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsMaster?> Update(int id, ClaimsMaster obj)
        {
            throw new NotImplementedException();
        }
    }
}
