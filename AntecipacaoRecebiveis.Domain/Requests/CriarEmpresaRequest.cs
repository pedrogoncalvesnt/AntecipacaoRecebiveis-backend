using AntecipacaoRecebiveis.Domain.Enums;
namespace AntecipacaoRecebiveis.Domain.Requests;

public sealed record CriarEmpresaRequest(
    string Cnpj,
    string Nome,
    decimal FaturamentoMensal,
    RamoAtividade Ramo
);
