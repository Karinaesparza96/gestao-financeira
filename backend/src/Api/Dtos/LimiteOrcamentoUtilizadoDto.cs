using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class LimiteOrcamentoUtilizadoDto
    {
        public Guid? Id { get; set; }
        public Guid? CategoriaId { get; set; }
        public string? CategoriaNome { get; set; }
        public DateOnly Periodo { get; set; }
        public decimal Limite { get; set; }
        public int PorcentagemAviso { get; set; }
        public int TipoLimite { get; set; }
        public string UsuarioId { get; set; } = null!;
        public decimal? LimiteUtilizado { get; set; }
        public string? PercentualLimiteUtilizado { get; set; }
    }
}
