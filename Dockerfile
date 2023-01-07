FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .
WORKDIR /src/GhostNetwork.Notifications.Api
RUN dotnet restore GhostNetwork.Notifications.Api.csproj
RUN dotnet publish GhostNetwork.Notifications.Api.csproj --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "GhostNetwork.Notifications.Api.dll"]
