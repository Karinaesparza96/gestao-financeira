using Business.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class RelatorioTransacaoDto
    {
        public Guid? Id { get; set; }
        public string Tipo { get; set; }
        public string Data { get; set; }

        [DisplayName("Descrição")]
        public string? Descricao { get; set; }
        public string Categoria { get; set; }
        public string Valor { get; set; }
    }
}
