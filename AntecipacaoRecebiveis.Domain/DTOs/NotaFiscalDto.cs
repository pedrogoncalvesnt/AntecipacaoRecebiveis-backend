namespace AntecipacaoRecebiveis.Domain.DTOs;
public class NotaFiscalDto
{
    public NotaFiscalDto(Guid id, Guid empresaId, string? numero, decimal valor, DateTime dataVencimento, Guid? carrinhoId)
    {
        Id = id;
        EmpresaId = empresaId;
        Numero = numero;
        Valor = valor;
        DataVencimento = dataVencimento;
        CarrinhoId = carrinhoId;
    }

    public Guid Id { get; set; }
    public Guid EmpresaId { get; set; }
    public string? Numero { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public Guid? CarrinhoId { get; set; }
}