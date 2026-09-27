FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Payment/OrderFlow.Payment.Api/OrderFlow.Payment.Api.csproj", "src/Payment/OrderFlow.Payment.Api/"]
COPY ["src/Payment/OrderFlow.Payment.Application/OrderFlow.Payment.Application.csproj", "src/Payment/OrderFlow.Payment.Application/"]
COPY ["src/Payment/OrderFlow.Payment.Domain/OrderFlow.Payment.Domain.csproj", "src/Payment/OrderFlow.Payment.Domain/"]
COPY ["src/Payment/OrderFlow.Payment.Infrastructure/OrderFlow.Payment.Infrastructure.csproj", "src/Payment/OrderFlow.Payment.Infrastructure/"]
RUN dotnet restore "src/Payment/OrderFlow.Payment.Api/OrderFlow.Payment.Api.csproj"
COPY . .
WORKDIR "/src/src/Payment/OrderFlow.Payment.Api"
RUN dotnet build "./OrderFlow.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderFlow.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderFlow.Payment.Api.dll"]
