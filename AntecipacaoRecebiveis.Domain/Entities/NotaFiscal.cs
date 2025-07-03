namespace AntecipacaoRecebiveis.Domain.Entities;

public class NotaFiscal
{
    public NotaFiscal(Guid id, Guid empresaId, string? numero, decimal valor, DateTime dataVencimento)
    {
        Id = id;
        EmpresaId = empresaId;
        Numero = numero;
        Valor = valor;
        DataVencimento = dataVencimento;
    }

    public Guid Id { get; private set; }
    public Guid EmpresaId { get; private set; }
    public string? Numero { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataVencimento { get; private set; }
    public virtual Empresa? Empresa { get; private set; }
    public Guid? CarrinhoId { get; set; }

    // Validação para cadastro de Nota Fiscal
    public bool EstaValida() => DataVencimento > DateTime.UtcNow;
}
