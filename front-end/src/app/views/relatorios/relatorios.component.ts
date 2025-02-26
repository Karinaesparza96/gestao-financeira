import { Component } from '@angular/core';
import { ReportService } from '../../services/relatorio.services';
import { CategoriaService } from '../../services/categoria.service';
import { Categoria } from '../../models/categoria';
import { CommonModule } from '@angular/common';
import { Relatorio } from '../../models/relatorio';

@Component({
  selector: 'app-relatorios',
  imports: [CommonModule],
  templateUrl: './relatorios.component.html',
  styleUrl: './relatorios.component.scss'
})
export class RelatoriosComponent  {
  categorias: Categoria[] = []
  erro: string | null = null;
  options = [
    { value: '1', label: 'Entrada' },
    { value: '2', label: 'Saída' }
  ];

  optionsRelatorio = [
    { value: 'csv', label: 'csv' },
    { value: 'pdf', label: 'pdf' }
  ];

  constructor(private reportService: ReportService,
    private categoriaService: CategoriaService) {
    this.categoriaService.obterTodos().subscribe(x => this.categorias = x);
  }

  downloadReportCategoria(tipo: string): void {
    this.reportService.getReportCategoria(tipo).subscribe(data => {
      const base64Data = data;
      const linkSource = `data:text/${tipo};base64,${base64Data}`;
      const downloadLink = document.createElement('a');
      const fileName = `categoria.${tipo}`;

      downloadLink.href = linkSource;
      downloadLink.download = fileName;
      downloadLink.click();
    });
  }

  downloadReportTransacao(relatorio: Relatorio): void {
    if (!relatorio.tipoRelatorio?.trim()) {
      this.erro = 'O tipo do relatório deve ser selecionado.';
      return;
    }

    this.reportService.getReportTransacao(relatorio).subscribe(data => {
      const base64Data = data;
      const linkSource = `data:text/${relatorio.tipoRelatorio};base64,${base64Data}`;
      const downloadLink = document.createElement('a');
      const fileName = `transacao.${relatorio.tipoRelatorio}`;

      downloadLink.href = linkSource;
      downloadLink.download = fileName;
      downloadLink.click();

      this.erro = null;
    });
  }
}
