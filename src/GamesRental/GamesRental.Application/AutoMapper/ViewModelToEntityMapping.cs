using AutoMapper;
using GamesRental.Application.ViewModel;
using GamesRental.Entities;

namespace GamesRental.Application.AutoMapper
{
    public class ViewModelToEntityMapping : Profile
    {
        public ViewModelToEntityMapping()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<FriendViewModel, Friend>();
            CreateMap<GameViewModel, Game>();
            CreateMap<RentalViewModel, Rental>();
        }
    }
}
