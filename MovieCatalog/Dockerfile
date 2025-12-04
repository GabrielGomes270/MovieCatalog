# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar apenas o csproj primeiro (melhora cache do Docker)
COPY MovieCatalog.csproj ./

RUN dotnet restore

# Copiar todo o restante
COPY . .

# Publicar em Release
RUN dotnet publish -c Release -o /app/publish

# Final stage (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar build final
COPY --from=build /app/publish .

# Expõe a porta definida no compose
EXPOSE 8080

ENTRYPOINT ["dotnet", "MovieCatalog.dll"]
