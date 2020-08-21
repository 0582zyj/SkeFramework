FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["03-Business/MicrosServices.API.AdminServer/MicrosServices.API.AdminServer.csproj", "03-Business/MicrosServices.API.AdminServer/"]
RUN dotnet restore "03-Business/MicrosServices.API.AdminServer/MicrosServices.API.AdminServer.csproj"
COPY . .
WORKDIR "/src/03-Business/MicrosServices.API.AdminServer"
RUN dotnet build "MicrosServices.API.AdminServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MicrosServices.API.AdminServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MicrosServices.API.AdminServer.dll"]