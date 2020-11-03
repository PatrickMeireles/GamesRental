using GamesRental.Domain.Interfaces;
using GamesRental.Entities;
using GamesRental.Infrastructure.Data.Context;
using GamesRental.Infrastructure.Data.Repository.Generic;

namespace GamesRental.Infrastructure.Data.Repository
{
    public class FriendRepository : Repository<Friend>, IFriend
    {
        public FriendRepository(BaseContext baseContext): base(baseContext) { }
    }
}
