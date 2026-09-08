FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Inventory/OrderFlow.Inventory.Api/OrderFlow.Inventory.Api.csproj", "src/Inventory/OrderFlow.Inventory.Api/"]
RUN dotnet restore "src/Inventory/OrderFlow.Inventory.Api/OrderFlow.Inventory.Api.csproj"
COPY . .
WORKDIR "/src/src/Inventory/OrderFlow.Inventory.Api"
RUN dotnet build "./OrderFlow.Inventory.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderFlow.Inventory.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderFlow.Inventory.Api.dll"]
