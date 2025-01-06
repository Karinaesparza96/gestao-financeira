using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public class UsuarioService(UserManager<Usuario> userManager) : IUsuarioService
    {
        public async Task<Usuario?> ObterUsuarioPorId(string id)
        {
            return await userManager.FindByIdAsync(id);
        }
    }
}
