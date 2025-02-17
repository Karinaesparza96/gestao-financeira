import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificacaoService {

  private mensagemSubject = new BehaviorSubject<{mensagem: string, tipo: 'sucesso' | 'falha'} | null>(null);
  mensagem$ = this.mensagemSubject.asObservable();

  mostrarMensagem(mensagem: string, duracaoMs: number = 3000, tipo: 'sucesso' | 'falha' = 'sucesso') {
    this.mensagemSubject.next({mensagem, tipo});

    setTimeout(() => {
      this.mensagemSubject.next(null);
    }, duracaoMs);
  }
}
