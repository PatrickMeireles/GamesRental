using GamesRental.Infrastructure.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GamesRental.WebApi.Configuration
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentException(nameof(services));

            NativeInjector.RegisterServices(services);
        }
    }
}
