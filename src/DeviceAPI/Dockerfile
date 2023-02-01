#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/DeviceAPI/DeviceAPI.csproj", "DeviceAPI/"]
COPY ["src/Core/Core.csproj", "Core/"]
COPY ["src/DeviceAPI.Application/DeviceAPI.Application.csproj", "DeviceAPI.Application/"]
COPY ["src/DeviceAPI.Infrastracture/DeviceAPI.Infrastracture.csproj", "DeviceAPI.Infrastracture/"]
RUN dotnet restore "DeviceAPI/DeviceAPI.csproj"
COPY . .
WORKDIR "/src/src/DeviceAPI"
RUN dotnet build "DeviceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeviceAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeviceAPI.dll"]