FROM mcr.microsoft.com/dotnet/aspnet:8.0.8-bookworm-slim-arm64v8 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TalentHub.UserService/TalentHub.UserService.Api/TalentHub.UserService.Api.csproj", "TalentHub.UserService/TalentHub.UserService.Api/"]
COPY ["TalentHub.UserService/TalentHub.UserService.Application/TalentHub.UserService.Application.csproj", "TalentHub.UserService/TalentHub.UserService.Application/"]
COPY ["TalentHub.UserService/TalentHub.UserService.Infrastructure/TalentHub.UserService.Infrastructure.csproj", "TalentHub.UserService/TalentHub.UserService.Infrastructure/"]
RUN dotnet restore "TalentHub.UserService/TalentHub.UserService.Api/TalentHub.UserService.Api.csproj"
COPY . .
WORKDIR "/src/TalentHub.UserService/TalentHub.UserService.Api"
RUN dotnet build "TalentHub.UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TalentHub.UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalentHub.UserService.Api.dll"]