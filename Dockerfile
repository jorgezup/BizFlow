FROM mcr.microsoft.com/dotnet/sdk:8.0.101 AS build-env
WORKDIR /

# Copy csproj and restore dependencies
COPY . ./

WORKDIR /src/WebAPI
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -o out --no-restore

# Ensure that localization directories exist
RUN mkdir -p bin/Release/net8.0/pt-br

# Build the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app
COPY --from=build-env /src/WebAPI/out . 
ENTRYPOINT ["dotnet", "WebAPI.dll"]
