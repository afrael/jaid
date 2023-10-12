@echo off

REM Variables
set "SLNDIR=%~dp0src"

REM Restore + Build
dotnet build "%SLNDIR%\jaid-cli" --nologo --verbosity quiet || exit /b

REM Run
dotnet run -p "%SLNDIR%\jaid-cli" --no-build -- %*
