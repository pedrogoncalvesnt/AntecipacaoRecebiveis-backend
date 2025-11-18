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

    public async Task<NotaFiscalDto> CriarNotaFiscal(NotaFiscalDto dto)
    {
        var notaFiscalDto = new NotaFiscalDto(
            Guid.NewGuid(),
            dto.EmpresaId, // incompleto na relação de nota -> empresa e inserção de nota
            dto.Numero,
            dto.Valor,
            dto.DataVencimento,
            dto.CarrinhoId
        );

        await _NFRepository.CadastrarNFAsync(notaFiscalDto);
        await _unitOfWork.SaveChangesAsync();

        return notaFiscalDto;
    }

    public async Task<NotaFiscal?> ObterNFPorId(Guid id)
    {
        var notaFiscal = await _NFRepository.ObterNFPorIdAsync(id);

        if (notaFiscal == null) return null;

        return notaFiscal;
    }
}
