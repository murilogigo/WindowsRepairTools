# Windows Repair Tools v1.0.0

## 📋 Descrição
Ferramenta moderna de reparo e limpeza do Windows com interface elegante e intuitiva.

## ✨ Funcionalidades

### 🔧 Reparos
- **Resetar Windows Update**: Reseta componentes do serviço de atualização
- **Executar SFC**: Verifica e repara integridade de arquivos do sistema
- **Executar DISM**: Repara imagem do Windows
- **Reparar Boot**: Reconstrói arquivos de inicialização
- **Limpar Registro**: Remove caches e entradas órfãs do registro

### 🧹 Limpeza
- **Apagar Temporários**: Remove arquivos temporários do sistema
- **Limpar Cache DNS**: Limpa cache de resolução de nomes
- **Resetar Microsoft Store**: Reinicia a Windows Store
- **Verificar Disco (CHKDSK)**: Verifica integridade do disco rígido

### ⬆️ Atualizações
- **Atualizar Sistema**: Instala atualizações do Windows Update
- **Atualizar Programas**: Atualiza programas via winget
- **Atualizar Drivers**: Verifica drivers desatualizados via PSWindowsUpdate

## 🚀 Instalação

### Requisitos
- Windows 10/11
- .NET 8 Desktop Runtime (instalado automaticamente)
- Privilégios de Administrador

### Quick Start

**Opção 1: Batch (Recomendado)**
```cmd
Install.bat
```

**Opção 2: PowerShell**
```powershell
powershell -ExecutionPolicy Bypass -File Install.ps1
```

## 🎨 Interface

- **Layout Moderno**: Design dark mode elegante com tema cyan/azul
- **3 Guias Organizadas**: Reparos, Limpeza e Atualizações
- **Ícones Personalizados**: Cada ação tem um ícone único desenhado
- **Barra de Progresso**: Feedback visual durante execução
- **Log em Tempo Real**: Acompanhe todos os comandos e resultados
- **Status Admin**: Indicador visual de privilégios de administrador

## 📌 Notas Importantes

- ⚠️ **Sempre execute como Administrador**
- ⚠️ Faça backup dos arquivos importantes antes de usar
- ⚠️ Algumas operações requerem reinicialização do sistema
- ⚠️ CHKDSK será agendado para execução na próxima reinicialização

## 🔒 Segurança

- Tratamento de erros robusto
- Confirmações automáticas para operações críticas
- Backup automático do BCD ao reparar boot
- Sem requisitos de conexão com internet (exceto PSWindowsUpdate)

## 📦 Empacotamento

Versão: **1.0.0**
Data: 02/02/2026
Plataforma: Windows (.NET 8)

## 🌐 Links

- **GitHub**: https://github.com/murilogigo/WindowsRepairTools
- **.NET 8**: https://dotnet.microsoft.com/download/dotnet/8.0
- **Windows Update**: https://support.microsoft.com/pt-br/windows

## 📝 Licença

Projeto pessoal para manutenção do Windows.

---
**Desenvolvido por Murilo** | 2026
