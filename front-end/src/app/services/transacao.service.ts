import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TransacaoService {

  constructor(private http: HttpClient) { }

  obterResumoTransacoes() {
    this.http.get('http://localhost:5224/api/transacoes/resumo')
  }
}
