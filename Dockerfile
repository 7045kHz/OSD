#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["OSD.SysMapper/OSD.SysMapper.csproj", "OSD.SysMapper/"]
COPY ["OSD.RazorData/OSD.RazorData.csproj", "OSD.RazorData/"]
RUN dotnet restore "OSD.SysMapper/OSD.SysMapper.csproj"
COPY . .
WORKDIR "/src/OSD.SysMapper"
RUN dotnet build "OSD.SysMapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OSD.SysMapper.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OSD.SysMapper.dll"]
