# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o .csproj da API (para melhor aproveitamento de cache)
COPY popfragg.Api/popfragg.Api.csproj ./popfragg.Api/
RUN dotnet restore ./popfragg.Api/popfragg.Api.csproj

# Copia o restante da aplicação
COPY . .

# Publica o projeto
RUN dotnet publish ./popfragg.Api/popfragg.Api.csproj -c Release -o /app/publish

# Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Define ambiente (você pode sobrescrever em produção)
ENV ASPNETCORE_ENVIRONMENT=Development

# Copia os arquivos da etapa de build
COPY --from=build /app/publish .

# Define o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "popfragg.Api.dll"]
