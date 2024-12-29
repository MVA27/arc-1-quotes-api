# Use the official .NET SDK image for building 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the solution file and the project files into the container
COPY Backend/Backend.sln ./Backend/
COPY Backend/Quotes.Api/Quotes.Api.csproj ./Backend/Quotes.Api/

# Restore dependencies
RUN dotnet restore Backend/Backend.sln

# Copy the rest of the application code
COPY . .

# Publish the application to a folder
RUN dotnet publish Backend/Quotes.Api/Quotes.Api.csproj -c Release -o /app/publish

# Use the official .NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Define the environment variable for DB_CONNECTION_STRING in the runtime stage
ENV DB_CONNECTION_STRING=${DB_CONNECTION_STRING}

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Quotes.Api.dll"]