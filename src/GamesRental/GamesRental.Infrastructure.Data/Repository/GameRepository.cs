using GamesRental.Domain.Interfaces;
using GamesRental.Entities;
using GamesRental.Infrastructure.Data.Context;
using GamesRental.Infrastructure.Data.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesRental.Infrastructure.Data.Repository
{
    public class GameRepository: Repository<Game>, IGame
    {
        public GameRepository(BaseContext baseContext) : base(baseContext) { }
    }
}
