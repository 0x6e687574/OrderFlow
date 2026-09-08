FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Order/OrderFlow.Order.Api/OrderFlow.Order.Api.csproj", "src/Order/OrderFlow.Order.Api/"]
RUN dotnet restore "src/Order/OrderFlow.Order.Api/OrderFlow.Order.Api.csproj"
COPY . .
WORKDIR "/src/src/Order/OrderFlow.Order.Api"
RUN dotnet build "./OrderFlow.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderFlow.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderFlow.Order.Api.dll"]
