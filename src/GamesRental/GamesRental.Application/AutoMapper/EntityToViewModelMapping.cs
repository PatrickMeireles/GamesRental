using AutoMapper;
using GamesRental.Application.ViewModel;
using GamesRental.Domain.Methods;
using GamesRental.Entities;
using GamesRental.Entities.Enuns;
using GamesRental.Util;
using System.Linq;

namespace GamesRental.Application.AutoMapper
{
    public class EntityToViewModelMapping : Profile
    {
        public EntityToViewModelMapping()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Friend, FriendViewModel>();

            CreateMap<Game, GameViewModel>()
                .ForMember(x => x.GenreDescription, y => y.MapFrom(z => ((GenreGame)z.Genre).GetDescription()))
                .ForMember(x => x.Avaliable, y => y.MapFrom(z => !z.Rents.Any() || 
                                                                  z.Rents.OrderByDescending(x => x.Id)
                                                                  .FirstOrDefault()
                                                                  .DateFinish.HasValue));

            CreateMap<Rental, RentalViewModel>()
                    .ForMember(x => x.FriendName, y => y.MapFrom(z => z.Friend != null ? z.Friend.Name : ""))
                    .ForMember(x => x.GameName, y => y.MapFrom(z => z.Game != null ? z.Game.Name : ""))
                    .ForMember(x => x.Status, y => y.MapFrom(z => z.Finished() ? "Closed" : "Open"));
        }
    }
}
