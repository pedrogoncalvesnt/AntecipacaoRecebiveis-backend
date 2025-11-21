using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Interfaces.Data;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
using AntecipacaoRecebiveis.Domain.Interfaces.Services;
using AntecipacaoRecebiveis.Domain.Requests;

namespace AntecipacaoRecebiveis.Application.Services;

public class NotaFiscalService : INotaFiscalService
{
    private readonly INotaFiscalRepository _NFRepository;
    private readonly IEmpresaRepository _EmpresaRepository;
    private readonly IUnitOfWork _unitOfWork;
    public NotaFiscalService(INotaFiscalRepository nfRepository, IEmpresaRepository empresaRepository, IUnitOfWork unitOfWork)
    {
        _NFRepository = nfRepository;
        _EmpresaRepository = empresaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<NotaFiscalDto> CriarNotaFiscal(CriarNotaFiscalRequest request)
    {
        var nota = NotaFiscal.FromRequest(request); // CarrinhoId sempre null na criação
        var created  = await _NFRepository.CadastrarAsync(nota);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(created);
    }

    public async Task<NotaFiscalDto?> ObterNFPorId(Guid id)
    {
        var nota = await _NFRepository.ObterPorIdAsync(id);
        return nota == null ? null : MapToDto(nota);
    }

    public async Task<NotaFiscalDto?> AdicionarNotaExistenteAoCarrinhoAsync(Guid empresaId, Guid notaId)
    {
        var empresa = await _EmpresaRepository.ObterEmpresaPorIdAsync(empresaId);
        if (empresa == null) return null;

        var nota = await _NFRepository.ObterPorIdAsync(notaId);
        if (nota == null) return null;
        if (!nota.EstaValida()) return null;
        if (nota.Antecipada) return null;
        if (nota.CarrinhoId.HasValue) return null; // já em carrinho

        var totalAtual = await _NFRepository.SomarValorPorCarrinhoIdAsync(empresaId);
        if (totalAtual + nota.Valor > empresa.Limite) return null;

        nota.CarrinhoId = empresaId;
        await _NFRepository.AtualizarAsync(nota);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(nota);
    }

    public async Task<bool> RemoverDoCarrinhoAsync(Guid empresaId, Guid notaId)
    {
        var nota = await _NFRepository.ObterPorIdAsync(notaId);
        if (nota == null) return false;
        if (nota.CarrinhoId != empresaId) return false;
        nota.CarrinhoId = null; // desassociar do carrinho
        await _NFRepository.AtualizarAsync(nota);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<NotaFiscalDto>> ObterCarrinhoAsync(Guid empresaId)
    {
        var notas = await _NFRepository.ObterPorCarrinhoIdAsync(empresaId);
        return notas.Select(MapToDto);
    }

    public async Task<EfetivacaoAntecipacaoResponse> EfetivarAntecipacaoAsync(Guid empresaId)
    {
        var empresa = await _EmpresaRepository.ObterEmpresaPorIdAsync(empresaId);
        if (empresa is null)
            return new EfetivacaoAntecipacaoResponse("", 0m, Array.Empty<NotaFiscalAntecipadaItem>(), 0m, 0m);

        var notas = (await _NFRepository.ObterPorCarrinhoIdAsync(empresaId))
            .Where(n => !n.Antecipada)
            .ToList();

        if (notas.Count == 0)
        {
            return new EfetivacaoAntecipacaoResponse(
                empresa.Cnpj,
                empresa.Limite,
                Array.Empty<NotaFiscalAntecipadaItem>(),
                0m,
                0m
            );
        }

        var taxaMensal = 0.0465m; // 4,65% ao mês
        var hoje = DateTime.UtcNow.Date;

        var itens = new List<NotaFiscalAntecipadaItem>();
        decimal totalLiquido = 0m;
        decimal totalBruto = 0m;

        foreach (var nf in notas)
        {
            var prazoDias = (nf.DataVencimento.Date - hoje).TotalDays;
            if (prazoDias < 0) prazoDias = 0;

            var meses = (decimal)prazoDias / 30m;
            var fator = (decimal)Math.Pow((double)(1 + taxaMensal), (double)meses);

            var valorPresente = nf.Valor / fator;
            var valorLiquido = Math.Round(valorPresente, 2, MidpointRounding.AwayFromZero);
            var valorBruto = nf.Valor;

            itens.Add(new NotaFiscalAntecipadaItem(
                nf.Numero,
                valorBruto,
                valorLiquido
            ));

            totalLiquido += valorLiquido;
            totalBruto += valorBruto;

            nf.MarcarComoAntecipada();
            await _NFRepository.AtualizarAsync(nf);
        }

        await _unitOfWork.SaveChangesAsync();

        return new EfetivacaoAntecipacaoResponse(
            empresa.Cnpj,
            empresa.Limite,
            itens,
            decimal.Round(totalLiquido, 2),
            decimal.Round(totalBruto, 2)
        );
    }

    private static NotaFiscalDto MapToDto(NotaFiscal nota)
        => new NotaFiscalDto(nota.Id, nota.EmpresaId, nota.Numero, nota.Valor, nota.DataVencimento, nota.CarrinhoId);
}
