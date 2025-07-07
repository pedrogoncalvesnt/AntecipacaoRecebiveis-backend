namespace AntecipacaoRecebiveis.Domain.DTOs;
public class NotaFiscalDto
{
    public Guid Id { get; set; }
    public Guid EmpresaId { get; set; }
    public string? Numero { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public Guid? CarrinhoId { get; set; }
}