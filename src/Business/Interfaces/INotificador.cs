using Business.Notificacoes;

namespace Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterTodos();
        void Adicionar(Notificacao notificacao);    
    }
}
