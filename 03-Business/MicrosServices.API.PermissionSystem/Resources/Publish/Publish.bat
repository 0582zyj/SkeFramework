@ECHO OFF

SET framework=v4.0.30319

"E:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"  D:\Github\SkeFramework\03-Business\MicrosServices.API.PermissionSystem\MicrosServices.API.PermissionSystem.csproj  /t:Rebuild /P:Configuration=Release /p:VisualStudioVersion=15.0 /p:DeployOnBuild=True;PublishProfile=IISLocal.pubxml /p:WebProjectOutputDir=C:\JProject\Web /p:OutputPath=C:\JProject\Web\bin 

ECHO  publish finsh

pause
