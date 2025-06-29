namespace AntecipacaoRecebiveis.Domain.Entities;

public class NotaFiscal
{
    public Guid Id { get; set; }
    public Guid EmpresaId { get; set; }
    public string? NumeroNotaFiscal { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public virtual Empresa Empresa { get; set; } = null!;

    // Validação para cadastro de Nota Fiscal
    public bool EstaValida() => DataVencimento > DateTime.UtcNow;
}
