using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsRepairTools
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Verifica se está em modo administrador
            if (!IsRunAsAdmin())
            {
                try
                {
                    var proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = Application.ExecutablePath;
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.Verb = "runas";
                    proc.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Não foi possível iniciar em modo administrador: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static bool IsRunAsAdmin()
        {
            try
            {
                var wi = System.Security.Principal.WindowsIdentity.GetCurrent();
                var wp = new System.Security.Principal.WindowsPrincipal(wi);
                return wp.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
            catch { return false; }
        }

        public static class RepairService
        {
            public static Task ResetWindowsUpdateAsync(Action<string> log)
            {
                return RunCommandAsync(
                    "powershell -Command \"" +
                    "Stop-Service -Name wuauserv -Force -ErrorAction SilentlyContinue; " +
                    "Stop-Service -Name bits -Force -ErrorAction SilentlyContinue; " +
                    "Stop-Service -Name cryptsvc -Force -ErrorAction SilentlyContinue; " +
                    "Rename-Item -Path 'C:\\\\Windows\\\\SoftwareDistribution' -NewName 'SoftwareDistribution.bak' -Force -ErrorAction SilentlyContinue; " +
                    "Rename-Item -Path 'C:\\\\Windows\\\\System32\\\\catroot2' -NewName 'catroot2.bak' -Force -ErrorAction SilentlyContinue; " +
                    "Start-Service -Name wuauserv -ErrorAction SilentlyContinue; " +
                    "Start-Service -Name bits -ErrorAction SilentlyContinue; " +
                    "Start-Service -Name cryptsvc -ErrorAction SilentlyContinue; " +
                    "Write-Host 'Componentes do Windows Update resetados com sucesso.'\"",
                    "Resetando componentes do Windows Update...",
                    log);
            }

            public static Task RunSfcAsync(Action<string> log)
            {
                return RunCommandAsync("sfc /scannow", "Executando SFC...", log);
            }

            public static Task RunDismAsync(Action<string> log)
            {
                return RunCommandAsync("DISM /Online /Cleanup-Image /RestoreHealth", "Executando DISM...", log);
            }

            public static Task ClearTempAsync(Action<string> log)
            {
                return RunCommandAsync("del /s /f /q %TEMP%\\*.* && del /s /f /q C:\\Windows\\Temp\\*.*", "Apagando arquivos temporários...", log);
            }

            public static async Task UpdateWindowsAsync(Action<string> log)
            {
                var checkModule = await RunCommandAsync("powershell -Command \"Get-Module -ListAvailable PSWindowsUpdate\"", "Verificando módulo PSWindowsUpdate...", log);
                if (!string.IsNullOrEmpty(checkModule) && checkModule.Contains("PSWindowsUpdate"))
                {
                    log?.Invoke("Módulo PSWindowsUpdate já instalado.\r\n");
                }
                else
                {
                    await RunCommandAsync("powershell -Command \"Install-PackageProvider -Name NuGet -Force\"", "Instalando NuGet Provider...", log);
                    await RunCommandAsync("powershell -Command \"Set-PSRepository -Name 'PSGallery' -InstallationPolicy Trusted\"", "Definindo política do repositório PSGallery...", log);
                    await RunCommandAsync("powershell -Command \"Set-ExecutionPolicy RemoteSigned -Scope CurrentUser -Force\"", "Definindo ExecutionPolicy para RemoteSigned...", log);
                    await RunCommandAsync("powershell -Command \"Install-Module PSWindowsUpdate -Force -SkipPublisherCheck\"", "Instalando módulo PSWindowsUpdate...", log);
                }

                var checkImport = await RunCommandAsync("powershell -Command \"Get-Module PSWindowsUpdate\"", "Verificando importação do módulo PSWindowsUpdate...", log);
                if (!string.IsNullOrEmpty(checkImport) && checkImport.Contains("PSWindowsUpdate"))
                {
                    log?.Invoke("Módulo PSWindowsUpdate já importado.\r\n");
                }
                else
                {
                    await RunCommandAsync("powershell -Command \"Import-Module PSWindowsUpdate\"", "Importando módulo PSWindowsUpdate...", log);
                }

                await RunCommandAsync("powershell -Command \"Get-WindowsUpdate -AcceptAll -Install -AutoReboot\"", "Buscando e instalando atualizações do Windows...", log);
            }

            public static Task UpdateProgramsAsync(Action<string> log)
            {
                return RunCommandAsync("winget upgrade --all --silent", "Atualizando todos os programas via winget...", log);
            }

            public static Task CheckDiskAsync(Action<string> log)
            {
                return RunCommandAsync(
                    "powershell -Command \"" +
                    "echo Y | chkdsk C: /F /R /X; " +
                    "Write-Host 'Verificação de disco agendada para a próxima reinicialização.'\"",
                    "Verificando integridade do disco (CHKDSK)...",
                    log);
            }

            public static Task FlushDnsAsync(Action<string> log)
            {
                return RunCommandAsync(
                    "powershell -Command \"" +
                    "ipconfig /flushdns; " +
                    "Write-Host 'Cache DNS limpo com sucesso.'\"",
                    "Limpando cache DNS...",
                    log);
            }

            public static Task RepairBootAsync(Action<string> log)
            {
                return RunCommandAsync(
                    "powershell -Command \"" +
                    "bcdedit /export C:\\BCD_Backup; " +
                    "bcdboot C:\\Windows /s C: /f ALL; " +
                    "Write-Host 'Configuração de boot restaurada. Backup salvo em C:\\BCD_Backup'\"",
                    "Reparando boot...",
                    log);
            }

            public static Task CleanRegistryAsync(Action<string> log)
            {
                return RunCommandAsync(
                    "powershell -Command \"" +
                    "Remove-Item -Path 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\RunMRU' -Recurse -Force -ErrorAction SilentlyContinue; " +
                    "Remove-Item -Path 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\TypedPaths' -Recurse -Force -ErrorAction SilentlyContinue; " +
                    "Write-Host 'Limpeza de registro concluída.'\"",
                    "Limpando registro do Windows...",
                    log);
            }

            public static Task ResetWindowsStoreAsync(Action<string> log)
            {
                return RunCommandAsync(
                    "powershell -Command \"" +
                    "Start-Process wsreset.exe -NoNewWindow -Wait; " +
                    "Write-Host 'Microsoft Store reiniciado com sucesso.'\"",
                    "Resetando Microsoft Store...",
                    log);
            }

            public static Task UpdateDriversAsync(Action<string> log)
            {
                return RunCommandAsync(
                    "powershell -Command \"" +
                    "Install-PackageProvider -Name NuGet -Force -ErrorAction SilentlyContinue; " +
                    "Set-PSRepository -Name 'PSGallery' -InstallationPolicy Trusted -ErrorAction SilentlyContinue; " +
                    "if (-not (Get-Module -ListAvailable -Name PSWindowsUpdate)) { " +
                    "Install-Module PSWindowsUpdate -Force -SkipPublisherCheck -ErrorAction SilentlyContinue; " +
                    "} " +
                    "Import-Module PSWindowsUpdate -ErrorAction SilentlyContinue; " +
                    "Get-WindowsUpdate -MicrosoftUpdate -Verbose -ErrorAction SilentlyContinue | ? {$_.Title -match 'Driver' -or $_.Title -match 'Controlador'} | ForEach-Object { Write-Host \\\"Encontrado: $($_.Title)\\\"; }; " +
                    "Write-Host 'Verificação de drivers PSWindowsUpdate concluída.'\"",
                    "Verificando drivers usando PSWindowsUpdate...",
                    log);
            }

            public static async Task<string> RunCommandAsync(string command, string description, Action<string> log)
            {
                log?.Invoke($"{DateTime.Now:HH:mm:ss} - {description}\r\n");
                string output = "";
                try
                {
                    log?.Invoke($"Comando: {command}\r\n");
                    var proc = new Process();
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.Arguments = $"/c {command}";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.CreateNoWindow = true;

                    proc.OutputDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            log?.Invoke(e.Data + "\r\n");
                            output += e.Data + "\n";
                        }
                    };
                    proc.ErrorDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            log?.Invoke("ERRO: " + e.Data + "\r\n");
                            output += e.Data + "\n";
                        }
                    };

                    proc.Start();
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    await Task.Run(() => proc.WaitForExit());
                    log?.Invoke($"Processo finalizado. Código de saída: {proc.ExitCode}\r\n");
                }
                catch (Exception ex)
                {
                    log?.Invoke($"Erro: {ex.Message}\r\n");
                }
                return output;
            }
        }
    }
}