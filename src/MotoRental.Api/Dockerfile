FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln ./
COPY src/MotoRental.Api/*.csproj src/MotoRental.Api/
COPY src/MotoRental.Application/*.csproj src/MotoRental.Application/
COPY src/MotoRental.Domain/*.csproj src/MotoRental.Domain/
COPY src/MotoRental.Infrastructure/*.csproj src/MotoRental.Infrastructure/

COPY tests/MotoRental.UnitTests/*.csproj tests/MotoRental.UnitTests/
COPY tests/MotoRental.IntegrationTests/*.csproj tests/MotoRental.IntegrationTests/

RUN dotnet restore

COPY . .
WORKDIR /app/src/MotoRental.Api
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .
EXPOSE 8081
ENTRYPOINT ["dotnet", "MotoRental.Api.dll"]
