using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Enums;
using AntecipacaoRecebiveis.Domain.Requests;

namespace AntecipacaoRecebiveis.Domain.Entities;

public class Empresa : EntityBase
{
    public Empresa(string cnpj, string nome, decimal faturamentoMensal, RamoAtividade ramo)
    {
        Cnpj = cnpj; // adicionar validação de número de campos
        Nome = nome;
        FaturamentoMensal = faturamentoMensal;
        Ramo = ramo;
    }

    public static Empresa FromRequest(CriarEmpresaRequest request)
    {
        return new Empresa(
            request.Cnpj,
            request.Nome,
            request.FaturamentoMensal,
            request.Ramo
        );
    }

    public EmpresaDto MapToDto()
    {
        return new EmpresaDto(
            Id,
            Cnpj,
            Nome,
            FaturamentoMensal,
            Ramo,
            Limite
        );
    }

    public string Cnpj { get; set; }
    public string Nome { get; set; }
    public decimal FaturamentoMensal { get; set; }
    public RamoAtividade Ramo { get; set; }
    public decimal Limite => GetLimite();
    public List<NotaFiscal>? NotasFiscais { get; set; }

    private decimal GetLimite()
    {
        var limite = 0m;
        if (FaturamentoMensal >= 10000 && FaturamentoMensal <= 50000) limite = FaturamentoMensal / 2;

        if (FaturamentoMensal > 50000 && FaturamentoMensal <= 100000)
        {
            if (Ramo == RamoAtividade.Servicos) limite = (FaturamentoMensal * 55) / 100;
            if (Ramo == RamoAtividade.Produtos) limite = (FaturamentoMensal * 60) / 100;
        }

        if (FaturamentoMensal > 100000)
        {
            if (Ramo == RamoAtividade.Servicos) limite = (FaturamentoMensal * 60) / 100;
            if (Ramo == RamoAtividade.Produtos) limite = (FaturamentoMensal * 65) / 100;
        }

        return limite;
    }
}
