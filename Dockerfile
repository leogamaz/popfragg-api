# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo para a imagem
COPY . .

# Restaura dependências
RUN dotnet restore

# Publica o projeto principal
RUN dotnet publish fromshot-api/fromshot-api.csproj -c Release -o /app/publish

# Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos da publicação
COPY --from=build /app/publish .

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "fromshot-api.dll"]
