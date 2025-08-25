# Stage 1: Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Stage 2: Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Copier le fichier csproj depuis le dossier actuel
COPY ["Apibackend.csproj", "./"]
RUN dotnet restore "Apibackend.csproj"
# Copier le reste du projet
COPY . .
RUN dotnet build "Apibackend.csproj" -c Release -o /app/build

# Stage 3: Publish stage
FROM build AS publish
RUN dotnet publish "Apibackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Apibackend.dll"]
