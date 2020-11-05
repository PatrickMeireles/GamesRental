using AutoMapper;
using ExpectedObjects;
using GamesRental.Application.AutoMapper;
using GamesRental.Application.Services;
using GamesRental.Application.Validation;
using GamesRental.Application.ViewModel;
using GamesRental.Domain.Interfaces;
using GamesRental.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GamesRental.Test.GameRepositoryTest
{
    public class GameTest
    {
        public readonly Mock<IGame> _gameMock;
        public readonly Mock<IRental> _rentalMock;
        private readonly GameApplication _gameApplication;
        private readonly IMapper _mapper;

        public GameTest()
        {
            var profiles = new List<Profile>() { new EntityToViewModelMapping(), new ViewModelToEntityMapping() };

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);

            });
            _mapper = mockMapper.CreateMapper();

            _gameMock = new Mock<IGame>();
            _rentalMock = new Mock<IRental>();
            _gameApplication = new GameApplication(_gameMock.Object, _rentalMock.Object, _mapper);
        }

        [Fact]
        public async void VerifyReturnItems()
        {
            var games = _mapper.Map<IEnumerable<Game>>(GameTestData.GetAll());

            _gameMock.Setup(x => x.GetAll()).ReturnsAsync(games);

            var dbGames = await _gameApplication.GetAll("", null);

            Assert.True(dbGames.Any());
            Assert.True(dbGames.Count() == 4);
        }

        [Fact]
        public async void VerifyCreateItem()
        {
            var gameVw = GameTestData.Get();
            var game = _mapper.Map<Game>(gameVw);

            _gameMock.Setup(x => x.Add(It.IsAny<Game>())).ReturnsAsync(game);

            var newGame = await _gameApplication.Add(gameVw);

            gameVw.ToExpectedObject().ShouldEqual(newGame);
        }

        [Fact]
        public async void CreateItemWithError()
        {
            var gameVw = GameTestData.Get();
            var game = _mapper.Map<Game>(gameVw);

            _gameMock.Setup(x => x.Add(It.IsAny<Game>())).ReturnsAsync(game);

            var newGame = await _gameApplication.Add(gameVw);

            var validation = new GameValidation(_gameApplication).Validate(gameVw);

            Assert.False(validation.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async void CreateItemWithErrorValidation(string name)
        {
            var gameVw = new GameViewModel(0, name, "new description", 1, null, true, false);
            var game = _mapper.Map<Game>(gameVw);

            _gameMock.Setup(x => x.Add(It.IsAny<Game>())).ReturnsAsync(game);

            var newGame = await _gameApplication.Add(gameVw);

            var validation = new GameValidation(_gameApplication).Validate(gameVw);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Where(x => x.ErrorMessage == "Nome não foi informado.").Any());
        }

        [Fact]
        public async void VerifyDeleteItem()
        {
            _gameMock.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(true);

            var itemDelete = await _gameApplication.Delete(1);

            _gameMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(100)]
        public async void VerifyGetItemById(int id)
        {
            var gameVw = GameTestData.Get(id);

            var game = _mapper.Map<Game>(gameVw);

            _gameMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(game);

            var item = await _gameApplication.GetById(1);

            Assert.Equal(id, item.Id);
        }
    }
}
