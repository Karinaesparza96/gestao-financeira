using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class LimiteOrcamento : Entity
    {
        public Guid? CategoriaId { get; set; }
        
        public string UsuarioId { get; set; } = null!;

        public DateOnly Periodo { get; set; } 

        public decimal Limite { get; set; }

        public TipoLimite TipoLimite { get; set; }

        public decimal PorcentagemAviso { get; set; }

        // Props navigation ef
        public Categoria? Categoria { get; set; }

        public Usuario Usuario { get; set; } = null!;

    }

    public enum TipoLimite
    {
        Geral = 1,
        Categoria
    }
}
