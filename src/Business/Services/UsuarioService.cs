using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public class UsuarioService(UserManager<Usuario> userManager) : IUsuarioService
    {   
        private readonly UserManager<Usuario> _userManager = userManager;
        public async Task<Usuario?> ObterUsuarioPorId(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
