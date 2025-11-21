using AntecipacaoRecebiveis.Domain.Requests;

namespace AntecipacaoRecebiveis.Domain.Entities;

public class NotaFiscal : EntityBase
{
    public NotaFiscal(Guid empresaId, string? numero, decimal valor, DateTime dataVencimento, Guid? carrinhoId)
    {
        EmpresaId = empresaId;
        Numero = numero;
        Valor = valor;
        DataVencimento = dataVencimento;
        CarrinhoId = carrinhoId;
    }

    public static NotaFiscal FromRequest(CriarNotaFiscalRequest dto)
    {
        return new NotaFiscal(
            dto.EmpresaId,
            dto.Numero,
            dto.Valor,
            dto.DataVencimento,
            null 
        );
    }

    public Guid EmpresaId { get; private set; }
    public string? Numero { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataVencimento { get; private set; }
    public virtual Empresa? Empresa { get; private set; }
    public Guid? CarrinhoId { get; set; }
    public bool Antecipada { get; private set; } = false;
    public void MarcarComoAntecipada() => Antecipada = true;
    public bool EstaValida() => DataVencimento > DateTime.UtcNow;
}
