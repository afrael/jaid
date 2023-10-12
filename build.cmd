@echo off

REM Variables
set "SLNDIR=%~dp0"

REM Build
dotnet build "%SLNDIR%\jaid.sln" --nologo || exit /b

REM Test
REM dotnet test "%SLNDIR%\gitdoc.tests" --nologo --no-build
