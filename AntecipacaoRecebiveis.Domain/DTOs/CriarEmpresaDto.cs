namespace AntecipacaoRecebiveis.Domain.DTOs;
public class CriarEmpresaDto
{
    public string Cnpj { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public decimal FaturamentoMensal { get; set; }
    public int Ramo { get; set; } 
}
