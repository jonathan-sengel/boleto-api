# BoletoAPI

## Sobre o projeto

Este projeto foi desenvolvido como parte de um teste técnico para a
empresa **Questor**.

A aplicação consiste em uma API REST para gerenciamento de boletos
utilizando:

-   .NET 6
-   ASP.NET Core Web API
-   Entity Framework Core
-   PostgreSQL
-   Swagger/OpenAPI

O objetivo é demonstrar organização de código, criação de endpoints
REST, persistência de dados, validações e boas práticas no
desenvolvimento de APIs.

------------------------------------------------------------------------

##  Pré-requisitos
 
Antes de executar o projeto, certifique-se de ter os itens abaixo instalados em sua máquina:
 
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [EF Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (caso não tenha, veja a seção de instalação abaixo)

------------------------------------------------------------------------

##  Configuração do Banco de Dados
 
Por padrão, a aplicação utiliza as seguintes configurações de conexão com o PostgreSQL:
 
| Parâmetro | Valor padrão |
|-----------|--------------|
| Database  | `boletoapi`  |
| Usuário   | `postgres`   |
| Senha     | `masterkey`  |
| Host      | `localhost`  |
| Porta     | `5432`       |

Caso necessário, altere o arquivo:

    appsettings.json

Exemplo:

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=boletoapi;Username=postgres;Password=masterkey"
  }
}
```

------------------------------------------------------------------------

## Criando o banco

Execute no PostgreSQL:

``` sql
CREATE DATABASE boletoapi;
```

------------------------------------------------------------------------
## Clone o repositório
 
```bash
git clone https://github.com/jonathan-sengel/boleto-api.git
cd boleto-api
```

## Entity Framework

Caso não possua o EF instalado:

``` bash
dotnet tool install --global dotnet-ef
```

Verifique:

``` bash
dotnet ef --version
```

------------------------------------------------------------------------

## Executando migrations

Na pasta do projeto:

``` bash
dotnet ef database update
```

Esse comando cria e atualiza as tabelas conforme as migrations
existentes.

------------------------------------------------------------------------

## Executando aplicação

Restaurar dependências:

``` bash
dotnet restore
```

Compilar:

``` bash
dotnet build
```

Executar:

``` bash
dotnet run
```

------------------------------------------------------------------------

## Swagger

Com a aplicação em execução, acesse a documentação interativa da API:
 
🔗 [https://localhost:7082/swagger/index.html](https://localhost:7082/swagger/index.html)

------------------------------------------------------------------------

## Estrutura do projeto

    BoletoAPI

    Controllers
    - BancoController.cs
    - BoletoController.cs

    Data
    - ApplicationDbContext.cs

    DTOs
    - ApiErroDto.cs
    - CriarBancoDto.cs
    - CriarBoletoDto.cs
    - ResponseDto.cs

    Exceptions
    - NotFoundException.cs
    - RegraDeNegocioException.cs

    Middlewares
    - ExceptionMiddleware.cs

    Models
    - Banco.cs
    - Boleto.cs

    Services
    - BancoService.cs
    - BoletoService.cs

------------------------------------------------------------------------

**Controllers** recebem as requisições HTTP, validam a entrada e delegam o processamento para os **Services**.

**Services** concentram as regras de negócio e se comunicam com o banco de dados por meio do **ApplicationDbContext** (EF Core com PostgreSQL).

**Models** representam as entidades persistidas no banco.

**DTOs** isolam os contratos da API das entidades internas, evitando expor o domínio diretamente.

**Exceptions** customizadas (`NotFoundException`, `RegraDeNegocioException`) permitem distinguir diferentes tipos de erros de forma semântica.

O **ExceptionMiddleware** captura essas exceções globalmente e retorna respostas padronizadas com o formato definido em `ApiErroDTo`, mantendo consistência nas respostas de erro da API.

------------------------------------------------------------------------

## Tecnologias Utilizadas
 
- [.NET 6](https://dotnet.microsoft.com/)
- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [PostgreSQL](https://www.postgresql.org/)
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)