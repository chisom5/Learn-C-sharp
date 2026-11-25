FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5135

ENV ASPNETCORE_URLS=http://+:5135

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /Presentation
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY [".editorconfig", "."]
COPY ["Application/DevHabit.Api.Application.csproj", "Application/"]
COPY ["Domain/DevHabit.Api.Domain.csproj", "Domain/"]
COPY ["Infrastructure/DevHabit.Api.Infrastructure.csproj", "Infrastructure/"]
COPY ["Presentation/DevHabit.Api.csproj", "Presentation/"]
RUN dotnet restore "Presentation/DevHabit.Api.csproj"
COPY . .
RUN dotnet build "Presentation/DevHabit.Api.csproj" -c $configuration -o /app/build 

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Presentation/DevHabit.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevHabit.Api.dll"]
