namespace AntecipacaoRecebiveis.Application.DTOs;
public class NotaFiscalDto
{
    public Guid Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
}