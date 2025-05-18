FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY *.sln .
COPY TaskManagement.Api/*.csproj ./TaskManagement.Api/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Build and publish
WORKDIR /src/TaskManagement.Api
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80

ENTRYPOINT ["dotnet", "TaskManagement.Api.dll"] 