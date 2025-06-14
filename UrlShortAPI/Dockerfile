# Use .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UrlShortAPI.csproj", "./"]
RUN dotnet restore "UrlShortAPI.csproj"
COPY . .
RUN dotnet publish "UrlShortAPI.csproj" -c Release -o /app/publish

# Final runtime container
FROM base AS final
WORKDIR /app

# 🔧 Install debug tools (optional — remove in production)
RUN apt-get update && apt-get install -y iputils-ping dnsutils curl nano

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UrlShortAPI.dll"]
