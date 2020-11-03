using AutoMapper;
using GamesRental.Application.ViewModel;
using GamesRental.Entities;
using GamesRental.Entities.Enuns;
using GamesRental.Util;

namespace GamesRental.Application.AutoMapper
{
    public class EntityToViewModelMapping : Profile
    {
        public EntityToViewModelMapping()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Friend, FriendViewModel>();

            CreateMap<Game, GameViewModel>()
                .ForMember(x => x.GenreDescription, y => y.MapFrom(z => ((GenreGame)z.Genre).GetDescription()));
        }
    }
}
