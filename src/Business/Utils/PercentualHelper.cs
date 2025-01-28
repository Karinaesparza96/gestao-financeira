using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utils
{
    public static class PercentualHelper
    {
        public static decimal CalcularPorcentagem(decimal valorAtual, decimal valorTotal)
        {
            return Math.Round((valorAtual / valorTotal) * 100, 2);
        }

        public static decimal CalcularPercentualExcedido(decimal valorAtual, decimal valorTotal)
        {
            return CalcularPorcentagem(valorAtual, valorTotal) - 100;
        }
    }
}
