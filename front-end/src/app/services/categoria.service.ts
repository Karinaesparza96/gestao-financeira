import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { Categoria } from '../models/categoria';
import { ResponseDefault } from '../models/responseDefault';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService extends BaseService {

  constructor(private http: HttpClient) { super(); }

  obterTodos() : Observable<Categoria[]> {
    return this.http.get<ResponseDefault>(`${this.UrlService}/categorias`, this.ObterAuthHeaderJson())
                    .pipe(
                      map(this.extractData),
                      catchError(this.serviceError)
                    );
  }
}
