FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 7055


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Apibackend.csproj", "."] 
RUN dotnet restore "Apibackend.csproj"
COPY . .                      
RUN dotnet build "Apibackend.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "Apibackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Apibackend.dll"]