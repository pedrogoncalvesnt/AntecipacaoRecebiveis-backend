# AntecipacaoRecebiveis Backend

## Requisitos
- .NET 8 SDK
- Docker e Docker Compose
- SQL Server (provisionado via docker-compose)

## Estrutura
- AntecipacaoRecebiveis.API: API principal (endpoints de empresas, notas, carrinho e antecipação)
- AntecipacaoRecebiveis.Domain: Entidades, DTOs, interfaces
- AntecipacaoRecebiveis.Application: Serviços de aplicação
- AntecipacaoRecebiveis.Infrastructure: Persistência (EF Core / DbContext / Repositórios)

## Configuração de Connection String
Defina a variável de ambiente para a senha do usuário SA (ex.: `SA_PASSWORD`) e uma connection string via User Secrets ou variável de ambiente (não versionar senhas). Exemplo de formato (não usar a senha literal aqui):
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=AntecipacaoRecebiveisDb;User Id=sa;Password=<SENHA>;TrustServerCertificate=True;"
}
```
Ou defina em tempo de execução: `DEFAULT_CONNECTION="Server=localhost,1433;Database=AntecipacaoRecebiveisDb;User Id=sa;Password=<SENHA>;TrustServerCertificate=True;"`

## Subindo com Docker Compose
```
# Windows PowerShell
$env:SA_PASSWORD="<SENHA>"

# Linux/macOS
export SA_PASSWORD="<SENHA>"

docker-compose up -d
```
Serviços:
- API: http://localhost:8080
- Swagger (Development): http://localhost:8080/swagger
- SQL Server: localhost:1433

### Logs
```
docker logs -f antecipacaorecebiveis.api
```

### Parar containers
```
docker-compose down
```
(para remover volume do SQL Server também: `docker-compose down -v`)

## Execução Local sem Docker
1. Inicie um SQL Server local.
2. Configure a connection string via User Secrets ou variável de ambiente (evite deixar senha em `appsettings.*`).
3. Rode:
```
dotnet restore
cd AntecipacaoRecebiveis.API
dotnet run
```

## Principais Endpoints
Empresas:
- POST /api/empresas
- GET /api/empresas/{id}
- POST /api/empresas/{empresaId}/carrinho/notas/{notaId}
- DELETE /api/empresas/{empresaId}/carrinho/notas/{notaId}
- GET /api/empresas/{empresaId}/carrinho
- POST /api/empresas/efetivar-antecipacao?empresaId={id}

Notas Fiscais:
- POST /api/notasfiscais
- GET /api/notasfiscais/{id}

## Antecipação
POST /api/empresas/efetivar-antecipacao?empresaId={id} retorna JSON:
```
{
  "cnpj": "...",
  "limite": 50000,
  "notas_fiscais": [
    { "numero": "123", "valor_bruto": 10000, "valor_liquido": 9534.88 }
  ],
  "total_liquido": 9534.88,
  "total_bruto": 10000
}
```
Notas já antecipadas não são recalculadas.

## Teste Manual
1. Criar empresa.
2. Criar nota fiscal (POST /api/notasfiscais).
3. Adicionar nota existente ao carrinho.
4. Efetivar antecipação.

## Limpeza
```
docker-compose down -v
```
Remove containers e volume de dados.