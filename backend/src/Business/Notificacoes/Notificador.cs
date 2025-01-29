using Business.Interfaces;

namespace Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> Notificacoes { get; } = [];

        public bool TemNotificacao()
        {
            return Notificacoes.Any(x => x.TipoNotificacao == TipoNotificacao.Erro);
        }

        public List<Notificacao> ObterTodos()
        {
            return Notificacoes;
        }

        public void Adicionar(Notificacao notificacao)
        {
            Notificacoes.Add(notificacao);
        }
    }
}
