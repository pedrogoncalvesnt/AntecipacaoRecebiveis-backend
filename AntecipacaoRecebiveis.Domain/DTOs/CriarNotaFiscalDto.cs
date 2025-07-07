namespace AntecipacaoRecebiveis.Domain.DTOs;

public class CriarNotaFiscalDto
{
    public Guid EmpresaId { get; set; }
    public string? Numero { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
}