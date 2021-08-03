#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Project.Web/Project.Web.csproj", "Project.Web/"]
COPY ["Project.Domain/Project.Domain.csproj", "Project.Domain/"]
COPY ["Project.Context/Project.Context.csproj", "Project.Context/"]
RUN dotnet restore "Project.Web/Project.Web.csproj"
COPY . .
WORKDIR "/src/Project.Web"
RUN dotnet build "Project.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.Web.dll"]