using GamesRental.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GamesRental.Application.Interface
{
    public interface IUserApplication
    {
        Task<UserViewModel> Authenticate(string email, string password);

        Task<IEnumerable<UserViewModel>> GetAll();
    }
}
