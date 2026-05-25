# STAGE 1: Base Runtime (Lightweight)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# STAGE 2: Build (Contains SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files first to leverage Docker cache for NuGet restore
COPY ["src/DentalManagement.API/DentalManagement.API.csproj", "src/DentalManagement.API/"]
COPY ["src/DentalManagement.Application/DentalManagement.Application.csproj", "src/DentalManagement.Application/"]
COPY ["src/DentalManagement.Domain/DentalManagement.Domain.csproj", "src/DentalManagement.Domain/"]
COPY ["src/DentalManagement.Infrastructure/DentalManagement.Infrastructure.csproj", "src/DentalManagement.Infrastructure/"]

RUN dotnet restore "src/DentalManagement.API/DentalManagement.API.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/src/DentalManagement.API"
RUN dotnet build "DentalManagement.API.csproj" -c Release -o /app/build

# STAGE 3: Publish
FROM build AS publish
RUN dotnet publish "DentalManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# STAGE 4: Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DentalManagement.API.dll"]