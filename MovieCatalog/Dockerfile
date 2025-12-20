# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar apenas o csproj primeiro (melhora cache do Docker)
COPY MovieCatalog.csproj ./
RUN dotnet restore

# Copiar todo o restante
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Porta usada no docker-compose
EXPOSE 8080

ENTRYPOINT ["dotnet", "MovieCatalog.dll"]