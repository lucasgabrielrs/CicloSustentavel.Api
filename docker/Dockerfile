# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar arquivos de projeto
COPY ["../src/CicloSustentavel.Api/CicloSustentavel.Api.csproj", "src/CicloSustentavel.Api/"]
COPY ["../src/CicloSustentavel.Application/CicloSustentavel.Application.csproj", "src/CicloSustentavel.Application/"]
COPY ["../src/CicloSustentavel.Communication/CicloSustentavel.Communication.csproj", "src/CicloSustentavel.Communication/"]
COPY ["../src/CicloSustentavel.Domain/CicloSustentavel.Domain.csproj", "src/CicloSustentavel.Domain/"]
COPY ["../src/CicloSustentavel.Infrastructure/CicloSustentavel.Infrastructure.csproj", "src/CicloSustentavel.Infrastructure/"]
COPY ["../CicloSustentavel.Exception/CicloSustentavel.Exception.csproj", "CicloSustentavel.Exception/"]

# Restaurar dependências
RUN dotnet restore "src/CicloSustentavel.Api/CicloSustentavel.Api.csproj"

# Copiar todo o código fonte
COPY .. .

# Build da aplicação
WORKDIR "/src/src/CicloSustentavel.Api"
RUN dotnet build "CicloSustentavel.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "CicloSustentavel.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CicloSustentavel.Api.dll"]
