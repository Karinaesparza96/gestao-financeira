using Business.Entities;

namespace Business.FiltrosBusca
{
    public class FiltroTransacao
    {
        public DateTime? Data { get; set; }
        public int? CategoriaId { get; set; }
        public TipoTransacao? TipoTransacao { get; set; }
    }
}
