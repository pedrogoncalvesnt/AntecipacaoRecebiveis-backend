namespace AntecipacaoRecebiveis.Application.DTOs;

public class CriarNotaFiscalDto
{
    public string NumeroNotaFiscal { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
}