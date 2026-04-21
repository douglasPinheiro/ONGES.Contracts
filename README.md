# ONGES.Contracts

Pacote de contratos compartilhados entre os microsserviços do ecossistema ONGES, usado para padronizar mensagens trocadas por eventos.

## Objetivo

Centralizar DTOs de integração para reduzir acoplamento entre serviços e evitar divergências de schema em mensagens publicadas/consumidas.

## Conteúdo Atual

### `ONGES.Contracts.DTOs.DonationMessage`

Representa o evento de atualização de arrecadação de campanha:

- `CampaignId` (`Guid`): identificador da campanha.
- `Amount` (`decimal`): valor da doação processada.
- `DonatedAt` (`DateTime`): data/hora de processamento da doação.

## Framework Alvo

- `.NET 10` (`net10.0`)

## Como Usar

### 1. Referência de Projeto (desenvolvimento local)

```xml
<ItemGroup>
  <ProjectReference Include="..\..\ONGES.Contracts\ONGES.Contracts.csproj" />
</ItemGroup>
```

### 2. Referência via Pacote NuGet

```xml
<ItemGroup>
  <PackageReference Include="ONGES.Contracts" Version="1.0.0" />
</ItemGroup>
```

### 3. Exemplo de Publicação da Mensagem

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
        // Aplicar regra de negócio com CampaignId, Amount e DonatedAt
        return Task.CompletedTask;
    }
}
```

## Versionamento

Recomendação para evolução de contratos:

- Alterações **breaking changes**: incrementar major.
- Novos campos opcionais/não quebrantes: incrementar minor.
- Correções internas/documentação: incrementar patch.

## Empacotamento

Gerar pacote local:

```bash
dotnet pack .\ONGES.Contracts\ONGES.Contracts.csproj -c Release
```

Saída esperada:

- `.\ONGES.Contracts\bin\Release\ONGES.Contracts.<versão>.nupkg`

## Observações

- Evite incluir regras de negócio neste projeto; mantenha apenas contratos.
- Sempre alinhe mudanças de DTO com todos os produtores e consumidores antes de publicar nova versão.
