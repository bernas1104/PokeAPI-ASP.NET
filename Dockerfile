# Defines the SDK for Build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy .csproj and restore
COPY . ./
RUN dotnet restore
RUN dotnet test --verbosity minimal
RUN dotnet publish -c Release -o out

# Buld runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=build-env /app/out .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet PokeAPI.dll
