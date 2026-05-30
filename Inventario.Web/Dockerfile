FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Inventario.Web/Inventario.Web.csproj", "Inventario.Web/"]
COPY ["Inventario.Application/Inventario.Application.csproj", "Inventario.Application/"]
COPY ["Inventario.Domain/Inventario.Domain.csproj", "Inventario.Domain/"]
COPY ["Inventario.Infrastructure/Inventario.Infrastructure.csproj", "Inventario.Infrastructure/"]
RUN dotnet restore "Inventario.Web/Inventario.Web.csproj"
COPY . .
WORKDIR "/src/Inventario.Web"
RUN dotnet build "Inventario.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inventario.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventario.Web.dll"]
