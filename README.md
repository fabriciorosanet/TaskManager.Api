# TaskManager.Api

API para gerenciamento de tarefas, com autenticação JWT, escrita em .NET 8, utilizando Entity Framework Core e PostgreSQL.

## Funcionalidades

- Cadastro e autenticação de usuários (JWT)
- Criar, listar, atualizar e deletar tarefas
- Marcar tarefas como concluídas/pendentes
- Filtrar tarefas por status
- Buscar tarefas por título

## Tecnologias Utilizadas

- **.NET 8**
- **Minimal APIs**
- **Entity Framework Core**
- **PostgreSQL** (via Docker)
- **FluentValidation**
- **Swagger/OpenAPI**

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop/) instalado
- Editor de código (Visual Studio, VS Code, Rider)
- Git

## Como Executar

### 1. Clone o repositório
```bash
git clone https://github.com/fabriciorosanet/TaskManager.Api.git
cd TaskManager.Api/TaskManager.Api
```

### 2. Suba o banco de dados PostgreSQL com Docker Compose
```bash
docker-compose -f Docker/docker-compose.yml up -d
```

> A senha do banco está em um arquivo secreto: `Docker/postgres_password.txt` (não versionado).

### 3. Configure a variável de ambiente para a senha do banco
```bash
export DB_PASSWORD=$(cat Docker/postgres_password.txt)
```

### 4. Restaure as dependências e aplique as migrations
```bash
dotnet restore
dotnet ef database update
```

### 5. Execute a aplicação
```bash
dotnet run
```



## Autenticação JWT no Swagger
1. Realize o login pelo endpoint `/auth/login` e copie o token JWT retornado.
2. Clique em **Authorize** no Swagger e cole: `Bearer {tokenGerado}`.
3. Agora, os endpoints protegidos aceitarão suas requisições.



---

Desenvolvido por Fabricio Rosa
