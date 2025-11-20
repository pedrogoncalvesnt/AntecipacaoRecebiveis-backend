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
        var nota = NotaFiscal.FromRequest(request);
        var created  = await _NFRepository.CadastrarAsync(nota);

        await _NFRepository.CadastrarAsync(nota);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(created);
    }

    public async Task<NotaFiscalDto?> ObterNFPorId(Guid id)
    {
        var nota = await _NFRepository.ObterPorIdAsync(id);

        if (nota == null) return null;

        return MapToDto(nota);
    }

    public async Task<NotaFiscalDto?> AdicionarAoCarrinhoAsync(Guid empresaId, CriarNotaFiscalRequest request)
    {
        var empresa = await _EmpresaRepository.ObterEmpresaPorIdAsync(empresaId);
        if (empresa == null) return null;

        var nota = NotaFiscal.FromRequest(request);

        if (!nota.EstaValida()) return null;
        if (nota.CarrinhoId.HasValue) return null; // já está em outro carrinho

        var totalAtual = await _NFRepository.SomarValorPorCarrinhoIdAsync(empresaId);
        var limite = empresa.Limite; // ou empresa.GetLimite() conforme modelo

        if (totalAtual + nota.Valor > limite) return null;

        // 5) associar e persistir (se nota já existe no banco, atualiza; caso contrário, cadastra)
        var existente = await _NFRepository.ObterPorIdAsync(nota.Id);
        nota.CarrinhoId = empresaId;

        if (existente == null)
        {
            var created = await _NFRepository.CadastrarAsync(nota);
            await _unitOfWork.SaveChangesAsync();
            return MapToDto(created);
        }
        else
        {
            await _NFRepository.AtualizarAsync(nota);
        }

        await _unitOfWork.SaveChangesAsync();

        return MapToDto(nota);
    }

    public async Task<bool> RemoverDoCarrinhoAsync(Guid empresaId, Guid notaId)
    {
        var nota = await _NFRepository.ObterPorIdAsync(notaId);
        if (nota == null) return false;
        if (nota.CarrinhoId != empresaId) return false;

        nota.CarrinhoId = null;
        await _NFRepository.AtualizarAsync(nota);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<NotaFiscalDto>> ObterCarrinhoAsync(Guid empresaId)
    {
        var notas = await _NFRepository.ObterPorCarrinhoIdAsync(empresaId);
        return notas.Select(MapToDto);
    }

    private static NotaFiscalDto MapToDto(NotaFiscal nota)
        => new NotaFiscalDto(nota.Id, nota.EmpresaId, nota.Numero, nota.Valor, nota.DataVencimento, nota.CarrinhoId);
}
