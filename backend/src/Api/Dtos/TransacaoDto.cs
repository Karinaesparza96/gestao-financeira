using Business.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class TransacaoDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("categoria da transação")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("tipo da transação")]
        public TipoTransacao Tipo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("data da transação")]
        public DateTime Data { get; set; }
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("valor da transação")]
        public decimal Valor { get; set; }
    }
}
