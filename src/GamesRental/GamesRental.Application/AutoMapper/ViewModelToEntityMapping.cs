using AutoMapper;
using GamesRental.Application.ViewModel;
using GamesRental.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesRental.Application.AutoMapper
{
    public class ViewModelToEntityMapping : Profile
    {
        public ViewModelToEntityMapping()
        {
            CreateMap<UserViewModel, User>()
                    .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                    .ForMember(x => x.Password, y => y.MapFrom(z => z.Password));

            CreateMap<FriendViewModel, Friend>();
            CreateMap<GameViewModel, Game>();
        }
    }
}
