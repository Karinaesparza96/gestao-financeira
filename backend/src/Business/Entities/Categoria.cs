namespace Business.Entities
{
    public class Categoria : Entity 
    {
        public string Nome { get; set; } = null!;

        public bool Default { get; set; }

        public string? UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        // EF relational
        public IEnumerable<Transacao>? Transacoes { get; set; }

        public IEnumerable<LimiteOrcamento>? Limites { get; set; }
    }
}
