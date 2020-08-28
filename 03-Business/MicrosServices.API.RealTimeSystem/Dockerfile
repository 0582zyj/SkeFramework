#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["03-Business/MicrosServices.API.RealTimeSystem/MicrosServices.API.RealTimeSystem.csproj", "03-Business/MicrosServices.API.RealTimeSystem/"]
RUN dotnet restore "03-Business/MicrosServices.API.RealTimeSystem/MicrosServices.API.RealTimeSystem.csproj"
COPY . .
WORKDIR "/src/03-Business/MicrosServices.API.RealTimeSystem"
RUN dotnet build "MicrosServices.API.RealTimeSystem.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MicrosServices.API.RealTimeSystem.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MicrosServices.API.RealTimeSystem.dll"]