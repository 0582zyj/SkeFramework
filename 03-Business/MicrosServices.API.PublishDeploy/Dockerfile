FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["03-Business/MicrosServices.API.PublishDeploy/MicrosServices.API.PublishDeploy.csproj", "03-Business/MicrosServices.API.PublishDeploy/"]
RUN dotnet restore "03-Business/MicrosServices.API.PublishDeploy/MicrosServices.API.PublishDeploy.csproj"
COPY . .
WORKDIR "/src/03-Business/MicrosServices.API.PublishDeploy"
RUN dotnet build "MicrosServices.API.PublishDeploy.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MicrosServices.API.PublishDeploy.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MicrosServices.API.PublishDeploy.dll"]