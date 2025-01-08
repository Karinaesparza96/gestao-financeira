namespace Business.Entities
{
    public class Usuario : Entity
    {
        public new string Id { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;

    }
}
