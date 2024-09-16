# Projeto Integrador - 4° BCC (Unimar)

Projeto desenvolvido durante o 2° semestre de 2024 na disciplina Fábrica de Projetos Ágeis IV como método avaliativo. Apoiado pela empresa ***Interfocus***.

## Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) (versão 6.0 ou superior)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) ou [Visual Studio Code](https://code.visualstudio.com/)


## Configuração do Ambiente de Desenvolvimento (Linux)

Para um desenvolvimento eficiente de projetos ASP.NET Core no Visual Studio Code (VSCode) em um ambiente Linux, siga estas recomendações para extensões e configurações:

### Extensões Recomendadas

1. **C#**
   - **ID**: `ms-dotnettools.csharp`
   - **Descrição**: Suporte completo para C# com IntelliSense e debugging.
   - **Instalação**: Abra o VSCode, vá para **Extensions** (`Ctrl+Shift+X`), pesquise por `C#`, e clique em **Install**.

2. **ASP.NET Core Snippets**
   - **ID**: `csharpdotnet`
   - **Descrição**: Trechos de código úteis para ASP.NET Core.
   - **Instalação**: No VSCode, vá para **Extensions**, pesquise por `ASP.NET Core Snippets`, e clique em **Install**.

3. **HTML CSS Support**
   - **ID**: `eBay.ebay-html-css-support`
   - **Descrição**: Suporte para CSS em arquivos HTML.
   - **Instalação**: No VSCode, vá para **Extensions**, pesquise por `HTML CSS Support`, e clique em **Install**.

4. **Prettier - Code Formatter**
   - **ID**: `esbenp.prettier-vscode`
   - **Descrição**: Formatador de código para HTML, CSS e mais.
   - **Instalação**: No VSCode, vá para **Extensions**, pesquise por `Prettier`, e clique em **Install**.

5. **Debugger for Chrome**
   - **ID**: `msjsdiag.debugger-for-chrome`
   - **Descrição**: Depuração de JavaScript no Google Chrome.
   - **Instalação**: No VSCode, vá para **Extensions**, pesquise por `Debugger for Chrome`, e clique em **Install**.

### Configurações Recomendadas

Adicione as seguintes configurações ao seu arquivo `settings.json` para otimizar o ambiente de desenvolvimento:

```json
{
  "editor.formatOnSave": true,
  "editor.defaultFormatter": "esbenp.prettier-vscode",
  "csharp.format.enable": true,
  "csharp.suppressDotnetInstallWarning": true
}
```

Para configurar o ambiente de depuração, adicione a seguinte configuração ao seu arquivo `launch.json`:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (Web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/HtmlPreviewApp/bin/Debug/net6.0/HtmlPreviewApp.dll",
      "args": [],
      "cwd": "${workspaceFolder}",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "console": "internalConsole",
      "launchBrowser": {
        "enabled": true,
        "args": "${auto-detect-url}",
        "linux": {
          "command": "xdg-open",
          "args": "${auto-detect-url}"
        }
      }
    }
  ]
}
```

Para configurar tarefas de build, adicione o seguinte ao seu arquivo `tasks.json`:

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build",
      "command": "dotnet",
      "args": [
        "build",
        "${workspaceFolder}/HtmlPreviewApp.csproj"
      ],
      "problemMatcher": [
        "$msCompile"
      ],
      "group": {
        "kind": "build",
        "isDefault": true
      }
    }
  ]
}
```

## Instruções de Abertura do Projeto

### Abrir no Visual Studio

1. **Clone o Repositório**:

```bash
git clone https://github.com/leticia-pontes/projeto-integrador-4bcc
```

2. **Navegue até o Diretório do Projeto**:

```bash
cd projeto-integrador-4bcc
```

3. **Abra o Projeto**:
- Clique em File > Open > Project/Solution.
- Navegue até o arquivo HtmlPreviewApp.sln e clique em Open.

    - Caso o arquivo não exista, selecione o arquivo HtmlPreviewApp.csproj. 

### Abrir no Visual Studio Code

1. Clone o Repositório:

```bash
git clone https://github.com/leticia-pontes/projeto-integrador-4bcc
```

2. **Navegue até o Diretório do Projeto**:

```bash
cd projeto-integrador-4bcc
```

3. **Abra o Projeto**:
- Execute o comando `code .` no terminal para abrir o diretório atual no Visual Studio Code.
- O Visual Studio Code reconhecerá automaticamente o arquivo .csproj e permitirá que você trabalhe no projeto.

## Executar a Aplicação

1. **Navegue até o Diretório do Projeto**:

```bash
cd htmlpreviewapp
```

2. **Execute o Projeto**:

```bash
dotnet run
```
- Clicar no link (Ctrl + Clique) disponível no prompt de comando.
```bash
Compilando...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:port
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: <path>
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
```
- A aplicação estará disponível em https://localhost:5001 por padrão.

## Estrutura do Projeto

- Controllers: Contém os controladores da aplicação.
- Views: Contém as views da aplicação.
- Models: Contém os modelos da aplicação.
- wwwroot: Contém arquivos estáticos como CSS e JavaScript.
- Startup.cs: Configura os serviços e o pipeline de requisições da aplicação.
- Program.cs: Ponto de entrada da aplicação.
