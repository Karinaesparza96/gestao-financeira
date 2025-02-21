import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ReportService extends BaseService {

  private apiUrl = 'URL_DA_SUA_API';

  constructor(private http: HttpClient) {
    super();
  }

  getReportPDF(): Observable<any> {
    return this.http.get(this.UrlService + "/relatorios/categorias/pdf", { responseType: 'text' });
  }

  getReportCSV(): Observable<any> {
    return this.http.get(this.UrlService + "/relatorios/categorias/csv", { responseType: 'text' });
  }
}
