#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GamesRental.WebApi/GamesRental.WebApi.csproj", "GamesRental.WebApi/"]
COPY ["GamesRental.Infrastructure.IoC/GamesRental.Infrastructure.CrossCutting.IoC.csproj", "GamesRental.Infrastructure.IoC/"]
COPY ["GamesRental.Infrastructure.Data/GamesRental.Infrastructure.Data.csproj", "GamesRental.Infrastructure.Data/"]
COPY ["GamesRental.Util/GamesRental.Util.csproj", "GamesRental.Util/"]
COPY ["GamesRental.Domain/GamesRental.Domain.csproj", "GamesRental.Domain/"]
COPY ["GamesRental.Entities/GamesRental.Entities.csproj", "GamesRental.Entities/"]
COPY ["GamesRental.Application/GamesRental.Application.csproj", "GamesRental.Application/"]
RUN dotnet restore "GamesRental.WebApi/GamesRental.WebApi.csproj"
COPY . .
WORKDIR "/src/GamesRental.WebApi"
RUN dotnet build "GamesRental.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GamesRental.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamesRental.WebApi.dll"]
