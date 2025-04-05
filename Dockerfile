# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o csproj e restaura dependências (melhor uso de cache)
COPY fromshot-api/*.csproj ./fromshot-api/
RUN dotnet restore ./fromshot-api/fromshot-api.csproj

# Copia o restante do código
COPY . .

# Publica o projeto
RUN dotnet publish fromshot-api/fromshot-api.csproj -c Release -o /app/publish

# Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Define ambiente
ENV ASPNETCORE_ENVIRONMENT=Development

# Copia os arquivos publicados
COPY --from=build /app/publish .

# Executa a aplicação
ENTRYPOINT ["dotnet", "fromshot-api.dll"]
