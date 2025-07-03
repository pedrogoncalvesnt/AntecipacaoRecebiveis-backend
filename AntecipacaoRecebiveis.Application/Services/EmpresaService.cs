using AntecipacaoRecebiveis.Application.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Interfaces.Data;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;

namespace AntecipacaoRecebiveis.Application.Services;
public class EmpresaService
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
        var empresa = new Empresa(
            Guid.NewGuid(), // Provide the required 'id' parameter
            dto.Cnpj,
            dto.Nome,
            dto.FaturamentoMensal,
            Enum.Parse<RamoAtividade>(dto.Ramo, ignoreCase: true) // Convert string to enum
        );

        await _empresaRepository.CadastrarEmpresaAsync(empresa);
        await _unitOfWork.SaveChangesAsync();

        return new EmpresaDto
        {
            Id = empresa.Id,
            Cnpj = empresa.Cnpj,
            Nome = empresa.Nome,
            FaturamentoMensal = empresa.FaturamentoMensal,
            Ramo = empresa.Ramo.ToString()
        };
    }

    public async Task<EmpresaDto?> ObterEmpresaPorId(Guid id)
    {
        var empresa = await _empresaRepository.ObterEmpresaPorIdAsync (id);

        if (empresa == null) return null;

        return new EmpresaDto
        {
            Id = empresa.Id,
            Cnpj = empresa.Cnpj,
            Nome = empresa.Nome,
            FaturamentoMensal = empresa.FaturamentoMensal,
            Ramo = empresa.Ramo.ToString(),
            Limite = empresa.Limite
        };
    }
}
