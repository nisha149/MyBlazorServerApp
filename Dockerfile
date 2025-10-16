# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyBlazorServerApp.csproj", "./"]
RUN dotnet restore "MyBlazorServerApp.csproj"
COPY . .
RUN dotnet build "MyBlazorServerApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyBlazorServerApp.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyBlazorServerApp.csproj", "./"]
RUN dotnet restore "MyBlazorServerApp.csproj"
COPY . .
RUN dotnet build "MyBlazorServerApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyBlazorServerApp.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
