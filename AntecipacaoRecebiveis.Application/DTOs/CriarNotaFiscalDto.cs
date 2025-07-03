namespace AntecipacaoRecebiveis.Application.DTOs;

public class CriarNotaFiscalDto
{
    public string? Numero { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
}