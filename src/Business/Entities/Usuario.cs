using Microsoft.AspNetCore.Identity;

namespace Business.Entities
{
    public class Usuario : IdentityUser
    {
        public string? Nome { get; set; }

    }
}
