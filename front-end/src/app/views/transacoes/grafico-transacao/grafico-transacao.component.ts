import { Component, Input } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-grafico-transacao',
  imports: [BaseChartDirective],
  templateUrl: './grafico-transacao.component.html',
  styleUrl: './grafico-transacao.component.scss'
})
export class GraficoTransacaoComponent {
  @Input() chartData?: {
    labels: string[],
    datasets: []
  };

  chartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    scales: {
      x: {
        grid: {
          display: false, 
        },
      },
      y: {
        grid: {
          color: '#f0f0f0',
        },
        ticks: {
          precision: 0, 
        }
      }
    },
    plugins: {
      legend: {
        position: 'top', 
        labels: {
          color: '#333',
        }
      }
    }
  };

 
}
