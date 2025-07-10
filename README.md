# TaskManager API

Uma API RESTful simples e eficiente para gerenciamento de tarefas pessoais, construída com .NET 8 Minimal APIs.

## Funcionalidades

- Criar novas tarefas
- Listar todas as tarefas
- Atualizar tarefas existentes
- Deletar tarefas
- Marcar tarefas como concluídas/pendentes
- Filtrar tarefas por status
- Buscar tarefas por título

## Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **Minimal APIs** - Abordagem leve para criação de APIs
- **Entity Framework Core** - ORM para acesso a dados
- **SQLite** - Banco de dados para desenvolvimento
- **FluentValidation** - Validação de dados
- **Swagger/OpenAPI** - Documentação automática da API

##  Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado
- Editor de código (Visual Studio, VS Code, Rider)
- Git

## Como Executar

### 1. Clone o repositório
```bash
git clone https://github.com/fabriciorosanet/TaskManager.Api.git
cd TaskManager.Api
```

### 2. Restaure as dependências
```bash
dotnet restore
```

### 3. Execute a aplicação
```bash
dotnet run
```

### 4. Acesse a aplicação
- **API**: `https://localhost:xxxx` (a porta pode variar)
- **Swagger/Documentação**: `https://localhost:xxxx/swagger`

##  Documentação da API

### Endpoints Principais

#### Tarefas

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/tasks` | Lista todas as tarefas |
| `GET` | `/api/tasks/{id}` | Obtém uma tarefa específica |
| `POST` | `/api/tasks` | Cria uma nova tarefa |
| `PUT` | `/api/tasks/{id}` | Atualiza uma tarefa existente |
| `DELETE` | `/api/tasks/{id}` | Remove uma tarefa |

#### Filtros e Pesquisa

| Método | Endpoint | Parâmetros | Descrição |
|--------|----------|------------|-----------|
| `GET` | `/api/tasks?status=completed` | `status=completed\|pending` | Filtra tarefas por status |
| `GET` | `/api/tasks?search=titulo` | `search=string` | Busca tarefas por título |

### Exemplos de Uso

#### Criar uma nova tarefa
```bash
POST /api/tasks
Content-Type: application/json

{
  "title": "Estudar .NET 8",
  "description": "Aprender sobre Minimal APIs"
}
```

#### Atualizar uma tarefa
```bash
PUT /api/tasks/1
Content-Type: application/json

{
  "title": "Estudar .NET 8 - Atualizado",
  "description": "Aprender sobre Minimal APIs e Entity Framework",
  "isCompleted": true
}
```

#### Resposta padrão
```json
{
  "id": 1,
  "title": "Estudar .NET 8",
  "description": "Aprender sobre Minimal APIs",
  "isCompleted": false,
  "createdAt": "2024-01-15T10:30:00Z",
  "completedAt": null
}
```

## Estrutura do Projeto

```
TaskManager.Api/
├── Models/                   # Entidades e DTOs
│   ├── TaskItem.cs           # Modelo principal
│   ├── CreateTaskRequest.cs  # DTO para criação
│   └── UpdateTaskRequest.cs  # DTO para atualização
├── Data/                     # Contexto do banco de dados
│   └── TaskContext.cs        # DbContext do Entity Framework
├── Endpoints/                # Definição dos endpoints
│   └── TasksEndpoints.cs     # Endpoints relacionados às tarefas
├── Services/                 # Lógica de negócio
│   └── TaskService.cs        # Serviço para operações com tarefas
├── Extensions/               # Métodos de extensão
│   └── ServiceExtensions.cs  # Configurações de serviços
├── Program.cs                # Ponto de entrada da aplicação
├── tasks.db                  # Banco de dados SQLite
└── README.md                 # Este arquivo
```

## Testes

Para executar os testes:

```bash
dotnet test
```

## Tratamento de Erros

A API retorna códigos de status HTTP apropriados:

- `200 OK` - Sucesso
- `201 Created` - Recurso criado com sucesso
- `400 Bad Request` - Dados inválidos
- `404 Not Found` - Recurso não encontrado
- `500 Internal Server Error` - Erro interno do servidor

Exemplo de resposta de erro:
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Validation Error",
  "status": 400,
  "detail": "O título da tarefa é obrigatório",
  "instance": "/api/tasks"
}
```

## Banco de Dados

O projeto utiliza SQLite como banco de dados para simplicidade. O arquivo `tasks.db` é criado automaticamente na primeira execução.

### Estrutura da Tabela Tasks

| Coluna | Tipo | Descrição |
|--------|------|-----------|
| `Id` | INTEGER | Chave primária (autoincremento) |
| `Title` | TEXT | Título da tarefa (obrigatório, max 200 caracteres) |
| `Description` | TEXT | Descrição opcional (max 1000 caracteres) |
| `IsCompleted` | BOOLEAN | Status de conclusão (padrão: false) |
| `CreatedAt` | DATETIME | Data de criação (UTC) |
| `CompletedAt` | DATETIME | Data de conclusão (opcional) |

## Roadmap

- [ ] Implementar autenticação JWT
- [ ] Adicionar categorias para tarefas
- [ ] Implementar paginação
- [ ] Adicionar testes unitários
- [ ] Implementar cache com Redis
- [ ] Adicionar logging estruturado
- [ ] Migrar para PostgreSQL/SQL Server
- [ ] Implementar notificações por email
- [ ] Adicionar métricas e monitoramento

## Contribuindo

1. Faça um fork do projeto
2. Crie uma branch para sua funcionalidade (`git checkout -b feature/nova-funcionalidade`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`)
4. Push para a branch (`git push origin feature/nova-funcionalidade`)
5. Abra um Pull Request

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

## Autor

- **Fabricio Rosa** - [Meu GitHub](https://github.com/fabriciorosanet)

## Suporte

Se você tiver alguma dúvida ou problema:

1. Verifique a documentação acima
2. Consulte os exemplos de uso
3. Abra uma issue no GitHub
4. Entre em contato através do email: fabriciorosanet@gmail.com

---

⭐ Se este projeto foi útil para você, considere dar um STAR no repositório!