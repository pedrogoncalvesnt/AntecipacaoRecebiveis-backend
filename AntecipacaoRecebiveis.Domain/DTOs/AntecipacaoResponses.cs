using System.Text.Json.Serialization;

namespace AntecipacaoRecebiveis.Domain.DTOs;

public sealed record NotaFiscalAntecipadaItem(
    [property: JsonPropertyName("numero")] string? Numero,
    [property: JsonPropertyName("valor_bruto")] decimal ValorBruto,
    [property: JsonPropertyName("valor_liquido")] decimal ValorLiquido
);

public sealed record EfetivacaoAntecipacaoResponse(
    [property: JsonPropertyName("cnpj")] string Cnpj,
    [property: JsonPropertyName("limite")] decimal Limite,
    [property: JsonPropertyName("notas_fiscais")] IReadOnlyList<NotaFiscalAntecipadaItem> NotasFiscais,
    [property: JsonPropertyName("total_liquido")] decimal TotalLiquido,
    [property: JsonPropertyName("total_bruto")] decimal TotalBruto
);