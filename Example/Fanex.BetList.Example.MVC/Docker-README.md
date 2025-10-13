# Docker Setup for Fanex.BetList.Example.MVC

This document provides instructions for building and running the Fanex.BetList.Example.MVC application using Docker.

## Prerequisites

- Docker Desktop installed and running
- Docker Compose (usually included with Docker Desktop)
- At least 4GB of available RAM for Docker

## Quick Start

### Option 1: Using Docker Compose (Recommended)

1. **Navigate to the project directory:**
   ```bash
   cd Example/Fanex.BetList.Example.MVC
   ```

2. **Build and run the application:**
   ```bash
   docker-compose up --build
   ```

3. **Access the application:**
   - HTTP: http://localhost:8080
   - HTTPS: https://localhost:8443

4. **Stop the application:**
   ```bash
   docker-compose down
   ```

### Option 2: Using Docker Commands

1. **Build the Docker image:**
   ```bash
   docker build -t betlist-mvc -f Example/Fanex.BetList.Example.MVC/Dockerfile .
   ```

2. **Run the container:**
   ```bash
   docker run -d \
     --name betlist-mvc-app \
     -p 8080:80 \
     -p 8443:443 \
     -v $(pwd)/Example/Fanex.BetList.Example.MVC/App_Data:/app/App_Data \
     -v $(pwd)/Example/Fanex.BetList.Example.MVC/DLL:/app/DLL \
     betlist-mvc
   ```

3. **Access the application:**
   - HTTP: http://localhost:8080
   - HTTPS: https://localhost:8443

4. **Stop and remove the container:**
   ```bash
   docker stop betlist-mvc-app
   docker rm betlist-mvc-app
   ```

## Dockerfile Details

The Dockerfile uses a multi-stage build approach:

- **Build stage**: Uses `mcr.microsoft.com/dotnet/sdk:8.0` to compile the application
- **Runtime stage**: Uses `mcr.microsoft.com/dotnet/aspnet:8.0` for the final image

### Key Features

- Multi-stage build for optimized image size
- .NET 8.0 runtime
- Support for System.Drawing.Common (libgdiplus installed)
- Proper handling of App_Data and DLL directories
- HTTP and HTTPS support
- Production environment configuration

## Configuration

### Environment Variables

- `ASPNETCORE_ENVIRONMENT`: Set to "Production" by default
- `ASPNETCORE_URLS`: Configured for HTTP (port 80) and HTTPS (port 443)

### Port Mapping

- Container port 80 → Host port 8080 (HTTP)
- Container port 443 → Host port 8443 (HTTPS)

### Volume Mounts

- `./App_Data` → `/app/App_Data` (Application data)
- `./DLL` → `/app/DLL` (Dynamic link libraries)

## Troubleshooting

### Common Issues

1. **Port already in use:**
   ```bash
   # Check what's using the port
   netstat -ano | findstr :8080
   
   # Kill the process or change the port in docker-compose.yml
   ```

2. **Permission denied for volumes:**
   ```bash
   # On Windows, ensure Docker has access to the directories
   # On Linux/Mac, check file permissions
   chmod 755 App_Data DLL
   ```

3. **Build fails:**
   ```bash
   # Clean Docker cache
   docker system prune -a
   
   # Rebuild without cache
   docker-compose build --no-cache
   ```

### Logs

View application logs:
```bash
docker-compose logs -f betlist-mvc
```

Or for a specific container:
```bash
docker logs -f betlist-mvc-app
```

## Development

### Hot Reload

For development with hot reload, you can override the Dockerfile:

```yaml
# docker-compose.override.yml
version: '3.8'
services:
  betlist-mvc:
    build:
      context: ../..
      dockerfile: Example/Fanex.BetList.Example.MVC/Dockerfile.dev
    volumes:
      - ../../:/src
      - /src/Example/Fanex.BetList.Example.MVC/bin
      - /src/Example/Fanex.BetList.Example.MVC/obj
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
```

### Debugging

To debug the application running in Docker:

1. **Enable debugging in docker-compose.yml:**
   ```yaml
   environment:
     - ASPNETCORE_ENVIRONMENT=Development
     - DOTNET_USE_POLLING_FILE_WATCHER=1
   ```

2. **Attach debugger to the container process**

## Production Deployment

### Security Considerations

- Change default ports in production
- Use environment-specific configuration files
- Implement proper logging and monitoring
- Consider using Docker secrets for sensitive data

### Scaling

```bash
# Scale to multiple instances
docker-compose up --scale betlist-mvc=3
```

### Health Checks

The application includes basic health check endpoints. Monitor these in production:

- `/health` - Basic health status
- `/ready` - Readiness check

## Support

For issues related to:
- Docker setup: Check Docker logs and configuration
- Application functionality: Review application logs
- Performance: Monitor resource usage with `docker stats`
