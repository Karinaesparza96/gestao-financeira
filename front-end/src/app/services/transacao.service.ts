import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RetornoPadrao } from '../utils/retornoPadrao';
import { map, Observable, of } from 'rxjs';
import { Transacao } from '../models/Transacao';
import { ResumoFinanceiro } from '../models/resumoFinanceiro';

@Injectable({
  providedIn: 'root'
})
export class TransacaoService {

  constructor(private http: HttpClient) { }

  obterPorId(id: string) {
    const mockResponse: RetornoPadrao = {
      "sucesso": true,
      "data": {
          "id": "8902a88d-2a28-45dc-8666-1c3df1dc43f3",
          "categoriaId": "5f8f9205-1aa7-4caa-8cbd-24c2f84a6dc2",
          "tipo": 2,
          "data": "2024-12-28T15:30:00",
          "descricao": "teste",
          "valor": 200
      },
      "mensagens": []
  }
  
    return of(mockResponse).pipe(
      map(resp => resp.data as Transacao)
    );
    return this.http.get<RetornoPadrao>(`/assets/mock/transacao.json`).pipe(map(resp => resp.data as Transacao))
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
