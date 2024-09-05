# Use the official .NET 6 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY . ./
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -o /out

# Use the official .NET 6 ASP.NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/runtime:6.0

# Set the working directory inside the container
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /out .


# Expose the port on which the application will run
EXPOSE 1234

# Define the entry point for the application
ENTRYPOINT ["dotnet", "Public_API.dll"]
