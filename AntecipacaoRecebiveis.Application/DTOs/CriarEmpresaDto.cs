namespace AntecipacaoRecebiveis.Application.DTOs;
public class CriarEmpresaDto
{
    public string Cnpj { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public decimal FaturamentoMensal { get; set; }
    public string Ramo { get; set; } = string.Empty; // será convertido para Enum
}
