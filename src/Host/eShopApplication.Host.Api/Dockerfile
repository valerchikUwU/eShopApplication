#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Host/eShopApplication.Host.Api/eShopApplication.Host.Api.csproj", "src/Host/eShopApplication.Host.Api/"]
COPY ["src/Application/eShopApplication.Application.AppData.csproj", "src/Application/eShopApplication.Application.AppData"]
COPY ["src/Contracts/eShopApplication.Contracts.csproj", "src/Contracts/eShopApplication.Contracts"]
COPY ["src/Infrastructure/eShopApplication.Infrastructure.csproj", "src/Infrastructure/eShopApplication.Infrastructure"]
COPY ["src/Infrastructure/eShopApplication.Infrastructure.DataAccess.csproj", "src/Infrastructure/eShopApplication.Infrastructure.DataAccess"]
RUN dotnet restore "src/Host/eShopApplication.Host.Api/eShopApplication.Host.Api.csproj"
COPY . .
WORKDIR "src/Host/eShopApplication.Host.Api"
RUN dotnet build "eShopApplication.Host.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShopApplication.Host.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopApplication.Host.Api.dll"]