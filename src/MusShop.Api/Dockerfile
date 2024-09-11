#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/MusShop.Api/MusShop.Api.csproj", "src/MusShop.Api/"]
COPY ["src/MusShop.Infrastructure/MusShop.Infrastructure.csproj", "src/MusShop.Infrastructure/"]
COPY ["src/MusShop.Infrastructure/MusShop.Persistence.csproj", "src/MusShop.Persistence/"]
COPY ["src/MusShop.Infrastructure/MusShop.Jobs.csproj", "src/MusShop.Jobs/"]
COPY ["src/MusShop.Infrastructure/MusShop.Domain.Model.csproj", "src/MusShop.Domain.Model/"]
COPY ["src/MusShop.Infrastructure/MusShop.Domain.Services.csproj", "src/MusShop.Domain.Services/"]
COPY ["src/MusShop.Presentation/MusShop.Presentation.csproj", "src/MusShop.Presentation/"]
COPY ["src/MusShop.Application.UseCases/MusShop.Application.UseCases.csproj", "src/MusShop.Application.UseCases/"]
COPY ["src/MusShop.Application.UseCases/MusShop.Application.Dto.csproj", "src/MusShop.Application.Dto/"]
RUN dotnet restore "src/MusShop.Api/MusShop.Api.csproj"
COPY . .
WORKDIR "/src/src/MusShop.Api"
RUN dotnet build "MusShop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusShop.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusShop.Api.dll"]