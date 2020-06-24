dotnet publish MicrosServices.API.PublishDeploy.csproj /p:PublishProfile=IISProfile /p:AllowUntrustedCertificate=true --self-contained true
pause
exit 0