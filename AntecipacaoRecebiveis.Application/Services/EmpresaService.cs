using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Interfaces.Data;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
using AntecipacaoRecebiveis.Domain.Interfaces.Services;

namespace AntecipacaoRecebiveis.Application.Services;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IUnitOfWork _unitOfWork;
    public EmpresaService(IEmpresaRepository empresaRepository, IUnitOfWork unitOfWork)
    {
        _empresaRepository = empresaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<EmpresaDto> CriarEmpresa(EmpresaDto dto)
    {

        if (!Enum.IsDefined(typeof(RamoAtividade), dto.Ramo))
            throw new ArgumentException("Ramo inválido");

        var empresaDto = new EmpresaDto(
            Guid.NewGuid(),
            dto.Cnpj,
            dto.Nome,
            dto.FaturamentoMensal,
            dto.Ramo,
            dto.Limite
        );

        await _empresaRepository.CadastrarEmpresaAsync(empresaDto);
        await _unitOfWork.SaveChangesAsync();

        return empresaDto;
    }

    public async Task<Empresa?> ObterEmpresaPorId(Guid id)
    {
        var empresa = await _empresaRepository.ObterEmpresaPorIdAsync(id);

        if (empresa == null) return null;

        return empresa;
    }
}
