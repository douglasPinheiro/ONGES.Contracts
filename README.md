# ONGES.Contracts

Pacote de contratos compartilhados entre os microsservicos do ecossistema ONGES, usado para padronizar mensagens trocadas por eventos.

## Objetivo

Centralizar DTOs de integracao para reduzir acoplamento entre servicos e evitar divergencias de schema em mensagens publicadas/consumidas.

## Conteudo Atual

### `ONGES.Contracts.DTOs.DonationMessage`

Representa o evento de atualizacao de arrecadacao de campanha:

- `CampaignId` (`Guid`): identificador da campanha.
- `Amount` (`decimal`): valor da doacao processada.
- `DonatedAt` (`DateTime`): data/hora de processamento da doacao.

## Framework Alvo

- `.NET 10` (`net10.0`)

## Como Usar

### 1. Referencia de Projeto (desenvolvimento local)

```xml
<ItemGroup>
  <ProjectReference Include="..\..\ONGES.Contracts\ONGES.Contracts.csproj" />
</ItemGroup>
```

### 2. Referencia via Pacote NuGet

```xml
<ItemGroup>
  <PackageReference Include="ONGES.Contracts" Version="1.0.0" />
</ItemGroup>
```

### 3. Exemplo de Publicacao da Mensagem

```csharp
using ONGES.Contracts.DTOs;

await campaignUpdatePublisher.PublishAsync(
    new DonationMessage
    {
        CampaignId = message.CampaignId,
        Amount = message.Amount,
        DonatedAt = DateTime.UtcNow
    },
    cancellationToken);
```

### 4. Exemplo de Consumo da Mensagem

```csharp
using MassTransit;
using ONGES.Contracts.DTOs;

public sealed class DonationConsumer : IConsumer<DonationMessage>
{
    public Task Consume(ConsumeContext<DonationMessage> context)
    {
        var donation = context.Message;
        // Aplicar regra de negocio com CampaignId, Amount e DonatedAt
        return Task.CompletedTask;
    }
}
```

## Versionamento

Recomendacao para evolucao de contratos:

- Alteracoes **breaking changes**: incrementar major.
- Novos campos opcionais/nao quebrantes: incrementar minor.
- Correcoes internas/documentacao: incrementar patch.

## Empacotamento

Gerar pacote local:

```bash
dotnet pack .\ONGES.Contracts\ONGES.Contracts.csproj -c Release
```

Saida esperada:

- `.\ONGES.Contracts\bin\Release\ONGES.Contracts.<versao>.nupkg`

## Observacoes

- Evite incluir regras de negocio neste projeto; mantenha apenas contratos.
- Sempre alinhe mudancas de DTO com todos os produtores e consumidores antes de publicar nova versao.
