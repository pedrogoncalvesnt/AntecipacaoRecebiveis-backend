namespace AntecipacaoRecebiveis.Domain.Entities;
public class CarrinhoAntecipacao
{
    public CarrinhoAntecipacao(Guid id, NotaFiscal[]? notasFiscais)
    {
        Id = id;
        NotasFiscais = notasFiscais;
    }

    public Guid Id { get; set; }
    public NotaFiscal[]? NotasFiscais { get; private set; }

    public bool PodeAdicionarAoCarrinho(NotaFiscal nota)
    {

        return true;
    }
}
