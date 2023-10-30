# Sistema de Gerenciamento de Biblioteca (.NET 7)

Este repositório contém o código-fonte para um sistema de gerenciamento de biblioteca desenvolvido em .NET 7 como parte de um desafio técnico para uma vaga. O sistema permite o gerenciamento de empréstimos, livros e usuários através de uma API RESTful. Abaixo estão as principais rotas disponíveis:

### Deploy
Para fazer o deploy utilizei o banco de dados mySQL hospedado no googleCloud, e a para a API utilizei o Azure por facilitar o deploy com o .Net 
#Swagger
Explore a API usando o Swagger: [https://bibliotecaappmvp.azurewebsites.net/swagger/index.html](https://bibliotecaappmvp.azurewebsites.net/swagger/index.html)

### Emprestimo

- **POST** `/api/emprestar-livro`: Realiza um empréstimo de livro.
- **POST** `/api/devolver-livro`: Registra a devolução de um livro.
- **GET** `/api/emprestimos`: Obtém a lista de todos os empréstimos.

### Livro

- **POST** `/api/livro`: Adiciona um novo livro.
- **PUT** `/api/livros/{id}`: Atualiza as informações de um livro existente.
- **DELETE** `/api/livro/{id}`: Remove um livro pelo ID.
- **GET** `/api/livro/{id}`: Obtém detalhes de um livro específico pelo ID.
- **GET** `/api/livros`: Obtém a lista de todos os livros.

### Usuario

- **POST** `/api/usuarios`: Cria um novo usuário.
- **GET** `/api/usuarios`: Obtém a lista de todos os usuários.
- **PUT** `/api/usuarios/{id}`: Atualiza as informações de um usuário existente pelo ID.
- **GET** `/api/usuarios/{id}`: Obtém detalhes de um usuário específico pelo ID.
- **DELETE** `/api/usuario/{id}`: Remove um usuário pelo ID.

## Pré-requisitos

- .NET 7 SDK instalado: [Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

## Como Executar

1. **Clone o repositório:**
   ```
   git clone https://github.com/seu-usuario/sistema-gerenciamento-biblioteca.git
   cd sistema-gerenciamento-biblioteca
   ```

2. **Restaure as dependências e execute o projeto:**
   ```
   dotnet restore
   dotnet run
   ```

A API estará acessível em `http://localhost:7040` por padrão.
