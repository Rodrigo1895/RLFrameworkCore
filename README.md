## O que é?
É um projeto de um framework desenvolvido em .NET, que implementa tecnologias e padrões de projetos muito utilizados.

## Como executar:
- Ter instalado o Visual Studio 2022 e .NET Core SDK 7.0
- Ter o Docker instalado
- Subir o docker-compose para utilização do Sql Server e do RabbitMQ
- Buildar o projeto **RLFrameworkCore** para geração das DLLs do framework que são dependências nos projetos **ExemploAuth** e **Exemplo**
  
- **ExemploAuth**:
  - Responsável por gerar o token de autenticação. Utilizar usuário 'Rodrigo' e senha '123' para autenticação.
- **Exemplo**:
  - Projeto de exemplo de utilização do Framework
  - Recuperar um token pelo projeto **ExemploAuth** e utilizá-lo para autenticação nas requisições do projeto **Exemplo**.
  - Startups Projects:
    - Exemplo.Web - Web API
    - Exemplo.Web.RabbitMq.Consumer - Console Application que fica consumindo as filas do RabbitMQ
  
**Obs:** Os projetos **ExemploAuth** e **Exemplo** estão configurados para criarem automaticamente os bancos de dados. Também irão executar Seeds na sua primeira execução, para popular os bancos de dados com algumas informações iniciais. Sendo assim, não é necessário executar script de criação de tabelas de forma manual.

## Tecnologias Utilizadas:
- ASP.NET 7.0
  - ASP.NET WebApi Core com autenticação JWT Bearer
  - Injeção de dependência do ASP.NET Core
- Entity Framework Core 7.0
- AutoMapper
- FluentValidator
- Swagger UI com JWT
- RabbitMQ
- MediatR
- Sql Server

## Arquitetura e Padrões de Projeto:
- Clean Code
- SOLID
- Domain Driven Design(DDD)
- CQRS
- Notification
- Domain Validations
- DTO Validations
- Repository
- Unit of Work
- RefreshToken
- API Versioning
- Cancellation Token