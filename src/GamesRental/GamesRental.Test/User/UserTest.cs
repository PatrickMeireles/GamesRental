using AutoMapper;
using Bogus;
using GamesRental.Application.AutoMapper;
using GamesRental.Application.Services;
using GamesRental.Application.ViewModel;
using GamesRental.Domain.Interfaces;
using GamesRental.Util;
using Moq;
using System.Collections.Generic;
using Xunit;
using ExpectedObjects;
using GamesRental.Application.Validation;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace GamesRental.Test.User
{
    public class UserTest 
    {
        private readonly UserViewModel userViewModel;
        private readonly LoginViewModel loginViewModel;
        private readonly Mock<IUser> _user;
        private readonly UserApplication _userApplication;
        private readonly IMapper _mapper;

        public UserTest()
        {
            var faker = new Faker();
            userViewModel = new UserViewModel()
            {
                Email = "teste@teste.com",
                Hash = HashMD5.getMD5("1"),
                Name = "faker user",
                Password = "123456",
                Role = 1
            };

            loginViewModel = new LoginViewModel()
            {
                Login = "teste@teste.com",
                Password = "123456"
            };

            //auto mapper configuration
            var profiles = new List<Profile>() { new EntityToViewModelMapping(), new ViewModelToEntityMapping() };

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);

            });
            _mapper = mockMapper.CreateMapper();

            _user = new Mock<IUser>();
            _userApplication = new UserApplication(_user.Object, _mapper);
        }

        [Fact]
        public async void VerifyUserIsAuthenticated()
        {
            var user = _mapper.Map<Entities.User>(userViewModel);

            _user.Setup(x => x.Authenticate(user.Email, user.Password)).ReturnsAsync(user);

            var authenticate = await _userApplication.Authenticate(loginViewModel.Login, loginViewModel.Password);

            user.ToExpectedObject().ShouldMatch(authenticate);
        }

        [Theory]
        [InlineData("user@fake.com", "invalidPass0rd")]
        [InlineData("fake@email.com", "98723")]
        public async void VerifyUserIsNotAuthenticated(string email, string password)
        {
            var user = _mapper.Map<Entities.User>(userViewModel);

            _user.Setup(x => x.Authenticate(user.Email, user.Password)).ReturnsAsync(user);

            var validation = new LoginValidation(_userApplication).Validate(new LoginViewModel() { Login = email, Password = password });

            var authenticate = await _userApplication.Authenticate(email, password);

            user.ToExpectedObject().ShouldNotMatch(authenticate);
        }

        [Theory]
        [InlineData("user@fake.com", "invalidPass0rd")]
        [InlineData("fake@email.com", "98723")]
        public void ValidateUserNotAuthenticated(string email, string password)
        {
            var user = _mapper.Map<Entities.User>(userViewModel);

            _user.Setup(x => x.Authenticate(user.Email, user.Password)).ReturnsAsync(user);

            var validation = new LoginValidation(_userApplication).Validate(new LoginViewModel() { Login = email, Password = password });
            
            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Where(x => x.ErrorMessage == "Usuário não encontrado.").Any());
        }
    }
}
