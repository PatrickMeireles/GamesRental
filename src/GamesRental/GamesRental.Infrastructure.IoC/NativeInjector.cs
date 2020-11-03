using GamesRental.Application.Interface;
using GamesRental.Application.Services;
using GamesRental.Domain.Interfaces;
using GamesRental.Infrastructure.Data.Context;
using GamesRental.Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace GamesRental.Infrastructure.CrossCutting.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //APPLICATION
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IFriendApplication, FriendApplication>();
            services.AddScoped<IGameApplication, GameApplication>();
            services.AddScoped<IRentalApplication, RentalApplication>();

            //INFRA
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IFriend, FriendRepository>();
            services.AddScoped<IGame, GameRepository>();
            services.AddScoped<IRental, RentalRepository>();

            //CONTEXT
            services.AddScoped<BaseContext>();
        }
    }
}
