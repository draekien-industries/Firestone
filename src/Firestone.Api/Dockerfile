FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Firestone.Api/Firestone.Api.csproj", "Firestone.Api/"]
RUN dotnet restore "src/Firestone.Api/Firestone.Api.csproj"
COPY . .
WORKDIR "/src/Firestone.Api"
RUN dotnet build "Firestone.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Firestone.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Firestone.Api.dll"]
