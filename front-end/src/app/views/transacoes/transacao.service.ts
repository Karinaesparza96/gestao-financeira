import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ResumoFinanceiro } from '../../models/resumoFinanceiro';
import { Transacao } from '../../models/Transacao';
import { RetornoPadrao } from '../../utils/retornoPadrao';

@Injectable({
  providedIn: 'root'
})
export class TransacaoService {

  constructor(private http: HttpClient) { }

  obterPorId(id: string) {
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxNjFjOWY5OS1kNGFmLTRkYTctOGNjMC03ZmIxOGQyNGQzODUiLCJuYmYiOjE3MzkzOTkwMTIsImV4cCI6MTczOTQwMjYxMiwiaWF0IjoxNzM5Mzk5MDEyLCJpc3MiOiJNZXVTaXN0ZW1hIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ.SK2SEmHAInjoqfuU7T2qdFzT5zym1axIQAZGVAtCQ0o"
    const headers = new HttpHeaders().set('Authorization',`Bearer ${token}`);
    return this.http.get<RetornoPadrao>(`http://localhost:5224/api/transacoes/${id}`, {headers})
              .pipe(map(resp => resp.data as Transacao))
  }

  obterResumoTransacoes(): Observable<ResumoFinanceiro> {
    return this.http.get<RetornoPadrao>('http://localhost:5224/api/transacoes/resumo')
              .pipe(map(resp => resp.data as ResumoFinanceiro))
  }

  adicionar(transacao: Transacao) {
    console.log('adicionar:',transacao)
    return this.http.post('http://localhost:5224/api/transacoes', transacao)
  }
}
