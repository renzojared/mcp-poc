FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/InvoiceSync/InvoiceSync.Mcp/InvoiceSync.Mcp.csproj", "src/InvoiceSync/InvoiceSync.Mcp/"]
COPY ["src/InvoiceSync/InvoiceSync.Contracts/InvoiceSync.Contracts.csproj", "src/InvoiceSync/InvoiceSync.Contracts/"]
RUN dotnet restore "src/InvoiceSync/InvoiceSync.Mcp/InvoiceSync.Mcp.csproj"
COPY . .
WORKDIR "/src/src/InvoiceSync/InvoiceSync.Mcp"
RUN dotnet build "./InvoiceSync.Mcp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InvoiceSync.Mcp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvoiceSync.Mcp.dll"]
