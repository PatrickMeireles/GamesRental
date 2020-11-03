using GamesRental.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GamesRental.WebApi.Configuration
{
    public static class DataBaseSetup
    {
        public static void AddDataBaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentException(nameof(services));

            services.AddDbContext<BaseContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
        }
    }
}
