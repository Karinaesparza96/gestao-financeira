import { Component, OnDestroy, OnInit } from '@angular/core';
import { ReportService } from '../../services/relatorio.services';

@Component({
  selector: 'app-relatorios',
  imports: [],
  templateUrl: './relatorios.component.html',
  styleUrl: './relatorios.component.scss'
})
export class RelatoriosComponent  {

  constructor(private reportService: ReportService) { }

  downloadReportPDF(): void {
    this.reportService.getReportPDF().subscribe(data => {
      const base64Data = data;
      const linkSource = `data:application/pdf;base64,${base64Data}`;
      const downloadLink = document.createElement('a');
      const fileName = 'report.pdf';

      downloadLink.href = linkSource;
      downloadLink.download = fileName;
      downloadLink.click();
    });
  }

  downloadReportCSV(): void {
    this.reportService.getReportCSV().subscribe(data => {
      const base64Data = data;
      const linkSource = `data:text/csv;base64,${base64Data}`;
      const downloadLink = document.createElement('a');
      const fileName = 'report.csv';

      downloadLink.href = linkSource;
      downloadLink.download = fileName;
      downloadLink.click();
    });
  }
}
