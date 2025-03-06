import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs';
import { LimiteOrcamento } from '../models/limiteOrcamento';
import { FiltroBuscaLimite } from '../models/filtroBusca';
import { CurrencyUtils } from '../utils/currency-utils';

@Injectable({
  providedIn: 'root'
})
export class LimiteService extends BaseService {

  constructor(private http: HttpClient) { super() }

  obterTodos() {
    return this.http.get(`${this.UrlService}/limites-orcamentos`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData),
                      catchError(this.serviceError)
                    )
  }

  obterTodosComFiltro(filtro: FiltroBuscaLimite) {
    const params = Object.entries(filtro).map(key => key.join('=')).join('&');
    return this.http.get(`${this.UrlService}/limites-orcamentos?${params}`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData),
                      catchError(this.serviceError)
                    );
  }

  obterPorId(id: string) {
    return this.http.get(`${this.UrlService}/limites-orcamentos/${id}`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData), 
                      catchError(this.serviceError)
                    )
  }

  adicionar(limite: LimiteOrcamento) {
    return this.http.post(`${this.UrlService}/limites-orcamentos`, limite, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractMensagens), 
                      catchError(this.serviceError)
                    )

  }

  atualizar(id: string, limite: LimiteOrcamento) {
    return this.http.put(`${this.UrlService}/limites-orcamentos/${id}`, limite, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractMensagens),
                      catchError(this.serviceError)
                    )
  }

  excluir(id: string) {
    return this.http.delete(`${this.UrlService}/limites-orcamentos/${id}`, this.ObterAuthHeaderJson())
                    .pipe(
                      catchError(this.serviceError)
                    )
  }
}
