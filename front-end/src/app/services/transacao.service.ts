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
          "id": "c10d72e9-31ac-4b0c-be98-8ecfa5fb3321",
          "categoria": {
              "id": "e8285697-4a2e-4a48-b415-5c3ad156697d",
              "nome": "Alimentação",
              "default": true
          },
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
