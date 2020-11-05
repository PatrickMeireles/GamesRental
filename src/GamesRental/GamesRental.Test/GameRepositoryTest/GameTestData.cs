using Bogus;
using GamesRental.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesRental.Test.GameRepositoryTest
{
    public static class GameTestData
    {
        public static List<GameViewModel> GetAll()
        {
            var faker = new Faker("pt_BR");
            return new List<GameViewModel>()
            {
                new GameViewModel(1, faker.Name.FullName(), faker.Lorem.Random.String(150), faker.Random.Int(1, 7), null, true, false),
                new GameViewModel(2, faker.Name.FullName(), faker.Lorem.Random.String(150), faker.Random.Int(1, 7), null, true, false),
                new GameViewModel(3, faker.Name.FullName(), faker.Lorem.Random.String(150), faker.Random.Int(1, 7), null, true, false),
                new GameViewModel(4, "fake name", faker.Lorem.Random.String(150), faker.Random.Int(1, 7), null, true, false)
        };
        }

        public static GameViewModel Get(int id = 1)
        {
            return new GameViewModel(id, "new game", "new description", 1,  null, true, false);
        }
    }
}
