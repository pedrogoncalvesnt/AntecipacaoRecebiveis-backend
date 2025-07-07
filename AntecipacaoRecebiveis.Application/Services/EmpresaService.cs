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

    public async Task<EmpresaDto> CriarEmpresa(CriarEmpresaDto dto)
    {

        if (!Enum.IsDefined(typeof(RamoAtividade), dto.Ramo))
            throw new ArgumentException("Ramo inválido");

        var empresa = new Empresa(
            Guid.NewGuid(),
            dto.Cnpj,
            dto.Nome,
            dto.FaturamentoMensal,
            (RamoAtividade)dto.Ramo
        );

        await _empresaRepository.CadastrarEmpresaAsync(empresa);
        await _unitOfWork.SaveChangesAsync();

        return new EmpresaDto
        {
            Id = empresa.Id,
            Cnpj = empresa.Cnpj,
            Nome = empresa.Nome,
            FaturamentoMensal = empresa.FaturamentoMensal,
            Ramo = empresa.Ramo.ToString(),
            Limite = empresa.GetLimite()
        };
    }

    public async Task<EmpresaDto?> ObterEmpresaPorId(Guid id)
    {
        var empresa = await _empresaRepository.ObterEmpresaPorIdAsync(id);

        if (empresa == null) return null;

        return new EmpresaDto
        {
            Id = empresa.Id,
            Cnpj = empresa.Cnpj,
            Nome = empresa.Nome,
            FaturamentoMensal = empresa.FaturamentoMensal,
            Ramo = empresa.Ramo.ToString(),
            Limite = empresa.GetLimite()
        };
    }
}
