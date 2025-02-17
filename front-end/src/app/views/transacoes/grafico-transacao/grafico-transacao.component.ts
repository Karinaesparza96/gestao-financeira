import { Component, Input, OnInit } from '@angular/core';
import { ChartData, ChartOptions } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { Transacao } from '../../../models/Transacao';

@Component({
  selector: 'app-grafico-transacao',
  imports: [BaseChartDirective],
  templateUrl: './grafico-transacao.component.html',
  styleUrl: './grafico-transacao.component.scss'
})
export class GraficoTransacaoComponent implements OnInit {
  private _transacoes?: Transacao[];

  @Input()
  set transacoes(value: Transacao[] | undefined) {
    this._transacoes = value;
    this.processarDados();
  }

  get transacoes(): Transacao[] | undefined {
    return this._transacoes;
  }

  chartOptions: ChartOptions = {
    responsive: true,
  };

  chartLabels: any[] = [];
  chartData: ChartData<'bar'> = {
    labels: this.chartLabels,
    datasets: []
  };

  ngOnInit(): void {
    this.processarDados()
  }

  processarDados() {
     const dadosMensais = new Map<string, { receita: number, despesa: number }>();
     this.transacoes?.forEach(transacao => {
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

     this.chartLabels = Array.from(dadosMensais.keys());
     const receitas = Array.from(dadosMensais.values()).map(v => v.receita);
     const despesas = Array.from(dadosMensais.values()).map(v => v.despesa);

     this.chartData = {
       labels: this.chartLabels,
       datasets: [
         { data: receitas, label: 'Receitas', backgroundColor: 'green' },
         { data: despesas, label: 'Despesas', backgroundColor: 'red' }
       ]
     };
   }
}
