import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Categoria } from '../models/categoria';
import { ResponseDefault } from '../models/responseDefault';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

  constructor(private http: HttpClient) { }

  obterTodos() : Observable<Categoria[]> {
    return this.http.get<ResponseDefault>('http://localhost:5224/api/categorias').pipe(map(r => r.data));
  }
}
