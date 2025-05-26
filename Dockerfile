# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy the csproj and restore as distinct layers
COPY ["ScoringSystem_web_api.csproj", "./"]
RUN dotnet restore "ScoringSystem_web_api.csproj"

# copy everything else and publish (AOT trim optional)
COPY . .
RUN dotnet publish "ScoringSystem_web_api.csproj" \
    -c Release -o /app/publish

# ---------- Runtime image ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# The app listens on http://*:8080 by convention; override if yours differs
EXPOSE 8080
ENTRYPOINT ["dotnet", "ScoringSystem_web_api.dll"]
