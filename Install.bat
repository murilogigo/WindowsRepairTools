@echo off
setlocal enabledelayedexpansion

REM Verificar se está rodando com privilégios de administrador
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo Iniciando com privilégios de administrador...
    powershell -Command "Start-Process cmd -ArgumentList '/c %~s0' -Verb RunAs"
    exit /b
)

echo.
echo ===================================
echo Windows Repair Tools - Instalador
echo ===================================
echo.

REM Verificar se .NET 8 está instalado
dotnet --version >nul 2>&1
if !errorLevel! equ 0 (
    for /f "tokens=1" %%i in ('dotnet --version') do (
        set "dotnet_version=%%i"
    )
    
    REM Verificar se é versão 8.x
    echo Detected .NET version: !dotnet_version!
    echo %dotnet_version% | findstr /R "^8\." >nul
    if !errorLevel! equ 0 (
        echo .NET 8 já está instalado!
        goto :start_app
    )
)

REM .NET 8 não está instalado - fazer download e instalar
echo .NET 8 não foi detectado. Instalando...
echo.

REM Fazer download do .NET 8 installer
echo Baixando .NET 8 Runtime Installer...
powershell -Command "& {[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; (New-Object Net.WebClient).DownloadFile('https://aka.ms/dotnet/8/windowsdesktop-runtime-installer', '%temp%\dotnet-installer.exe')}"

if not exist "%temp%\dotnet-installer.exe" (
    echo Erro: Falha ao baixar o instalador do .NET 8
    echo Visite https://dotnet.microsoft.com/download/dotnet/8.0 para instalar manualmente
    pause
    exit /b 1
)

REM Executar instalador
echo Instalando .NET 8...
"%temp%\dotnet-installer.exe" /quiet /norestart

REM Aguardar conclusão
timeout /t 5 /nobreak

REM Limpar arquivo de instalação
del "%temp%\dotnet-installer.exe" 2>nul

:start_app
echo.
echo Iniciando Windows Repair Tools...
echo.

REM Procurar e executar WindowsRepairTools.exe
if exist "WindowsRepairTools.exe" (
    start "" "WindowsRepairTools.exe"
) else (
    echo Erro: WindowsRepairTools.exe não encontrado no diretório atual
    pause
    exit /b 1
)

echo Instalação concluída!
exit /b 0
