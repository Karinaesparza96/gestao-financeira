using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class Transacao : Entity
    {   
        public Guid CategoriaId { get; set; }

        public string UsuarioId { get; set; } = null!;
        public TipoTransacao Tipo { get; set; }
        public DateTime Data { get; set; }
        
        public string? Descricao { get; set; }

        public decimal Valor { get; set; }

        // Props navigation ef
        public Categoria Categoria { get; set; } = null!;

        public Usuario Usuario { get; set; } = null!;

    }
}
