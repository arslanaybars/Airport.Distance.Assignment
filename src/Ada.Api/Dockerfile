FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Ada.Api/Ada.Api.csproj", "src/Ada.Api/"]
RUN dotnet restore "src/Ada.Api/Ada.Api.csproj"
COPY . .
WORKDIR "/src/src/Ada.Api"
RUN dotnet build "Ada.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ada.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ada.Api.dll"]