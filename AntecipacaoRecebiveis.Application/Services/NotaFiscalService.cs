using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Interfaces.Data;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
using AntecipacaoRecebiveis.Domain.Interfaces.Services;

namespace AntecipacaoRecebiveis.Application.Services;

public class NotaFiscalService : INotaFiscalService
{
    private readonly INotaFiscalRepository _NFRepository;
    private readonly IUnitOfWork _unitOfWork;
    public NotaFiscalService(INotaFiscalRepository nfRepository, IUnitOfWork unitOfWork)
    {
        _NFRepository = nfRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<NotaFiscalDto> CriarNotaFiscal(CriarNotaFiscalDto dto)
    {
        var nota = new NotaFiscal(
            Guid.NewGuid(),
            dto.EmpresaId, // incompleto na relação de nota -> empresa e inserção de nota
            dto.Numero,
            dto.Valor,
            dto.DataVencimento
        );

        await _NFRepository.CadastrarNFAsync(nota);
        await _unitOfWork.SaveChangesAsync();

        return new NotaFiscalDto
        {
            Id = nota.Id,
            EmpresaId = nota.EmpresaId,
            Numero = nota.Numero,
            Valor = nota.Valor,
            DataVencimento = nota.DataVencimento,
            CarrinhoId = nota.CarrinhoId ?? null
        };
    }

    public async Task<NotaFiscalDto?> ObterNFPorId(Guid id)
    {
        var nota = await _NFRepository.ObterNFPorIdAsync(id);

        if (nota == null) return null;

        return new NotaFiscalDto
        {
            Id = nota.Id,
            EmpresaId = nota.EmpresaId,
            Numero = nota.Numero,
            Valor = nota.Valor,
            DataVencimento = nota.DataVencimento,
            CarrinhoId = nota.CarrinhoId ?? null
        };
    }
}
