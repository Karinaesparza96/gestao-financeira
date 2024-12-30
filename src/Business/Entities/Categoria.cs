﻿namespace Business.Entities
{
    public class Categoria : Entity 
    {
        public string Nome { get; set; } = null!;

        public bool Default { get; set; }

        public string? UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
