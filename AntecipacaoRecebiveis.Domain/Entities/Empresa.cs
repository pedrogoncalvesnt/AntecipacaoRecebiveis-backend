namespace AntecipacaoRecebiveis.Domain.Entities;

public class Empresa
{
    public Empresa(Guid id, string? cnpj, string? nome, decimal faturamentoMensal, RamoAtividade ramo)
    {
        Id = id;
        Cnpj = cnpj; // adicionar validação de número de campos
        Nome = nome;
        FaturamentoMensal = faturamentoMensal;
        Ramo = ramo;
    }

    public Guid Id { get; set; }
    public string? Cnpj { get; set; }
    public string? Nome { get; set; }
    public decimal FaturamentoMensal { get; set; }
    public RamoAtividade Ramo { get; set; }
    public decimal Limite => GetLimite();

    public decimal GetLimite()
    {
        var limite = 0m;
        if (FaturamentoMensal >= 10000 && FaturamentoMensal <= 50000) limite = FaturamentoMensal / 2;

        if (FaturamentoMensal >= 50001 && FaturamentoMensal <= 100000)
        {
            if (Ramo == RamoAtividade.Servicos) limite = FaturamentoMensal * 55 / 100;
            if (Ramo == RamoAtividade.Produtos) limite = FaturamentoMensal * 60 / 100;
        }

        if (FaturamentoMensal >= 100001)
        {
            if (Ramo == RamoAtividade.Servicos) limite = FaturamentoMensal * 60 / 100;
            if (Ramo == RamoAtividade.Produtos) limite = FaturamentoMensal * 65 / 100;
        }

        return limite;
    }
}
