using AutoMapper;
using GamesRental.Application.AutoMapper;
using GamesRental.Application.Services;
using GamesRental.Application.ViewModel;
using GamesRental.Domain.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;
using GamesRental.Entities;
using System.Linq;
using ExpectedObjects;
using GamesRental.Application.Validation;

namespace GamesRental.Test.FriendRepositoryTest
{
    public class FriendTest
    {
        private readonly Mock<IFriend> _friendMock;
        private readonly FriendApplication _friendApplication;
        private readonly IMapper _mapper;

        public FriendTest()
        {
            //Auto Mapper
            var profiles = new List<Profile>() { new EntityToViewModelMapping(), new ViewModelToEntityMapping() };

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);

            });
            _mapper = mockMapper.CreateMapper();

            _friendMock = new Mock<IFriend>();
            _friendApplication = new FriendApplication(_friendMock.Object, _mapper);
        }


        [Fact]
        public async void VerifyExistItems()
        {
            var friends = _mapper.Map<IEnumerable<Friend>>(FriendTestData.GetAll());

            _friendMock.Setup(x => x.GetAll()).ReturnsAsync(friends);

            var dbFriends = await _friendApplication.GetAll();

            Assert.True(dbFriends.Count() == 3);
        }

        [Fact]
        public async void VerifyCreateItemNoError()
        {
            var friendVM = FriendTestData.Get();

            var friend = _mapper.Map<Friend>(friendVM);

            _friendMock.Setup(x => x.Add(It.IsAny<Friend>())).ReturnsAsync(friend);

            var newFriend = await _friendApplication.Create(friendVM);

            friendVM.ToExpectedObject().ShouldEqual(newFriend);
        }

        [Fact]
        public async void VerifyCreateItemWithError()
        {
            var friendVM = FriendTestData.Get();
            
            var friends = _mapper.Map<IEnumerable<Friend>>(FriendTestData.GetAll());

            _friendMock.Setup(x => x.GetAll()).ReturnsAsync(friends);

            var newFriend = await _friendApplication.Create(friendVM);

            var validation = new FriendValidation(_friendApplication).Validate(friendVM);

            Assert.False(validation.IsValid);

            Assert.True(validation.Errors.Where(x => x.ErrorMessage == "Já possui um amigo cadastrado com esse email.").Any());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("", " ")]
        [InlineData("", null)]
        [InlineData(null, "")]
        public void VerifyCreateItemWithErroProperty(string _name, string _email)
        {
            var friendVM = new FriendViewModel(0, _name, _email);

            var validation = new FriendValidation(_friendApplication).Validate(friendVM);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Where(x => x.ErrorMessage == "Email não foi informado." || x.ErrorCode == "Nome não foi informado.").Any());
        }

        [Fact]
        public async void VerifyDeleteItem()
        {
            var friends = _mapper.Map<IEnumerable<Friend>>(FriendTestData.GetAll());

            _friendMock.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(true);

            var itemDelete = await _friendApplication.Delete(1);

            _friendMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(100)]
        public async void VerifyGetItemById(int id)
        {
            var friendVM = FriendTestData.Get(id);

            var friend = _mapper.Map<Friend>(friendVM);

            _friendMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(friend);

            var item = await _friendApplication.GetById(id);

            Assert.Equal(id, item.Id);
        }
    }
}
