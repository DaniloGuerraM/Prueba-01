# 1. Imagen base para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5231

# 2. Imagen para construir la app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Taipa.API/ .   # <-- ahora apunta a la carpeta correcta
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# 3. Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Taipa.API.dll"]  # <-- tu DLL
