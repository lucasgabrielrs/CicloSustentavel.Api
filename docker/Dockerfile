# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY CicloSustentavel.Api/CicloSustentavel.Api.csproj CicloSustentavel.Api/
COPY CicloSustentavel.Application/CicloSustentavel.Application.csproj CicloSustentavel.Application/
COPY CicloSustentavel.Communication/CicloSustentavel.Communication.csproj CicloSustentavel.Communication/
COPY CicloSustentavel.Domain/CicloSustentavel.Domain.csproj CicloSustentavel.Domain/
COPY CicloSustentavel.Infrastructure/CicloSustentavel.Infrastructure.csproj CicloSustentavel.Infrastructure/

RUN dotnet restore CicloSustentavel.Api/CicloSustentavel.Api.csproj

# Copia o restante do código e publica
COPY . .
RUN dotnet publish CicloSustentavel.Api/CicloSustentavel.Api.csproj -c Release -o /app/publish

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Cria a pasta onde o SQLite vai salvar o arquivo .db
RUN mkdir -p /app/data

EXPOSE 8080

ENTRYPOINT ["dotnet", "CicloSustentavel.Api.dll"]