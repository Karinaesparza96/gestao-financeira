import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { Categoria } from '../models/categoria';
import { ResponseDefault } from '../models/responseDefault';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService extends BaseService {

  constructor(private http: HttpClient) { super(); }

  obterTodos() : Observable<Categoria[]> {

    const mock = {
      "sucesso": true,
      "data": [
          {
              "id": "5f8f9205-1aa7-4caa-8cbd-24c2f84a6dc2",
              "nome": "Saúde",
              "default": true
          },
          {
              "id": "e8285697-4a2e-4a48-b415-5c3ad156697d",
              "nome": "Alimentação",
              "default": true
          },
          {
              "id": "e9470c9f-c99d-4cfe-986c-20b737eed946",
              "nome": "Transporte",
              "default": true
          }
      ],
      "mensagens": []
  }

  return of(mock.data)
    return this.http.get<ResponseDefault>('http://localhost:5224/api/categorias', this.ObterAuthHeaderJson()).pipe(map(r => r.data));
  }
}
