namespace Business.Entities
{
    public class LimiteOrcamento : Entity
    {
        public int? CategoriaId { get; set; }

        public string UsuarioId { get; set; } = null!;

        public DateOnly Periodo { get; set; } 

        public decimal Limite { get; set; }

        public bool LimiteGeral => CategoriaId == null;

        // Props navigation ef
        public Categoria? Categoria { get; set; }

        public Usuario Usuario { get; set; } = null!;

    }
}
