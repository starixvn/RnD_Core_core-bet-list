# Use the official .NET 8.0 SDK image as the build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /src

# Copy the solution file and restore dependencies
COPY Fanex.BetList.Example.sln ./
COPY Fanex.BetList.Core/Fanex.BetList.Core.csproj Fanex.BetList.Core/
COPY Fanex.BetList.Example.Data/Fanex.BetList.Example.Data.csproj Fanex.BetList.Example.Data/
COPY Fanex.BetList.Example.Extension/Fanex.BetList.Example.Extension.csproj Fanex.BetList.Example.Extension/
COPY Fanex.BetList.Example.MVC/Fanex.BetList.Example.MVC.csproj Fanex.BetList.Example.MVC/

# Restore NuGet packages
RUN dotnet restore

# Copy the source code
COPY . .

# Build the application
WORKDIR /src/Fanex.BetList.Example.MVC
RUN dotnet build -c Release -o /app/build

# Publish the application
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set working directory
WORKDIR /app

# Install required packages for System.Drawing.Common
RUN apt-get update && apt-get install -y \
    libgdiplus \
    libc6-dev \
    && rm -rf /var/lib/apt/lists/*

# Copy the published application
COPY --from=build /app/publish .

# Create directory for App_Data and DLL
RUN mkdir -p App_Data DLL

# Expose port 80 and 443
EXPOSE 80
EXPOSE 443

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80;https://+:443
ENV ASPNETCORE_ENVIRONMENT=Production

# Set the entry point
ENTRYPOINT ["dotnet", "Fanex.BetList.Example.MVC.dll"]
