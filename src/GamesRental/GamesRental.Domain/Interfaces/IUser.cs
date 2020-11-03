using GamesRental.Domain.Interfaces.Generic;
using GamesRental.Entities;
using System.Threading.Tasks;

namespace GamesRental.Domain.Interfaces
{
    public interface IUser : IRepository<User>
    {
        Task<User> Authenticate(string email, string password);
    }
}
