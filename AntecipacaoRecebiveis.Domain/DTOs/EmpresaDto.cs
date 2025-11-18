using AntecipacaoRecebiveis.Domain.Entities;

namespace AntecipacaoRecebiveis.Domain.DTOs;
public class EmpresaDto
{
    public EmpresaDto(Guid id, string cnpj, string nome, decimal faturamentoMensal, RamoAtividade ramo, decimal limite)
    {
        Id = id;
        Cnpj = cnpj;
        Nome = nome;
        FaturamentoMensal = faturamentoMensal;
        Ramo = ramo;
        Limite = limite;
    }

    public Guid Id { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public decimal FaturamentoMensal { get; set; }
    public RamoAtividade Ramo { get; set; }
    public decimal Limite { get; set; }
}
