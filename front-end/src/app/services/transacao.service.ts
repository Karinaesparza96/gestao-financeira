import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RetornoPadrao } from '../utils/retornoPadrao';
import { catchError, map, Observable, of } from 'rxjs';
import { Transacao } from '../models/Transacao';
import { ResumoFinanceiro } from '../models/resumoFinanceiro';
import { BaseService } from './base.service';
import { FiltroBuscaTransacao } from '../models/filtroBusca';
import { CurrencyUtils } from '../utils/currency-utils';

@Injectable({
  providedIn: 'root'
})
export class TransacaoService extends BaseService {

  constructor(private http: HttpClient) { super(); }

  obterTodos(): Observable<Transacao[]> {
    return this.http.get<RetornoPadrao>(`${this.UrlService}/transacoes`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData),
                      catchError(this.serviceError)
                    )
  }

  obterTodosComFiltro(filtro: FiltroBuscaTransacao): Observable<Transacao[]> {
    const params = Object.entries(filtro).map(key => key.join('=')).join('&');
    return this.http.get<RetornoPadrao>(`${this.UrlService}/transacoes?${params}`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData),
                      catchError(this.serviceError)
                    )
  }

  obterPorId(id: string): Observable<Transacao> {
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
    transacao.valor = CurrencyUtils.StringParaDecimal(transacao.valor);
    return this.http.post(`${this.UrlService}/transacoes`, transacao, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractMensagens),
                      catchError(this.serviceError)
                    )
  }

  atualizar(id:string, transacao: Transacao) {
    transacao.valor = CurrencyUtils.StringParaDecimal(transacao.valor);
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
