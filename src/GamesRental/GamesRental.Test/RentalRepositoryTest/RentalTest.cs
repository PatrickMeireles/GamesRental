using AutoMapper;
using GamesRental.Application.AutoMapper;
using GamesRental.Application.Services;
using GamesRental.Application.ViewModel;
using GamesRental.Domain.Interfaces;
using GamesRental.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GamesRental.Test.RentalRepositoryTest
{
    public class RentalTest
    {
        private readonly Mock<IRental> _rentalMock;
        private readonly Mock<IGame> _gameMock;
        private readonly Mock<IFriend> _friendMock;
        private readonly RentalApplication _rentalApplication;
        private readonly IMapper _mapper;

        public RentalTest()
        {
            var profiles = new List<Profile>() { new EntityToViewModelMapping(), new ViewModelToEntityMapping() };

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);

            });
            _mapper = mockMapper.CreateMapper();

            _rentalMock = new Mock<IRental>();
            _gameMock = new Mock<IGame>();
            _friendMock = new Mock<IFriend>();
            _rentalApplication = new RentalApplication(_rentalMock.Object, _mapper);
        }

        [Fact]
        public async void VerifyGetAllItems()
        {
            var rents = _mapper.Map<IEnumerable<Rental>>(RentalTestData.GetAll());

            _rentalMock.Setup(x => x.GetAll()).ReturnsAsync(rents);

            var dbRents = await _rentalApplication.GetAll();

            Assert.True(dbRents.Any());
            Assert.True(dbRents.Count() == 3);
            _rentalMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async void VerifyCreateItem()
        {
            var rent = _mapper.Map<Rental>(RentalTestData.Get());

            _rentalMock.Setup(x => x.Add(It.IsAny<Rental>())).ReturnsAsync(rent);

            var dbRent = await _rentalApplication.Create(RentalTestData.Get());

            _rentalMock.Verify(x => x.Add(It.IsAny<Rental>()), Times.Once);
        }

        [Fact]
        public async void VerifyFinishtem()
        {
            var rentVw = new RentalViewModel(1, DateTime.Now, null, null, 1, "", 1, "", "Open");

            var rent = _mapper.Map<Rental>(rentVw);

            _rentalMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(rent);
            _rentalMock.Setup(x => x.Update(It.IsAny<Rental>())).ReturnsAsync(rent);

            var dbRent = await _rentalApplication.Finish(1);

            Assert.NotNull(dbRent.DateFinish);
            _rentalMock.Verify(x => x.Update(It.IsAny<Rental>()), Times.Once);
        }
    }
}
