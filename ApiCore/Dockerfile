# Usar la imagen base oficial de .NET SDK para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecer el directorio de trabajo
WORKDIR /app

# Copiar los archivos del proyecto y restaurar las dependencias
COPY *.csproj .
RUN dotnet restore

# Copiar el resto de los archivos del proyecto y construir el proyecto
COPY . .
RUN dotnet publish -c Release -o out

# Usar la imagen base oficial de .NET Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Establecer el directorio de trabajo
WORKDIR /app

# Copiar los archivos construidos desde la fase anterior
COPY --from=build /app/out .


# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ApiCore.dll"]
