﻿using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class LimiteOrcamentoDto
    {
        public Guid? Id { get; set; }
        public int? CategoriaId { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public DateOnly Periodo { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public decimal Limite { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public int PorcentagemAviso { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public int TipoLimite { get; set; }
    }
}
