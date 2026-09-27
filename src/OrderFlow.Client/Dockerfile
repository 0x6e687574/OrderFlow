FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OrderFlow.Client/OrderFlow.Client.csproj", "src/OrderFlow.Client/"]
RUN dotnet restore "src/OrderFlow.Client/OrderFlow.Client.csproj"
COPY . .
WORKDIR "/src/src/OrderFlow.Client"
RUN dotnet publish "./OrderFlow.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM nginx:alpine AS final
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY src/OrderFlow.Client/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80