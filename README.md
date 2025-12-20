## 📌 README.md 


# 🎬 MovieCatalog API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91)
![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?logo=docker&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql&logoColor=white)
![JWT](https://img.shields.io/badge/Auth-JWT-orange)
![License](https://img.shields.io/badge/License-MIT-green)

API RESTful para catálogo de filmes, desenvolvida em **ASP.NET Core 8**, com **MySQL**, **Docker**, **JWT Authentication**, **Roles (Admin/User)** e arquitetura baseada em **boas práticas de mercado**.

Projeto desenvolvido com foco em **aprendizado**, **organização**, **segurança** e **portfólio profissional**.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core
- MySQL
- Docker & Docker Compose
- JWT (JSON Web Token)
- FluentValidation
- AutoMapper
- Swagger / OpenAPI

---

## 📐 Arquitetura do Projeto

```
MovieCatalog
│
├── Controllers
│   ├── MoviesController
│   ├── GenresController
│   ├── UsersController
│   └── AuthController
│
├── DTOs
│   ├── Movie
│   ├── Genre
│   ├── User
│   └── Authentication
│
├── Entities
│   ├── Movie
│   ├── Genre
│   └── User
│
├── Repositories
│   ├── Interfaces
│   └── Implementations
│
├── Services
│   ├── AuthService
│   └── JwtTokenGenerator
│
├── Configurations
│   └── JwtSettings
│
├── Validators
│
├── Middlewares
│   └── ErrorHandlingMiddleware
│
├── Data
│   └── AppDbContext
│
└── Program.cs
````

---

## 🔐 Autenticação e Autorização

A API utiliza **JWT** para autenticação e **roles** para autorização.

### Roles disponíveis

* `Admin`
* `User`

### Proteção de endpoints

* Endpoints sensíveis usam:

```csharp
[Authorize]
```

* Operações administrativas usam:

```csharp
[Authorize(Roles = "Admin")]
```

---

## 🔑 JWT – Configuração Segura

As configurações do JWT **não são versionadas**.

### Variáveis de ambiente necessárias

```env
JwtSettings__Key=YOUR_SECRET_KEY
JwtSettings__Issuer=MovieCatalog
JwtSettings__Audience=MovieCatalogUsers
```

✔ A chave JWT nunca é commitada
✔ Utiliza `.env` e **User Secrets** localmente
✔ `.env.example` pode ser fornecido como referência

---

## 🐳 Docker

O projeto utiliza **Docker Compose** para subir a API e o banco MySQL.

### Subir a aplicação

```bash
docker compose up -d --build
```

### Derrubar containers e volumes

```bash
docker compose down --volumes
```

### Serviços

* **API:** [http://localhost:8080](http://localhost:8080)
* **MySQL:** porta `3307` (host)

---

## 📄 Swagger

A API possui documentação automática via **Swagger**.

📍 Acesse:

```
http://localhost:8080/swagger
```

### Autorização no Swagger

1. Faça login
2. Copie o `AccessToken`
3. Clique em **Authorize**
4. Use:

```
Bearer {seu_token}
```

---

## 👤 Funcionalidades Implementadas

### 🎥 Filmes

* Criar, listar, atualizar e remover
* Filtros por título, ano e gênero
* Paginação e ordenação

### 🏷️ Gêneros

* CRUD completo
* Validação com FluentValidation

### 👥 Usuários

* Registro
* Login
* Listagem protegida por role
* Soft delete (Admin)

### 🔐 Autenticação

* Login com username ou email
* JWT Access Token
* Estrutura preparada para Refresh Token

---

## ⚠️ Middleware Global de Erros

Middleware responsável por:

* Capturar exceções não tratadas
* Retornar respostas JSON padronizadas
* Evitar vazamento de stack trace

### Exemplo de resposta

```json
{
  "message": "Ocorreu um erro inesperado no servidor.",
  "status": 500
}
```

---

## 🧪 Validações

Utiliza **FluentValidation** para:

* DTOs de criação e atualização
* Mensagens claras de erro
* Separação de responsabilidades

---

## 📌 Boas Práticas Aplicadas

* DTOs para evitar vazamento de entidades
* Repository Pattern
* Separação de camadas
* JWT seguro
* Variáveis sensíveis fora do código
* Dockerização
* Código organizado e escalável

---

## 📚 Próximos Passos (Opcional)

* Persistência real de Refresh Token
* Policy-based Authorization
* Padronização de respostas (Envelope)
* Testes automatizados

> *Não implementados para manter a complexidade adequada ao escopo de portfólio.*

---

## 🧑‍💻 Autor

**Gabriel Gomes**


Estudante de Análise e Desenvolvimento de Sistemas.

Foco em **Back-end com .NET**.
