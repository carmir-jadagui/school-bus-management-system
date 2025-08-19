# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.
# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["app/SBMS.API/SBMS.API.csproj", "app/SBMS.API/"]
COPY ["src/core/SBMS.Application/SBMS.Application.csproj", "src/core/SBMS.Application/"]
COPY ["src/core/SBMS.Domain/SBMS.Domain.csproj", "src/core/SBMS.Domain/"]
COPY ["src/core/SBMS.Base/SBMS.Base.csproj", "src/core/SBMS.Base/"]
COPY ["src/infrastructure/persistence/SBMS.Persistence.MySQL/SBMS.Persistence.MySQL.csproj", "src/infrastructure/persistence/SBMS.Persistence.MySQL/"]
RUN dotnet restore "app/SBMS.API/SBMS.API.csproj"
COPY . .
WORKDIR "/src/app/SBMS.API"
RUN dotnet build "SBMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "SBMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SBMS.API.dll"]