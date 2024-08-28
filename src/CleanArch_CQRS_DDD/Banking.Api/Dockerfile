#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Banking.Api/Banking.Api.csproj", "Banking.Api/"]
COPY ["Banking.Application/Banking.Application.csproj", "Banking.Application/"]
COPY ["Banking.Domain/Banking.Domain.csproj", "Banking.Domain/"]
COPY ["Banking.SharedKernel/Banking.SharedKernel.csproj", "Banking.SharedKernel/"]
COPY ["Shared.Register/Shared.Register.csproj", "Shared.Register/"]
COPY ["Banking.Persistence/Banking.Persistence.csproj", "Banking.Persistence/"]
RUN dotnet restore "./Banking.Api/Banking.Api.csproj"
COPY . .
WORKDIR "/src/Banking.Api"
RUN dotnet build "./Banking.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Banking.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banking.Api.dll"]