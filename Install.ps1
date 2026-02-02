# Windows Repair Tools - Installer PowerShell Script

param(
    [switch]$Silent = $false
)

# Verificar privilégios de administrador
$isAdmin = [Security.Principal.WindowsIdentity]::GetCurrent().Groups -contains [Security.Principal.SecurityIdentifier]'S-1-5-32-544'
if (-not $isAdmin) {
    Write-Host "Elevando para modo Administrador..." -ForegroundColor Yellow
    Start-Process powershell -ArgumentList "-File `"$PSCommandPath`" -Silent:$Silent" -Verb RunAs
    exit
}

Write-Host ""
Write-Host "===================================" -ForegroundColor Cyan
Write-Host "Windows Repair Tools - Installer" -ForegroundColor Cyan
Write-Host "===================================" -ForegroundColor Cyan
Write-Host ""

# Verificar .NET 8
Write-Host "Verificando .NET 8..." -ForegroundColor Yellow
$dotnetVersion = & dotnet --version 2>$null

if ($dotnetVersion) {
    if ($dotnetVersion -match "^8\.") {
        Write-Host ".NET 8 já está instalado: $dotnetVersion" -ForegroundColor Green
    } else {
        Write-Host ".NET 8 não foi detectado. Versão atual: $dotnetVersion" -ForegroundColor Yellow
        Write-Host "Instalando .NET 8 Desktop Runtime..." -ForegroundColor Yellow
        
        # Download e instalação
        $installerUrl = "https://aka.ms/dotnet/8/windowsdesktop-runtime-installer"
        $installerPath = "$env:TEMP\dotnet-installer.exe"
        
        Write-Host "Baixando instalador..." -ForegroundColor Yellow
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
        (New-Object Net.WebClient).DownloadFile($installerUrl, $installerPath)
        
        if (Test-Path $installerPath) {
            Write-Host "Executando instalador..." -ForegroundColor Yellow
            & $installerPath /quiet /norestart
            
            Start-Sleep -Seconds 5
            Remove-Item $installerPath -Force -ErrorAction SilentlyContinue
            Write-Host ".NET 8 instalado com sucesso!" -ForegroundColor Green
        } else {
            Write-Host "Erro ao baixar o instalador" -ForegroundColor Red
            exit 1
        }
    }
} else {
    Write-Host "Erro: .NET não está instalado" -ForegroundColor Red
    exit 1
}

# Iniciar aplicação
Write-Host ""
Write-Host "Iniciando Windows Repair Tools..." -ForegroundColor Green

if (Test-Path "WindowsRepairTools.exe") {
    & ".\WindowsRepairTools.exe"
} else {
    Write-Host "Erro: WindowsRepairTools.exe não encontrado" -ForegroundColor Red
    Read-Host "Pressione Enter para sair"
    exit 1
}
