using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class CategoriaDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public bool Default { get; set; }
    }
}
