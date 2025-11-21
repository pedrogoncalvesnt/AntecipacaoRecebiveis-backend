namespace AntecipacaoRecebiveis.Domain.Requests;

public sealed record CriarNotaFiscalRequest(
    Guid EmpresaId,
    string? Numero,
    decimal Valor,
    DateTime DataVencimento
);