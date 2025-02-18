import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs';
import { LimiteOrcamento } from '../models/limiteOrcamento';

@Injectable({
  providedIn: 'root'
})
export class LimiteService extends BaseService {

  constructor(private http: HttpClient) { super() }

  obterTodos() {
    return this.http.get(`${this.UrlService}/limites`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData),
                      catchError(this.serviceError)
                    )
  }

  obterPorId(id: string) {
    return this.http.get(`${this.UrlService}/limites/${id}`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData), 
                      catchError(this.serviceError)
                    )
  }

  adicionar(limite: LimiteOrcamento) {
    return this.http.post(`${this.UrlService}/limites`, limite, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractMensagens), 
                      catchError(this.serviceError)
                    )

  }

  atualizar(id: string, limite: LimiteOrcamento) {
    return this.http.put(`${this.UrlService}/limites/${id}`, limite, this.ObterAuthHeaderJson())
                    .pipe(
                      catchError(this.serviceError)
                    )
  }

  excluir(id: string) {
    return this.http.delete(`${this.UrlService}/limites/${id}`, this.ObterAuthHeaderJson())
                    .pipe(
                      catchError(this.serviceError)
                    )
  }
}
