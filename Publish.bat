@echo off
rem ---------------------------------
rem ---------------------------------

cd /d %~dp0

echo %date%

set /p input="Compile now(y/n): "

if not %input% == y (
    pause
    exit 0
)

for /f "tokens=1-4 delims=/ " %%i in ("%date%") do (
     set year=%%i
     set month=%%j
     set day=%%k
     set down=%%l
)
set datestr=%year%%month%%day%

rem MSBuild.exe 路径
set MSBUILD_PATH=E:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe
rem solution 路径
set SOLUTION_FILE=D:\Github\SkeFramework\SkeFramework.sln
rem Release / Debug
set BUILD_CONFIG=Release
rem build / rebuild / clean
set BUILD_TYPE=clean;build
rem Any CPU / x64 / x86
set BUILD_PLATFORM="Any CPU"
set OUTPUT_PATH=C:\JProject\Web\%datestr%

@echo Compile Start

"%MSBUILD_PATH%" %SOLUTION_FILE% /t:%BUILD_TYPE% /p:Configuration=%BUILD_CONFIG%;Platform=%BUILD_PLATFORM%;OutDir=%OUTPUT_PATH%\%BUILD_CONFIG% /m

@echo Compi  End

pause
exit 0