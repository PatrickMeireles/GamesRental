using GamesRental.Domain.Interfaces;
using GamesRental.Entities;
using GamesRental.Infrastructure.Data.Context;
using GamesRental.Infrastructure.Data.Repository.Generic;
using GamesRental.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRental.Infrastructure.Data.Repository
{
    public class UserRepository : Repository<User>, IUser
    {
        public UserRepository(BaseContext context) : base(context) { }

        public async Task<User> Authenticate(string email, string password)
        {
            return await _dbSet.Where(x => x.Email == email 
                                        && x.Password == HashMD5.getMD5(password))
                                   .FirstOrDefaultAsync();
        }
    }
}
