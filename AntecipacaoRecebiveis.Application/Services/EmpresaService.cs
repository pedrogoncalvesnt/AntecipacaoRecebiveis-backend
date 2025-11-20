using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Enums;
using AntecipacaoRecebiveis.Domain.Interfaces.Data;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
using AntecipacaoRecebiveis.Domain.Interfaces.Services;
using AntecipacaoRecebiveis.Domain.Requests;

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

    public async Task<EmpresaDto> CriarEmpresa(CriarEmpresaRequest request)
    {

        if (!Enum.IsDefined(typeof(RamoAtividade), request.Ramo))
            throw new ArgumentException("Ramo inválido");

        var empresa = await _empresaRepository.CadastrarEmpresaAsync(request);
        await _unitOfWork.SaveChangesAsync();

        return empresa.MapToDto();
    }

    public async Task<Empresa?> ObterEmpresaPorId(Guid id)
    {
        var empresa = await _empresaRepository.ObterEmpresaPorIdAsync(id);

        if (empresa == null) return null;

        return empresa;
    }
}
