using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
using GamesRental.Application.AutoMapper;

namespace GamesRental.WebApi.Configuration
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentException(nameof(services));

            services.AddAutoMapper(typeof(EntityToViewModelMapping), typeof(ViewModelToEntityMapping));
        }
    }
}
