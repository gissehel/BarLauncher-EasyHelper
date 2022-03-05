REM @echo off

SET VERSION=%1
SET NUGET_TOKEN=%2

IF EXIST .\create-package.cmd cd .. 

CALL scripts\environnement.cmd

CALL :SET_BUILD_DATE

CALL :CREATE_NUGET BarLauncher.EasyHelper
CALL :CREATE_NUGET BarLauncher.EasyHelper.Test.Mock
CALL :CREATE_NUGET BarLauncher.EasyHelper.Wox

:SET_BUILD_DATE

for /f %%# in ('wMIC Path Win32_LocalTime Get /Format:value') do @for /f %%@ in ("%%#") do @set %%@
SET year=0000%year%
SET month=00%month%
SET day=00%day%
SET hour=00%hour%
SET minute=00%minute%
SET second=00%second%
SET year=%year:~-4%
SET month=%month:~-2%
SET day=%day:~-2%
SET hour=%hour:~-2%
SET minute=%minute:~-2%
SET second=%second:~-2%
SET BUILD_DATE=%year%%month%%day%-%hour%%minute%%second%

GOTO FIN

:SET_NUGET_PACKAGE_VERSION
IF NOT %VERSION%==dev GOTO NODEV
for /f "usebackq tokens=1* delims=: " %%i in (`scripts\sed.exe -e "s/.*<\(.*\)>\(.*\)<.*/\1:\2/g"  %NUGET_PACKAGE_PATH%\%NUGET_PACKAGE_NAME%.csproj`) do (
  if /i "%%i"=="Version" set VERSION=%%j
)
SNAPSHOT=SNAPSHOT
:NODEV
SET NUGET_PACKAGE_VERSION=%VERSION%
REM IF %SNAPSHOT%==SNAPSHOT GOTO IS_SNAPSHOT
REM GOTO FIN
REM :IS_SNAPSHOT
REM SET NUGET_PACKAGE_VERSION=%NUGET_PACKAGE_VERSION%-%BUILD_DATE%
GOTO FIN

:CREATE_NUGET
SET NUGET_PACKAGE_NAME=%1
SET NUGET_PACKAGE_PATH=%1
SET NUGET_PACKAGE_DESCRIPTION_ADD=%2
CALL :SET_NUGET_PACKAGE_VERSION

SET NUGET_REPOURL=https://api.nuget.org/v3/index.json
SET PACKAGE_FILENAME=%NUGET_PACKAGE_NAME%.%NUGET_PACKAGE_VERSION%.nupkg
SET NUGET_PACKAGE_NAME_PATTERN={package_name}
SET NUGET_PACKAGE_VERSION_PATTERN={version}
SET NUGET_PACKAGE_DESCRIPTION_ADD_PATTERN={description_add}
SET NUGET_PACKAGE_PATH_PATTERN={package_path}

SET NUSPEC_TEMPLATE=%NUGET_PACKAGE_PATH%\template.nuspec
SET NUSPEC_FILE=tempnuspec.nuspec

scripts\sed.exe -e "s/%NUGET_PACKAGE_VERSION_PATTERN%/%NUGET_PACKAGE_VERSION%/g; s/%NUGET_PACKAGE_NAME_PATTERN%/%NUGET_PACKAGE_NAME%/g; s/%NUGET_PACKAGE_DESCRIPTION_ADD_PATTERN%/%NUGET_PACKAGE_DESCRIPTION_ADD%/g; s/%NUGET_PACKAGE_PATH_PATTERN%/%NUGET_PACKAGE_PATH%/g;" "%NUSPEC_TEMPLATE%" > "%NUSPEC_FILE%" 
scripts\nuget.exe pack "%NUSPEC_FILE%"

IF "%NUGET_TOKEN%"=="" GOTO NOUPLOAD
scripts\nuget.exe push "%PACKAGE_FILENAME%" "%NUGET_TOKEN%" -Source "%NUGET_REPOURL%"
:NOUPLOAD

del "%NUSPEC_FILE%"
REM del "%PACKAGE_FILENAME%"
GOTO FIN

:FIN
