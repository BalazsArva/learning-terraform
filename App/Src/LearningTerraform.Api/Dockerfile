FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
ENV IsDockerBuild="true"
COPY ["LearningTerraform.Api/LearningTerraform.Api.csproj",                           "LearningTerraform.Api/"]
COPY ["LearningTerraform.BusinessLogic/LearningTerraform.BusinessLogic.csproj",       "LearningTerraform.BusinessLogic/"]
COPY ["LearningTerraform.DataAccess.MsSql/LearningTerraform.DataAccess.MsSql.csproj", "LearningTerraform.DataAccess.MsSql/"]
RUN dotnet restore "LearningTerraform.Api/LearningTerraform.Api.csproj"
COPY . .
WORKDIR "/src/LearningTerraform.Api"
RUN dotnet build "LearningTerraform.Api.csproj" -c Release --no-restore

FROM build AS publish
RUN dotnet publish "LearningTerraform.Api.csproj" -c Release --no-build -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LearningTerraform.Api.dll"]
