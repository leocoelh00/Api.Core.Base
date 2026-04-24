# Api.Core.Base

Template de API REST em **.NET 10** seguindo os princípios de **Clean Architecture**, com autenticação Basic, Swagger configurado e container de DI organizado por camada. Ideal para servir de ponto de partida em projetos pessoais ou estudos.

---

## Tecnologias

- [.NET 10](https://dotnet.microsoft.com/)
- ASP.NET Core Web API
- [Swashbuckle.AspNetCore 10](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) — Swagger / OpenAPI
- Microsoft.OpenApi 2.x

---

## Estrutura do projeto

```
Api.Core.Base.sln
└── src/
    ├── Api.Core.Base.Domain/          # Entidades, interfaces e objetos de valor
    ├── Api.Core.Base.Application/     # Casos de uso, DTOs e padrão Result
    ├── Api.Core.Base.Infrastructure/  # Repositórios, serviços externos e DbContext
    └── Api.Core.Base.API/             # Controllers, middlewares, Swagger e DI
```

### Fluxo de dependências

```
API → Application → Domain
API → Infrastructure → Application → Domain
```

A camada **Domain** não depende de ninguém. Cada camada interna só conhece a imediatamente anterior — nenhum projeto referencia diretamente o que está acima dele.

---

## Funcionalidades incluídas

### Basic Authentication

Autenticação via header `Authorization: Basic <base64(user:password)>` implementada com um `AuthenticationHandler` customizado.

As credenciais são lidas do `appsettings.json`:

```json
"BasicAuth": {
  "Username": "admin",
  "Password": "password"
}
```

> Para produção, use variáveis de ambiente ou um secret manager — nunca suba credenciais reais no repositório.

### Swagger com suporte a Basic Auth

Acesse `/swagger` após subir a aplicação. O botão **Authorize** permite informar usuário e senha diretamente na UI do Swagger.

Endpoints marcados com `[AllowAnonymous]` não exibem o requisito de segurança.

### DI organizado por camada

Cada camada expõe um extension method próprio para registro no container:

```csharp
builder.Services.AddApplication();       // Application layer
builder.Services.AddInfrastructure();    // Infrastructure layer
builder.Services.AddBasicAuthentication(); // Auth handler
builder.Services.AddSwaggerConfiguration(); // Swagger
```

### Padrão Result

A camada Application usa `Result<T>` para retornos explícitos de sucesso ou falha, sem lançar exceções para fluxos de negócio:

```csharp
return Result<MeuDto>.Success(dto);
return Result<MeuDto>.Failure("Recurso não encontrado.");
```

### BaseEntity

Entidade base no Domain com `Id` (Guid), `CreatedAt` e `UpdatedAt`:

```csharp
public class MinhaEntidade : BaseEntity
{
    // seus campos aqui
}
```

---

## Como usar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Rodando localmente

```bash
git clone https://github.com/seu-usuario/Api.Core.Base.git
cd Api.Core.Base

dotnet restore
dotnet run --project src/Api.Core.Base.API
```

Swagger disponível em: `https://localhost:{porta}/swagger`

### Compilar a solution completa

```bash
dotnet build
```

---

## Como adaptar para seu projeto

1. **Renomeie** os projetos e namespaces substituindo `Api.Core.Base` pelo nome do seu projeto.
2. **Domain** — adicione suas entidades herdando de `BaseEntity` e defina interfaces de repositório.
3. **Application** — implemente casos de uso usando o padrão `Result<T>`.
4. **Infrastructure** — implemente as interfaces do Domain (repositórios, integrações externas).
5. **API** — adicione seus controllers com `[Authorize]` onde necessário.
6. **Credenciais** — troque `BasicAuth:Username` e `BasicAuth:Password` por valores seguros via variável de ambiente ou secrets.

---

## Contribuindo

Contribuições são bem-vindas! Abra uma issue ou pull request com sugestões, melhorias ou correções.

---

## Licença

MIT
