# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CSE-325-Team-16-Eunomia.csproj", "./"]
RUN dotnet restore "CSE-325-Team-16-Eunomia.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CSE-325-Team-16-Eunomia.dll"]