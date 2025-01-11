@echo off

set "SourcePath=%~dp0..\src"

if not exist "%SourcePath%" (
    echo ERROR: Source directory does not exist: %SourcePath%
    pause
    exit /b 1
)

