import { Injectable } from '@angular/core';
import { TransacaoService } from './transacao.service';
import { Transacao } from '../models/Transacao';
import { ChartData } from 'chart.js';
import { TipoTransacao } from '../models/TipoTransacao';
import { map } from 'rxjs';

@Injectable()
export class GraficoService {
  constructor(private transacaoService: TransacaoService) { }
  obterDados() {
    return this.transacaoService.obterTodos().pipe(
      map(x => {
        return {
          entradasEDespesas: this.obterComparativoEntradasEDespesas(x),
          maioresGastos: this.obterMaioresGastosPorCategoria(x)
        }
      })
    )
  }

  obterComparativoEntradasEDespesas(transacoes: Transacao[]) {
    let chartData: ChartData = {
      labels: [],
      datasets: []
    }
    const dadosMensais = new Map<string, { receita: number, despesa: number }>();
    transacoes.forEach(transacao => {
      const mesAno = new Date(transacao.data).toLocaleDateString('pt-BR', { month: 'short', year: 'numeric' });

      if (!dadosMensais.has(mesAno)) {
        dadosMensais.set(mesAno, { receita: 0, despesa: 0 });
      }
      const valores = dadosMensais.get(mesAno)!;
      if (transacao.tipo === 1) {
        valores.receita += transacao.valor;
      } else {
        valores.despesa += transacao.valor;
      }
    });

    const receitas = [...dadosMensais.values()].map(v => v.receita);
    const despesas = [...dadosMensais.values()].map(v => v.despesa);

    return chartData = {
      labels: [...dadosMensais.keys()],
      datasets: [
        { 
          data: receitas, 
          label: 'Receitas', 
          backgroundColor: 'rgba(0, 128, 0, 0.7)',
          borderColor: 'rgba(0, 128, 0, 1)', 
          borderWidth: 1.5,
          borderRadius: 6, 
          barPercentage: 0.6, 
          categoryPercentage: 0.5,
          hoverBackgroundColor: 'rgba(0, 128, 0, 0.9)', 
        },
        { 
          data: despesas, 
          label: 'Despesas', 
          backgroundColor: 'rgba(255, 0, 0, 0.5)',
          borderColor: 'rgba(255, 0, 0, 1)', 
          borderWidth: 1.5,
          borderRadius: 6,
          barPercentage: 0.6,
          categoryPercentage: 0.5,
          hoverBackgroundColor: 'rgba(255, 0, 0, 0.7)',
        }
      ]
    };
  }

   obterMaioresGastosPorCategoria(transacoes: Transacao[]) {
    let chartData: ChartData = {
      labels: [],
      datasets: []
    }
      const data = new Date()
      const ano = data.getFullYear()
      const mes = String(data.getMonth() + 1).padStart(2, '0')
      const mesAno = `${ano}-${mes}`

      const maioresGastosNoMes = transacoes.filter(x => {
        const data = x.data.split('T')[0].substring(0,7)
        return data == mesAno
      }).reduce((acc, atual) => {
        const nomeCategoria = atual.categoria?.nome ?? 'sem categoria'
        if (atual.tipo != TipoTransacao.Saida) return acc
        if (!acc[nomeCategoria]) {
          acc[nomeCategoria] = 0
        }
        acc[nomeCategoria] += atual.valor
        return acc
      }, {} as any)
  
      return chartData = {
        labels: Object.keys(maioresGastosNoMes),
        datasets: [
          {
            data: Object.values(maioresGastosNoMes),
            label: 'Maiores Gastos Mês Atual',
            backgroundColor: [
              'rgba(255, 99, 132, 0.7)',  
              'rgba(54, 162, 235, 0.7)',  
              'rgba(255, 206, 86, 0.7)', 
              'rgba(75, 192, 192, 0.7)',
              'rgba(153, 102, 255, 0.7)',
              'rgba(255, 159, 64, 0.7)'
            ],
            borderColor: [
              'rgba(255, 99, 132, 1)',
              'rgba(54, 162, 235, 1)',
              'rgba(255, 206, 86, 1)',
              'rgba(75, 192, 192, 1)',
              'rgba(153, 102, 255, 1)',
              'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1.5,
            borderRadius: 6,
            barPercentage: 0.6,
            categoryPercentage: 0.5,
            hoverBackgroundColor: [
              'rgba(255, 99, 132, 0.9)',
              'rgba(54, 162, 235, 0.9)',
              'rgba(255, 206, 86, 0.9)',
              'rgba(75, 192, 192, 0.9)',
              'rgba(153, 102, 255, 0.9)',
              'rgba(255, 159, 64, 0.9)'
            ]
          }
        ]
      }
     }
}
