# Etapa de construcci�n
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos del proyecto y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos y compilar la aplicaci�n
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa de ejecuci�n
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Exponer el puerto en el que la aplicaci�n escucha
EXPOSE 80

# Comando para ejecutar la aplicaci�n
ENTRYPOINT ["dotnet", "ApiPortafolio.dll"]
