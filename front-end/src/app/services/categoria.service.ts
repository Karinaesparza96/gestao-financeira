import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Categoria } from '../models/categoria';
import { ResponseDefault } from '../models/responseDefault';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService extends BaseService {

  constructor(private http: HttpClient) { super(); }

  obterTodos() : Observable<Categoria[]> {
    return this.http.get<ResponseDefault>('http://localhost:5224/api/categorias', this.ObterAuthHeaderJson()).pipe(map(r => r.data));
  }
}
