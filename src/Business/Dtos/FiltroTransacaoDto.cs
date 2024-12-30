using Business.Entities;

namespace Business.Dtos
{
    public class FiltroTransacaoDto
    {
        public DateTime? Data { get; set; }
        public int? CategoriaId { get; set; }
        public TipoTransacao? TipoTransacao { get; set; }

    }
}
