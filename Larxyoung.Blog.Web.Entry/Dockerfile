FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8079

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Larxyoung.Blog.Web.Entry/Larxyoung.Blog.Web.Entry.csproj", "Larxyoung.Blog.Web.Entry/"]
COPY ["Larxyoung.Blog.Web.Core/Larxyoung.Blog.Web.Core.csproj", "Larxyoung.Blog.Web.Core/"]
COPY ["Larxyoung.Blog.Application/Larxyoung.Blog.Application.csproj", "Larxyoung.Blog.Application/"]
COPY ["Larxyoung.Blog.Core/Larxyoung.Blog.Core.csproj", "Larxyoung.Blog.Core/"]
RUN dotnet restore "Larxyoung.Blog.Web.Entry/Larxyoung.Blog.Web.Entry.csproj"
COPY . .
WORKDIR "/src/Larxyoung.Blog.Web.Entry"
RUN dotnet build "Larxyoung.Blog.Web.Entry.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Larxyoung.Blog.Web.Entry.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Larxyoung.Blog.Web.Entry.dll"]