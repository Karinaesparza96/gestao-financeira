﻿using System.ComponentModel.DataAnnotations;

namespace Business.Dtos
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Campo {0} está em formato inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "Campo {0} precisa ter entre {1} e {2} caracteres.", MinimumLength = 8)]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string? ConfimacaoSenha { get; set; }
    }
}
