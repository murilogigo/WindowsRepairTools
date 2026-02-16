; Inno Setup script for WindowsRepairTools
; Build steps (PowerShell):
;   dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
; Then open this .iss in Inno Setup and click Build.

#define AppName "Window Repair tools"
#define AppVersion "1.0.1.0"
#define AppExeName "WindowsRepairTools.exe"
#define PublishDir "bin\\Release\\net8.0-windows\\win-x64\\publish"

[Setup]
AppId={{A5B13F2C-6BC5-4FCE-8F6A-8A8C827AF1BE}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#AppName}
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
DisableProgramGroupPage=yes
OutputDir=dist
OutputBaseFilename={#AppName}_Setup_{#AppVersion}
SetupIconFile=app.ico
UninstallDisplayIcon={app}\{#AppExeName}
Compression=lzma
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=admin
LicenseFile=License.rtf

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "brazilianportuguese"; MessagesFile: "compiler:Languages\\BrazilianPortuguese.isl"

[Files]
Source: "{#PublishDir}\\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\\{#AppName}"; Filename: "{app}\\{#AppExeName}"

[Run]
Filename: "{app}\\{#AppExeName}"; Description: "Executar {#AppName}"; Flags: nowait postinstall skipifsilent
