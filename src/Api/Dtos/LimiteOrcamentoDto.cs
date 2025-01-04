using Business.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class LimiteOrcamentoDto
    {
        public int? CategoriaId { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string UsuarioId { get; set; } = null!;

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public DateOnly Periodo { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public decimal Limite { get; set; }
    }
}
