using Business.Entities;

namespace Business.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> ObterUsuarioPorId(string id);
    }
}
