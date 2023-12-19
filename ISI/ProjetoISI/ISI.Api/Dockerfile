FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ISI.Api/ISI.Api.csproj", "ISI.Api/"]
RUN dotnet restore "ISI.Api/ISI.Api.csproj"
COPY . .
WORKDIR "/src/ISI.Api"
RUN dotnet build "ISI.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ISI.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ISI.Api.dll"]
