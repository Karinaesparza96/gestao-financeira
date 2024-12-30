using System.ComponentModel.DataAnnotations;

namespace Business.Dtos
{
    public class CategoriaDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public bool Default { get; set; }
    }
}
