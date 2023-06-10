<div align="center">
  <img alt="GitHub language count" src="https://img.shields.io/github/languages/count/Rodrigo1895/RLFrameworkCore">
  
  <a href="https://www.linkedin.com/in/rodrigo-lima-9607137b/">
    <img alt="Made by Rodrigo1895" src="https://img.shields.io/badge/made%20by-Rodrigo1895-%2304D361">
  </a>

  <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/Rodrigo1895/RLFrameworkCore">
</div>

## :question: O que é?
É um projeto de um framework, onde implementa as tecnologias mais utilizadas e os principais padrões de projetos.

## :white_check_mark: Como executar:
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

## :rocket: Tecnologias Utilizadas:
- [ASP.NET 7.0][asp_net_link]
  - [ASP.NET WebApi Core com autenticação JWT Bearer][asp_net_web_api_link]
  - [Injeção de dependência do ASP.NET Core][asp_net_di_link]
- [Entity Framework Core 7.0][ef_core_link]
- [AutoMapper][automapper_link]
- [FluentValidator][fluent_validator_link]
- [Swagger UI com JWT][swagger_link]
- [RabbitMQ][rabbitmq_link]
- [MediatR][mediatr_link]
- [Sql Server Express][sql_server_link]
- [Docker][docker_link]

## :gear: Arquitetura e Padrões de Projeto:
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

<!-- Links -->
[asp_net_link]: https://learn.microsoft.com/pt-br/aspnet/core/release-notes/aspnetcore-7.0?view=aspnetcore-7.0
[asp_net_web_api_link]: https://learn.microsoft.com/pt-br/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio
[asp_net_di_link]: https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-7.0
[ef_core_link]: https://learn.microsoft.com/pt-br/ef/core/
[automapper_link]: https://automapper.org/
[fluent_validator_link]: https://fluentvalidation.net/
[swagger_link]: https://swagger.io/tools/swagger-ui/
[rabbitmq_link]: https://www.rabbitmq.com/
[mediatr_link]: https://github.com/jbogard/MediatR
[sql_server_link]: https://learn.microsoft.com/en-us/sql/sql-server/
[docker_link]: https://www.docker.com/