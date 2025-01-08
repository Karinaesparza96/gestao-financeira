using Business.Interfaces;

namespace Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> Notificacoes { get; } = [];

        public bool TemNotificacao(TipoNotificacao? tipo = null)
        {
            return tipo == null ? Notificacoes.Any() : Notificacoes.Any(n => n.TipoNotificacao == tipo);
        }

        public List<Notificacao> ObterTodos()
        {
            return Notificacoes;
        }

        public List<Notificacao> ObterPorTipo(TipoNotificacao tipo)
        {
            return Notificacoes.Where(n => n.TipoNotificacao == tipo).ToList();
        }

        public void Adicionar(Notificacao notificacao)
        {
            Notificacoes.Add(notificacao);
        }
    }
}
