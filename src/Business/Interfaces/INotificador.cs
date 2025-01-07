using Business.Notificacoes;

namespace Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao(TipoNotificacao? tipo = null);
        List<Notificacao> ObterTodos();

        List<Notificacao> ObterPorTipo(TipoNotificacao tipo);
        void Adicionar(Notificacao notificacao);    
    }
}
