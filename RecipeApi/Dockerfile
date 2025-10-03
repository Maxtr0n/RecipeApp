FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# install curl for healthchecks
USER root
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
# switch back to non-root user if provided by Docker build args
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# --- Restore Layer ---
# Copy solution + props files first
COPY Directory.Build.props .
COPY Directory.Packages.props .
COPY RecipeApi.sln .

# Copy only project files (so restore cache is reused unless deps change)
COPY RecipeApi/RecipeApi.csproj RecipeApi/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY SharedKernel/SharedKernel.csproj SharedKernel/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

# Restore dependencies using the solution
RUN dotnet restore Application/Application.csproj \
    && dotnet restore Domain/Domain.csproj \
    && dotnet restore SharedKernel/SharedKernel.csproj \
    && dotnet restore Infrastructure/Infrastructure.csproj \
    && dotnet restore RecipeApi/RecipeApi.csproj

# --- Build Layer ---
# Copy the rest of the source AFTER restore
COPY . .

WORKDIR /src/RecipeApi
RUN dotnet build "RecipeApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RecipeApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecipeApi.dll"]
