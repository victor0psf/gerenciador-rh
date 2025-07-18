#  Prova Prática – Dev Júnior

Projeto desenvolvido como parte da prova prática para a vaga de Desenvolvedor Júnior, utilizando .NET 9, Angular, WebForms e SQL Server. A aplicação permite gerenciar funcionários, cadastrar férias e gerar relatórios em PDF.

---

##  Pré-Requisitos

### Banco de Dados: SQL Server + SSMS

- Instale o [SQL Server (Developer ou Express)](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)  
- Instale o [SQL Server Management Studio (SSMS 20+)](https://learn.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms)

> Após instalar o SSMS, conecte-se ao servidor local (`localhost` ou `localhost\SQLEXPRESS`, se tiver usado o SQL Server Express)

Antes de rodar o projeto, certifique-se de ter instalado:

### Backend
- [.NET SDK 9.0+](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server (2019 ou superior)](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### Frontend 
- [Node.js (versão 18+)](https://nodejs.org/)
- [Angular CLI (versão 16+)](https://angular.io/cli)
- WebForms (versão 4.7.2) 
```bash
npm install -g @angular/cli
```

---

## Instruções Para Rodar O Sistema Localmente

### 🔹 Clonar o projeto
```bash
git clone https://github.com/victor0psf/gerenciador-rh.git
cd seu-repositorio
```

### 🔹 Backend (.NET)

1. Acesse a pasta `server_dotnet`:
   ```bash
   cd server_dotnet
   ```

2. Restaure os pacotes:
   ```bash
   dotnet restore
   ```

3. Atualize a string de conexão no `appsettings.json` com os dados do seu SQL Server local:
   ```json
   "ConnectionStrings": {
    "DefaultConnection": "Server=seuServidor\\SQLEXPRESS;Database=rh-gerenciador;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```
   > ⚠️ **Observação:** Essa string usa a instância padrão do SQL Server Express (`SQLEXPRESS`). Se estiver usando uma instância diferente ou o SQL Server completo, substitua o nome do servidor conforme necessário.

4. Rode as migrações e crie o banco:
   ```bash
   dotnet ef database update
   ```

5. Inicie a API:
   ```bash
   dotnet run
   ```

6. Acesse a documentação Swagger:
   ```
   http://localhost:5114/swagger
   ```

---

### 🔹 Frontend Angular

1. Acesse a pasta do projeto Angular:
   ```bash
   cd angular-client
   ```

2. Instale as dependências:
   ```bash
   npm install
   ```

3. Inicie o frontend:
   ```bash
   ng serve
   ```

4. Acesse:
   ```
   http://localhost:4200
   ```

---

## Considerações Sobre O Banco De Dados

- O projeto utiliza **SQL Server**.
- As entidades e relacionamentos são criados automaticamente via `EF Core Migrations`.
- A string de conexão pode ser adaptada para autenticação SQL:
  ```json
  "Server=localhost;Database=ProvaDevJr;User Id=sa;Password=SuaSenha;"
  ```
  ou com o Express
  ```json
  "Server=seuServidor\\SQLEXPRESS;Database=rh-gerenciador;Trusted_Connection=True;TrustServerCertificate=True"
  ```

---

## Observações Finais

- A aplicação conta com dois frontends: Angular e WebForms.
- O sistema é capaz de realizar **CRUD completo**, **relatórios em PDF** e **cadastro/gerenciamento de funcionários e férias**.

---
