using AutoMapper;
using GamesRental.Application.Interface;
using GamesRental.Application.ViewModel;
using GamesRental.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesRental.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public UserApplication(IUser user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Authenticate(string email, string password) => _mapper.Map<UserViewModel>(await _user.Authenticate(email, password));

        public async Task<IEnumerable<UserViewModel>> GetAll() => _mapper.Map<IEnumerable<UserViewModel>>(await _user.GetAll());
    }
}
