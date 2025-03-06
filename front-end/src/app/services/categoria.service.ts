import { Categoria, NovaCategoria } from './../models/categoria';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { ResponseDefault } from '../models/responseDefault';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService extends BaseService {

  constructor(private http: HttpClient) { super(); }

  obterTodos() : Observable<Categoria[]> {
    return this.http.get<ResponseDefault>(this.UrlService + '/categorias', this.ObterAuthHeaderJson()).
      pipe(
        map(this.extractData),
        catchError(this.serviceError)
      );
  }

  ObterPorId(id: string) : Observable<Categoria[]>{
    return this.http.get<ResponseDefault>(this.UrlService + '/categorias' + id, this.ObterAuthHeaderJson())
      .pipe(
        map(this.extractData),
        catchError(this.serviceError)
      );
  }

  novaCategoria(categoria: NovaCategoria): Observable<Categoria[]>{
    return this.http.post<ResponseDefault>(this.UrlService + '/categorias', categoria, this.ObterAuthHeaderJson())
      .pipe(
        map(this.extractMensagens),
        catchError(this.serviceError)
      );
  }

  atualizarCategoria(categoria: Categoria): Observable<Categoria[]> {
    return this.http.put<ResponseDefault>(this.UrlService + '/categorias/' + categoria.id, categoria, this.ObterAuthHeaderJson())
      .pipe(
        map(this.extractMensagens),
        catchError(this.serviceError)
      );
  }

  excluirCategoria(id: string): Observable<Categoria[]>{
    return this.http.delete<ResponseDefault>(this.UrlService + "/categorias/" + id, this.ObterAuthHeaderJson())
      .pipe(
        map(this.extractData),
        catchError(this.serviceError)
      );
  }
}
