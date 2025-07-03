namespace AntecipacaoRecebiveis.Domain.Entities;
public class Antecipacao
{
    public Antecipacao(Guid id, NotaFiscal[]? notasFiscais)
    {
        Id = id;
        NotasFiscais = notasFiscais;
    }

    public Guid Id { get; private set; }
    public NotaFiscal[]? NotasFiscais { get; private set; }
    public Empresa? Empresa { get; private set; }
    public DateTime Competencia { get; private set; } = DateTime.UtcNow;
    public decimal Total => NotasFiscais?.Sum(x => x.Valor) ?? 0;
    public bool AdicionaAoCarrinho(NotaFiscal nota) // considerar result patterns para retorno de sucesso ou falha
    {
        if(Total + nota.Valor > Empresa?.Limite) // tentando sacar acima do saldo
        {
            return false;
        }

        if(nota.CarrinhoId.HasValue) // nota já está em outro carrinho
        {
            return false;
        }

        if(!nota.EstaValida()) // nota não é válida, bom considerar, pois nem sempre é adicionada no carrinho assim que criada.
        {
            return false;
        }

        if(NotasFiscais == null) // caso não exista notas fiscais no carrinho, adiciona a primeira nota
        {
            NotasFiscais = new NotaFiscal[] { nota };
        }
        else
        {
            NotasFiscais = NotasFiscais.Append(nota).ToArray();
        }

        return true;
    }

    public bool RemoveDoCarrinho(NotaFiscal nota)
    {
        if (NotasFiscais == null || !NotasFiscais.Contains(nota))
        {
            return false; // Nota não está no carrinho
        }
        NotasFiscais = NotasFiscais.Where(x => x.Id != nota.Id).ToArray();
        return true; // Nota removida com sucesso
    }
}

// Melhorias: considerar período de tempo por mês,
// para considerar também limite de antecipação mensal baseado no faturamento mensal da empresa.