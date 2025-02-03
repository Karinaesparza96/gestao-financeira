using Business.Entities;

namespace Business.FiltrosBusca
{
    public class FiltroTransacao
    {
        public DateTime? Data { get; set; }
        public Guid? CategoriaId { get; set; }
        public TipoTransacao? TipoTransacao { get; set; }
    }
}
