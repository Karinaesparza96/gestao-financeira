import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RetornoPadrao } from '../utils/retornoPadrao';
import { catchError, map, Observable, of } from 'rxjs';
import { Transacao } from '../models/Transacao';
import { ResumoFinanceiro } from '../models/resumoFinanceiro';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class TransacaoService extends BaseService {

  constructor(private http: HttpClient) { super(); }

  obterTodos() {
    return this.http.get<RetornoPadrao>(`${this.UrlService}/transacoes`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData),
                      catchError(this.serviceError)
                    )
  }

  obterPorId(id: string) {
    return this.http.get<RetornoPadrao>(`${this.UrlService}/transacoes/${id}`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData), 
                      catchError(this.serviceError)
                    )
  }

  obterResumoTransacoes(): Observable<ResumoFinanceiro> {
    return this.http.get<RetornoPadrao>(`${this.UrlService}/transacoes/resumo`, this.ObterAuthHeaderJson())
              .pipe(
                map(this.extractData),
                catchError(this.serviceError)
              )
  }

  adicionar(transacao: Transacao) {
    return this.http.post(`${this.UrlService}/transacoes`, transacao, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractMensagens),
                      catchError(this.serviceError)
                    )
  }

  atualizar(id:string, transacao: Transacao) {
    return this.http.put(`${this.UrlService}/transacoes/${id}`, transacao, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractMensagens),
                      catchError(this.serviceError)
                    )
  }

  excluir(id: string) {
    return this.http.delete(`${this.UrlService}/transacoes/${id}`, this.ObterAuthHeaderJson())
                    .pipe(
                      catchError(this.serviceError)
                    )
  }
}
