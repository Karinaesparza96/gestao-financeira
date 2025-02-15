import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RetornoPadrao } from '../utils/retornoPadrao';
import { map, Observable } from 'rxjs';
import { Transacao } from '../models/Transacao';
import { ResumoFinanceiro } from '../models/resumoFinanceiro';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class TransacaoService extends BaseService {

  constructor(private http: HttpClient) { super(); }

  obterResumoTransacoes(): Observable<ResumoFinanceiro> {
    return this.http.get<RetornoPadrao>('http://localhost:5224/api/transacoes/resumo', this.ObterAuthHeaderJson())
              .pipe(map(resp => resp.data as ResumoFinanceiro))
  }

  adicionar(transacao: Transacao) {
    console.log('adicionar:',transacao)
    return this.http.post('http://localhost:5224/api/transacoes', transacao, this.ObterAuthHeaderJson())
  }
}
