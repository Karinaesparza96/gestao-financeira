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
    return this.http.get<ResponseDefault>(this.UrlService + '/categorias', this.ObterAuthHeaderJson()).pipe(map(this.extractData), catchError(this.serviceError));
  }

  ObterPorId(id: string) : Observable<Categoria[]>{
    return this.http.get<ResponseDefault>(this.UrlService + '/categorias' + id, this.ObterAuthHeaderJson()).pipe(map(this.extractData), catchError(this.serviceError));
  }

  novaCategoria(categoria: NovaCategoria): Observable<Categoria[]>{
    return this.http.post<ResponseDefault>(this.UrlService + '/categorias', categoria, this.ObterAuthHeaderJson()).pipe(map(this.extractData), catchError(this.serviceError));
  }

  // atualizarCategoria<ResponseDefault>(categoria: Categoria): Observable<Categoria[]>{
  //   return this.http.put<ResponseDefault>(this.UrlService + "/categorias/" + categoria.id, categoria, this.ObterAuthHeaderJson());
  // }

  atualizarCategoria(categoria: Categoria): Observable<Categoria[]> {
    return this.http.put<ResponseDefault>(this.UrlService + '/categorias/' + categoria.id, categoria, this.ObterAuthHeaderJson())
      .pipe(
        map(this.extractData),
        catchError(this.serviceError)
      );
  }

  excluirCategoria(id: string): Observable<Categoria[]>{
    return this.http.delete<ResponseDefault>(this.UrlService + "/categorias/" + id, this.ObterAuthHeaderJson()).pipe(map(this.extractData), catchError(this.serviceError));
  }
}

// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { catchError, map, Observable, of } from 'rxjs';
// import { Categoria } from '../models/categoria';
// import { ResponseDefault } from '../models/responseDefault';
// import { BaseService } from './base.service';

// @Injectable({
//   providedIn: 'root'
// })
// export class CategoriaService extends BaseService {
//   // private mockCategorias: Categoria[] = [
//   //   {
//   //     id: "5f8f9205-1aa7-4caa-8cbd-24c2f84a6dc2",
//   //     nome: "Saúde",
//   //     default: true
//   //   },
//   //   {
//   //     id: "e8285697-4a2e-4a48-b415-5c3ad156697d",
//   //     nome: "Alimentação",
//   //     default: true
//   //   },
//   //   {
//   //     id: "e9470c9f-c99d-4cfe-986c-20b737eed946",
//   //     nome: "Transporte",
//   //     default: true
//   //   },
//   //   {
//   //     id: "7f8d9e10-b2c3-4d5e-6f7g-8h9i0j1k2l3m",
//   //     nome: "Lazer",
//   //     default: false
//   //   },
//   //   {
//   //     id: "3a4b5c6d-7e8f-9g0h-1i2j-3k4l5m6n7o8p",
//   //     nome: "Educação",
//   //     default: false
//   //   }
//   // ];

//   constructor(private http: HttpClient) { super(); }

//   obterTodos(): Observable<Categoria[]> {
//     // console.log('Mock de categorias:', this.mockCategorias);
//     // return of(this.mockCategorias);
//     // return this.http.get<ResponseDefault>(this.UrlService + '/categorias', this.ObterAuthHeaderJson())
//     //   .pipe(map(this.extractData));
//       return this.http.get<ResponseDefault>(this.UrlService + '/categorias', this.ObterAuthHeaderJson()).pipe(map(this.extractData), catchError(this.serviceError));
//   }

//   obterPorId(id: string): Observable<Categoria> {
//     const categoria = this.mockCategorias.find(c => c.id === id);
//     return of(categoria as Categoria);
//     // return this.http.get<ResponseDefault>(`http://localhost:5224/api/categorias/${id}`, this.ObterAuthHeaderJson())
//     //   .pipe(map(r => r.data));
//   }

//   novaCategoria(categoria: Categoria): Observable<ResponseDefault> {
//     this.mockCategorias.push(categoria);
//     const mockResponse: ResponseDefault = {
//       sucesso: true,
//       data: [],
//       mensagens: ['Categoria criada com sucesso!']
//     };
//     return of(mockResponse);
//     // return this.http.post<ResponseDefault>('http://localhost:5224/api/categorias', categoria, this.ObterAuthHeaderJson());
//   }

//   atualizarCategoria(categoria: Categoria): Observable<ResponseDefault> {
//     const index = this.mockCategorias.findIndex(c => c.id === categoria.id);
//     if (index !== -1) {
//       this.mockCategorias[index] = categoria;
//     }
//     const mockResponse: ResponseDefault = {
//       sucesso: true,
//       data: [],
//       mensagens: ['Categoria atualizada com sucesso!']
//     };
//     return of(mockResponse);
//     // return this.http.put<ResponseDefault>(`http://localhost:5224/api/categorias/${categoria.id}`, categoria, this.ObterAuthHeaderJson());
//   }

//   excluirCategoria(id: string): Observable<ResponseDefault> {
//     const index = this.mockCategorias.findIndex(c => c.id === id);
//     if (index !== -1) {
//       this.mockCategorias.splice(index, 1);
//     }
//     const mockResponse: ResponseDefault = {
//       sucesso: true,
//       data: [],
//       mensagens: ['Categoria excluída com sucesso!']
//     };
//     return of(mockResponse);
//     // return this.http.delete<ResponseDefault>(`http://localhost:5224/api/categorias/${id}`, this.ObterAuthHeaderJson());
//   }
// }
