import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { Relatorio } from '../models/relatorio';

@Injectable({
  providedIn: 'root'
})
export class ReportService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getReportCategoria(tipo: string): Observable<any> {
    return this.http.get(this.UrlService + "/relatorios/categorias/" + tipo, { responseType: 'text', headers: this.ObterAuthHeaderJson().headers  });
  }

  getReportTransacao(relatorio: Relatorio): Observable<any> {
    let params = new HttpParams();

    if (relatorio.categoriaId){
      params = params.set('categoriaId', relatorio.categoriaId)
    }

    if (relatorio.data){
      params = params.set('data', relatorio.data)
    }

    if (relatorio.tipoTransacao){
      params = params.set('tipoTransacao', relatorio.tipoTransacao);
    }

    return this.http.get(this.UrlService + "/relatorios/transacoes/" + relatorio.tipoRelatorio, { params: params, responseType: 'text', headers: this.ObterAuthHeaderJson().headers  });
  }
}
