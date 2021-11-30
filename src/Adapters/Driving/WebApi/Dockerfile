FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Adapters/Driving/WebApi/WebApi.csproj", "src/Adapters/Driving/WebApi/"]
RUN dotnet restore "src/Adapters/Driving/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/src/Adapters/Driving/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]