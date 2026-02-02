# 🔨 Criando o Instalador MSI

## Pré-requisitos

### WiX Toolset v3.11 (Obrigatório)

**Baixar e Instalar:**
1. Acesse: https://github.com/wixtoolset/wix3/releases
2. Baixe: `wix311.exe` (WiX Toolset v3.11)
3. Execute o instalador
4. Reinicie o terminal após instalação

**Verificar instalação:**
```powershell
candle.exe -?
```

## Compilar o MSI

### Opção 1: Script Automático (Recomendado)
```cmd
Build-MSI.bat
```

### Opção 2: Manual
```powershell
# 1. Compilar projeto
dotnet build -c Release

# 2. Compilar WiX source
candle.exe Installer.wxs -ext WixUIExtension -out obj\Installer.wixobj

# 3. Criar MSI
light.exe obj\Installer.wixobj -ext WixUIExtension -out WindowsRepairTools-v1.0.0.msi -sval
```

## Estrutura do Instalador

O instalador MSI inclui:
- ✅ Verificação de .NET 8 Runtime
- ✅ Instalação em `C:\Program Files\Windows Repair Tools`
- ✅ Atalho no Menu Iniciar
- ✅ Atalho na Área de Trabalho
- ✅ Entrada em Programas e Recursos
- ✅ Suporte a upgrade automático

## Testando o MSI

```cmd
# Instalar
msiexec /i WindowsRepairTools-v1.0.0.msi

# Instalar silenciosamente
msiexec /i WindowsRepairTools-v1.0.0.msi /quiet

# Desinstalar
msiexec /x WindowsRepairTools-v1.0.0.msi

# Desinstalar silenciosamente
msiexec /x WindowsRepairTools-v1.0.0.msi /quiet
```

## Personalizações

Edite `Installer.wxs` para:
- Alterar diretório de instalação
- Adicionar/remover atalhos
- Modificar ícones
- Ajustar propriedades do produto

## Troubleshooting

### Erro: "candle.exe não é reconhecido"
- WiX Toolset não está instalado ou não está no PATH
- Solução: Reinstalar WiX e reiniciar terminal

### Erro: "light.exe failed with exit code"
- Verifique se todos os arquivos em `bin\Release\net8.0-windows\` existem
- Execute `dotnet build -c Release` primeiro

### Erro: ICE validation errors
- Use `-sval` no light.exe para suprimir validações ICE
- Ou corrija conforme mensagens de erro
