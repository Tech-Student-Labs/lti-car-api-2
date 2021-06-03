FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
EXPOSE 143

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["VehicleDatabase/VehicleDatabase.csproj", "VehicleDatabase/"]
RUN dotnet restore "VehicleDatabase/VehicleDatabase.csproj"
COPY . .
WORKDIR "/src/VehicleDatabase"
RUN dotnet build "VehicleDatabase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VehicleDatabase.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehicleDatabase.dll"]
