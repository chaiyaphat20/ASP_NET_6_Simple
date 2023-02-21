FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ASPNET7LIVE.csproj", "./"]
RUN dotnet restore "ASPNET7LIVE.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ASPNET7LIVE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASPNET7LIVE.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASPNET7LIVE.dll"]
